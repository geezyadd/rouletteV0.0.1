using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpinStarter : MonoBehaviour
{
    [SerializeField] private List<SlotSpin> _slots = new List<SlotSpin>();
    [SerializeField] private GameObject _rouleteCanvas;
    [SerializeField] private GameObject _slotsCanvas;
    [SerializeField] private GameObject _cardsButton;
    private GameStateButtonsHandler _gameStateButtonsHandler;
    private SlotSpinModel _slotSpinModel;
    private MiniGameSpin _miniGameSpin;
    private AccountModel _accountModel;
    private SpinRewardConfig _spinRewardConfig;

    [Inject]
    private void InjectDependencies(GameStateButtonsHandler gameStateButtonsHandler, SlotSpinModel slotSpinModel, MiniGameSpin miniGameSpin, AccountModel accountModel, SpinRewardConfig spinRewardConfig)
    {
        _gameStateButtonsHandler = gameStateButtonsHandler;
        _slotSpinModel = slotSpinModel;
        _miniGameSpin = miniGameSpin;
        _accountModel = accountModel;
        _spinRewardConfig = spinRewardConfig;
    }

    public void OnEnable()
    {
        _gameStateButtonsHandler.OnSpinButtonClicked += SpinSlots;
    }

    public void OnDisable()
    {
        _slotSpinModel.IsSpin = false;
        _slotSpinModel.IsMiniGameSpin = false;
        _slotSpinModel.Clear();
        _gameStateButtonsHandler.OnSpinButtonClicked -= SpinSlots;
    }

    private void SpinSlots()
    {
        if (!_slotSpinModel.IsSpin && !_slotSpinModel.IsMiniGameSpin && _slotSpinModel.IsMiniGame)
        {
            _cardsButton.SetActive(false);
            _slotsCanvas.SetActive(false);
            _rouleteCanvas.SetActive(true);
            _slotSpinModel.IsMiniGameSpin = true;
            _miniGameSpin.StartSpin();
        }
        if (!_slotSpinModel.IsSpin && !_slotSpinModel.IsMiniGameSpin)
        {
            _cardsButton.SetActive(true);
            _slotsCanvas.SetActive(true);
            _rouleteCanvas.SetActive(false);
            if (_accountModel.Vault - _spinRewardConfig.OneSpinCost < 0)
            {
                Debug.LogError("No moneeeeeey");
                return;
            }
            _slotSpinModel.IsRewarCounted = false;
            _slotSpinModel.IsSpin = true;
            foreach (SlotSpin slot in _slots) {
                slot.Spin();
            }
        }
    }
}
