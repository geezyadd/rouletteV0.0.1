using UnityEngine;

public class MiniGameSlot : MonoBehaviour
{
    [SerializeField] private MiniGameSlotType _type;

    public MiniGameSlotType Type =>
        _type;

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
