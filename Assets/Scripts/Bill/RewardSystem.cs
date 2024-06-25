using Zenject;
using System;
using UnityEngine;

public class RewardSystem : IInitializable, IDisposable
{
    private SpinRewardConfig _spinRewardConfig;
    private ISlotWinCheckerService _slotWinCheckerService;
    private SlotSpinModel _slotSpinModel;
    private MiniGameSlotModel _miniGameSlotModel;
    private AccountModel _accountModel;
    private ParticleEffectsService _particleEffectsService;

    public RewardSystem(SpinRewardConfig spinRewardConfig, ISlotWinCheckerService slotWinCheckerService,
        SlotSpinModel slotSpinModel, MiniGameSlotModel miniGameSlotModel, AccountModel accountModel, ParticleEffectsService particleEffectsService) 
    {
        _spinRewardConfig = spinRewardConfig;
        _slotWinCheckerService = slotWinCheckerService;
        _slotSpinModel = slotSpinModel;
        _miniGameSlotModel = miniGameSlotModel;
        _accountModel = accountModel;
        _particleEffectsService = particleEffectsService;
    }
    public void Initialize()
    {
        _slotWinCheckerService.OnWin += ClaimReward;
        _slotWinCheckerService.OnLoose += OnLoose;
        _slotSpinModel.OnIsSpinChanged += DecreaseSpinCost;
    }

    public void Dispose() 
    {
        _slotWinCheckerService.OnLoose -= OnLoose;
        _slotWinCheckerService.OnWin -= ClaimReward;
        _slotSpinModel.OnIsSpinChanged -= DecreaseSpinCost;
    }
    private void ClaimReward(SlotType type, int count)
    {
        foreach(SpinRewardHolder rewardHolder in _spinRewardConfig.RewardConfig)
        {
            if(rewardHolder.SlotType == type && rewardHolder.Count == count)
            {
                _particleEffectsService.SpawnParticle(type);
                _accountModel.IncreaseVault(MiniGameReward(rewardHolder.Reward));
                _slotSpinModel.IsRewarCounted = true;
            }
        }
    }

    private void OnLoose()
    {
        _miniGameSlotModel.MiniGameType = MiniGameSlotType.None;
        _miniGameSlotModel.SkillType = SkillType.None;
        _slotSpinModel.IsRewarCounted = true;
    }

    private void DecreaseSpinCost()
    {
        if(_miniGameSlotModel.MiniGameType == MiniGameSlotType.FreeSpin)
        {
            _miniGameSlotModel.MiniGameType = MiniGameSlotType.None;
            return;
        }

        if (_slotSpinModel.IsSpin)
        {
            _accountModel.DecreaseVault(_spinRewardConfig.OneSpinCost);
        }
    }

    private int MiniGameReward(int reward)
    {
        int countReward = SkillReward(reward);
        if(_miniGameSlotModel.MiniGameType == MiniGameSlotType.X2)
        {
            _miniGameSlotModel.MiniGameType = MiniGameSlotType.None;
            return countReward * 2;
        }
        if(_miniGameSlotModel.MiniGameType == MiniGameSlotType.X10)
        {
            _miniGameSlotModel.MiniGameType = MiniGameSlotType.None;
            return countReward * 10;
        }
        if(_miniGameSlotModel.MiniGameType == MiniGameSlotType.Plus150)
        {
            _miniGameSlotModel.MiniGameType = MiniGameSlotType.None;
            return countReward + 150;
        }

        return countReward;
    }

    private int SkillReward(int reward)
    {
        bool isSkill = ShouldActivateSkill();
        if (_miniGameSlotModel.SkillType == SkillType.Frozen && isSkill) 
        {
            _miniGameSlotModel.SkillType = SkillType.None;
            return reward * 2;
        }

        if (_miniGameSlotModel.SkillType == SkillType.Magic && isSkill)
        {
            _miniGameSlotModel.SkillType = SkillType.None;
            return reward * 5;
        }

        if (_miniGameSlotModel.SkillType == SkillType.Fire && isSkill)
        {
            _miniGameSlotModel.SkillType = SkillType.None;
            return reward * 10;
        }

        return reward;
    }

    public bool ShouldActivateSkill()
    {
        return UnityEngine.Random.Range(0f, 1f) < 0.5f;
    }


}
