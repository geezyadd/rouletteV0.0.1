using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour, ISlot
{
    [SerializeField] private SlotType _slotType;

    public SlotType SlotType =>
        _slotType;

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
