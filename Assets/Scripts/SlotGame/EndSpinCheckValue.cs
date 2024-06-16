using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndSpinCheckValue : MonoBehaviour
{
    [SerializeField] private List<Slot> _slots;
    [SerializeField] private SlotSpin _slotSpin;

    private void OnEnable()
    {
        _slotSpin.OnStopSpinMove += StartCheck;
    }
    private void OnDisable()
    {
        _slotSpin.OnStopSpinMove -= StartCheck;
    }

    private void StartCheck()
    {
        Slot winSlot = null;
        foreach (Slot slot in _slots)
        {
            if (winSlot == null)
            {
                winSlot = slot;
                continue;
            }

            float currentDistanceToSlot = Vector3.Distance(winSlot.GetGameObject().transform.position, transform.position);
            float checkedDistanceToSlot = Vector3.Distance(slot.GetGameObject().transform.position, transform.position);
            bool isCloser = checkedDistanceToSlot < currentDistanceToSlot;

            if(isCloser)
            {
                winSlot = slot;
            }

        }
        _slotSpin.BringToTheWinSlot(winSlot, transform.position);
    }
}
