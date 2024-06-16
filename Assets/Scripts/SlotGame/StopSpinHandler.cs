using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StopSpinHandler : MonoBehaviour
{
    [SerializeField] private List<SlotSpin> _spinSlots;
    private List<SlotType> _slots = new();
    private int _slotsCount = 3;
    private SlotSpinModel _slotSpinModel;
    private ISlotWinCheckerService _slotWinCheckerService;

    [Inject]
    private void InjectDEpendencies(SlotSpinModel slotSpinModel, ISlotWinCheckerService slotWinCheckerService)
    {
        _slotSpinModel = slotSpinModel;
        _slotWinCheckerService = slotWinCheckerService;
    }

    private void OnEnable()
    {
        foreach (SlotSpin spin in _spinSlots)
        {
            spin.OnStopSpin += HandleStopSpin;
        }
    }

    private void OnDisable()
    {
        foreach (SlotSpin spin in _spinSlots)
        {
            spin.OnStopSpin -= HandleStopSpin;
        }
    }

    private void HandleStopSpin(SlotType slotType)
    {
        _slots.Add(slotType);
        if(_slots.Count != _slotsCount)
        {
            return;
        }
        if (_slots.Count == _slotsCount)
        {
            _slotSpinModel.SpinCounter++;
            _slotWinCheckerService.CheckSlots(_slots);
        }
        CrearSlots();
        SpinCounterChecker();
        _slotSpinModel.IsSpin = false;
    }

    private void CrearSlots()
    {
        _slots.Clear();
    }

    private void SpinCounterChecker()
    {
        if(_slotSpinModel.SpinCounter == _slotSpinModel.SpinsToMiniGame)
        {
            _slotSpinModel.IsMiniGame = true;
            _slotSpinModel.SpinCounter = 0;
        }
    }
}
