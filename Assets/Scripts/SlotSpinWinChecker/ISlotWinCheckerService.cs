using System;
using System.Collections.Generic;

public interface ISlotWinCheckerService
{
    public event Action<SlotType, int> OnWin;
    public event Action OnLoose;
    public void CheckSlots(List<SlotType> slots);
}