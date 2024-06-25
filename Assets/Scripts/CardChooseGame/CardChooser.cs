using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CardChooser : MonoBehaviour
{
    [SerializeField] private Image _chooseSprite;
    [SerializeField] private float _initialIntervalMin = 0.1f;
    [SerializeField] private float _initialIntervalMax = 0.5f;
    [SerializeField] private float _slowdownFactor = 1.2f;
    [SerializeField] private int _currentSpriteIndex = 0;
    [SerializeField] private float _stopInterval = 0.8f;
    [SerializeField] private Button _startGameButton;
    private CardsConfig _cardConfig;
    private CardChooseGameModel _cardChooseGameModel;
    private float _currentInterval;

    [Inject]
    private void InjectDependencies(CardsConfig config, CardChooseGameModel cardChooseGameModel)
    {
        _cardConfig = config;
        _cardChooseGameModel = cardChooseGameModel;
    }

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(StartCardGame);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(StartCardGame);
    }

    private void StartCardGame()
    {
        if (_cardChooseGameModel.IsCardGame)
        {
            return;
        }
        _cardChooseGameModel.IsCardGame = true;
        _currentInterval = Random.Range(_initialIntervalMin, _initialIntervalMax);
        StartCoroutine(ChangeSprite());
    }

    IEnumerator ChangeSprite()
    {
        _cardChooseGameModel.IsCardSpin = true;
        while (_currentInterval < _stopInterval)
        {
            if (_currentSpriteIndex >= _cardConfig.Cards.Count)
            {
                _currentSpriteIndex = 0;
            }
            _chooseSprite.sprite = _cardConfig.Cards[_currentSpriteIndex].Sprite;
            _cardChooseGameModel.CardType = _cardConfig.Cards[_currentSpriteIndex].CardType;
            _currentSpriteIndex++;

            yield return new WaitForSeconds(_currentInterval);

            _currentInterval *= _slowdownFactor;
        }
        _cardChooseGameModel.IsCardSpin = false;
    }
}
