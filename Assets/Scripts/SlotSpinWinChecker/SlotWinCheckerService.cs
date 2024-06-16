using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SlotWinCheckerService : ISlotWinCheckerService
{
    public event Action<SlotType, int> OnWin;
    public event Action OnLoose;

    public void CheckSlots(List<SlotType> slots)
    {
        Dictionary<SlotType, int> slotCounts = new();

        foreach (SlotType slot in slots)
        {
            if (slotCounts.ContainsKey(slot))
            {
                slotCounts[slot]++;
            }
            else
            {
                slotCounts[slot] = 1;
            }
        }

        foreach (KeyValuePair<SlotType, int> pair in slotCounts)
        {
            if (pair.Value == 3)
            {
                OnWin?.Invoke(pair.Key, 3);
                return;
            }
            else if (pair.Value == 2)
            {
                OnWin?.Invoke(pair.Key, 2);
                return;
            }
        }

        OnLoose?.Invoke();
    }
}
