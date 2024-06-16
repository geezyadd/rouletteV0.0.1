using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpinStarter : MonoBehaviour
{
    [SerializeField] private List<SlotSpin> _slots = new List<SlotSpin>();
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
        _gameStateButtonsHandler.OnSpinButtonClicked -= SpinSlots;
    }

    private void SpinSlots()
    {
        if (!_slotSpinModel.IsSpin && !_slotSpinModel.IsMiniGameSpin && _slotSpinModel.IsMiniGame)
        {
            _slotSpinModel.IsMiniGameSpin = true;
            _miniGameSpin.StartSpin();
        }

        if (!_slotSpinModel.IsSpin && !_slotSpinModel.IsMiniGameSpin)
        {
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
