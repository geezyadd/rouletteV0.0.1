using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MiniGameStopSpinHandler : MonoBehaviour
{
    [SerializeField] private List<MiniGameSlot> _slots;
    [SerializeField] private MiniGameSpin _miniGameSpin;
    private SlotSpinModel _slotSpinModel;
    private MiniGameSlotModel _miniGameSlotModel;

    [Inject]
    private void InjectDependencies(SlotSpinModel slotSpinModel, MiniGameSlotModel miniGameSlotModel)
    {
        _slotSpinModel = slotSpinModel;
        _miniGameSlotModel = miniGameSlotModel;
    }

    private void OnEnable()
    {
        _miniGameSpin.OnStopSpin += StartCheck;
    }
    private void OnDisable()
    {
        _miniGameSpin.OnStopSpin -= StartCheck;
    }

    private void StartCheck()
    {
        MiniGameSlot winSlot = null;
        foreach (MiniGameSlot slot in _slots)
        {
            if (winSlot == null)
            {
                winSlot = slot;
                continue;
            }

            float currentDistanceToSlot = Vector3.Distance(winSlot.GetGameObject().transform.position, transform.position);
            float checkedDistanceToSlot = Vector3.Distance(slot.GetGameObject().transform.position, transform.position);
            bool isCloser = checkedDistanceToSlot < currentDistanceToSlot;

            if (isCloser)
            {
                winSlot = slot;
            }

        }
        _slotSpinModel.IsMiniGameSpin = false;
        _slotSpinModel.IsMiniGame = false;
        _miniGameSlotModel.MiniGameType = winSlot.Type;
        Debug.LogError(gameObject.name + " " + winSlot.GetGameObject().name);
    }
}
