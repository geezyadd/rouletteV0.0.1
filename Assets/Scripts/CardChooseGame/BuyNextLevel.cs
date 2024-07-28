using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuyNextLevel : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    private LevelModel _levelModel;
    private AccountModel _accountModel;

    [Inject]
    private void InjectDependencies(LevelModel model, AccountModel accountModel)
    {
        _levelModel = model;
        _accountModel = accountModel;
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(BuyLevel);
        _levelModel.OnCurrentLevelChanged += DisableBuyButton;
    }


    private void OnDisable()
    {
        _levelModel.OnCurrentLevelChanged -= DisableBuyButton;
        _buyButton.onClick.RemoveAllListeners();
    }
    private void DisableBuyButton()
    {
        if (_levelModel.CurrentLevel == 10)
        {
            _buyButton.interactable = false;
        }
    }

    private void BuyLevel()
    {
        if(_levelModel.CurrentLevel == 10)
        {
            return;
        }
        if(_accountModel.Vault - 100 < 0)
        {
            Debug.LogError("no money");
            return;
        }

        _levelModel.CurrentLevel++;

        _accountModel.DecreaseVault(100);
    }

}
