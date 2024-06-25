using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CardGame : MonoBehaviour
{
    [SerializeField] private List<CardsInitializerHolder> _cards;
    private CardChooseGameModel _cardChooseGameModel;
    private CardsConfig _cardsConfig;
    private AccountModel _accountModel;

    [Inject]
    private void InjectDependencies(CardChooseGameModel cardChooseGameModel, CardsConfig config, AccountModel accountModel)
    {
        _cardChooseGameModel = cardChooseGameModel;
        _cardsConfig = config;
        _accountModel = accountModel;
    }

    private void OnEnable()
    {
        foreach (CardsInitializerHolder cardsInitializerHolder in _cards)
        {
            cardsInitializerHolder.Type = CardType.None;
            cardsInitializerHolder.Button.gameObject.GetComponent<Image>().DOFade(1, 0);
        }
        _cardChooseGameModel.OnIsCardSpinChanged += InitializeCards;
        _cardChooseGameModel.OnIsCardGameChanged += HideCards;
    }

    private void HideCards()
    {
        if (!_cardChooseGameModel.IsCardGame)
        {
            return;
        }
        foreach (CardsInitializerHolder cardsInitializerHolder in _cards)
        {
            cardsInitializerHolder.Button.gameObject.SetActive(true);
            cardsInitializerHolder.Button.gameObject.GetComponent<Image>().DOFade(1, 1);

        }
    }

    private void OnDisable()
    {
        _cardChooseGameModel.OnIsCardGameChanged -= HideCards;
        _cardChooseGameModel.OnIsCardSpinChanged -= InitializeCards;
        foreach (CardsInitializerHolder cardsInitializerHolder in _cards)
        {
            cardsInitializerHolder.Button.onClick.RemoveAllListeners();
        }
        _cardChooseGameModel.IsCardGame = false;
    }

    private void InitializeCards()
    {
        if (_cardChooseGameModel.IsCardSpin)
        {
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, _cards.Count);
        CardsInitializerHolder holder = _cards[randomIndex];
        holder.Image.sprite = GetCardByType(_cardChooseGameModel.CardType).Sprite;
        holder.Type = GetCardByType(_cardChooseGameModel.CardType).CardType;
        
        foreach(CardsInitializerHolder cardsInitializerHolder in _cards)
        {
            if(cardsInitializerHolder == holder)
            {
                continue;
            }
            int random = UnityEngine.Random.Range(0, _cardsConfig.Cards.Count);
            cardsInitializerHolder.Image.sprite = _cardsConfig.Cards[random].Sprite;
            cardsInitializerHolder.Type = _cardsConfig.Cards[random].CardType;
        }
        CheckWin();
    }

    private void CheckWin()
    {
        foreach (CardsInitializerHolder cardsInitializerHolder in _cards)
        {
            cardsInitializerHolder.Button.onClick.AddListener(() => CheckCards(cardsInitializerHolder));
        }
    }

    private void CheckCards(CardsInitializerHolder cardInitializerHolder)
    {
        if(cardInitializerHolder.Type == _cardChooseGameModel.CardType)
        {
            _accountModel.IncreaseVault(150);
        }
        else
        {
            _accountModel.DecreaseVault(50);
        }
        _cardChooseGameModel.CardType = CardType.None;
        foreach (CardsInitializerHolder cardsInitializerHolder in _cards)
        {
            cardsInitializerHolder.Button.onClick.RemoveAllListeners(); 
            cardsInitializerHolder.Button.gameObject.GetComponent<Image>().DOFade(0, 1);
            cardsInitializerHolder.Type = CardType.None;
        }
        _cardChooseGameModel.IsCardGame = false;
    }

    public CardHolder GetCardByType(CardType cardType)
    {
        foreach (CardHolder card in _cardsConfig.Cards)
        {
            if (card.CardType == cardType)
            {
                return card;
            }
        }
        return null; 
    }
}
