using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SpinRewardConfig), menuName = "Reward/" + nameof(SpinRewardConfig), order = 1)]
public class SpinRewardConfig : ScriptableObject
{
    [SerializeField] private int _oneSpinCost;
    [SerializeField] private int _frozenCost;
    [SerializeField] private int _magicCost;
    [SerializeField] private int _fireCost;
    [SerializeField] private List<SpinRewardHolder> _rewardConfig;

    public List<SpinRewardHolder> RewardConfig =>
        _rewardConfig;
    public int OneSpinCost => 
        _oneSpinCost;
    public int FrozenCost =>
        _frozenCost;
    public int MagicCost =>
        _magicCost;
    public int FireCost =>
        _fireCost;
}

[Serializable]
public class SpinRewardHolder
{
    public SlotType SlotType;
    public int Count;
    public int Reward;
}
