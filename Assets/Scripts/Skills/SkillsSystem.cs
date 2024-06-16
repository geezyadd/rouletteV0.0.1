using System;
using UnityEngine;
using Zenject;

public class SkillsSystem : IInitializable, IDisposable
{
    private SkillsButtonsHandler _skillsButtonsHandler;
    private MiniGameSlotModel _miniGameSlotModel;
    private AccountModel _accountModel;
    private SpinRewardConfig _spinRewardConfig;

    public SkillsSystem(SkillsButtonsHandler skillsButtonsHandler, MiniGameSlotModel miniGameSlotModel, AccountModel accountModel, SpinRewardConfig spinRewardConfig)
    {
        _skillsButtonsHandler = skillsButtonsHandler;
        _miniGameSlotModel = miniGameSlotModel;
        _accountModel = accountModel;
        _spinRewardConfig = spinRewardConfig;
    }

    public void Dispose()
    {
        _skillsButtonsHandler.OnFrozenSkill -= FrozenSkill;
        _skillsButtonsHandler.OnFireSkill -= FireSkill;
        _skillsButtonsHandler.OnMagicSkill -= MagicSkill;
    }

    public void Initialize()
    {
        _skillsButtonsHandler.OnFrozenSkill += FrozenSkill;
        _skillsButtonsHandler.OnFireSkill += FireSkill;
        _skillsButtonsHandler.OnMagicSkill += MagicSkill;
    }

    private void MagicSkill()
    {
        if (_accountModel.Vault - _spinRewardConfig.MagicCost < 0)
        {
            Debug.LogError("No moneeeeeey");
            return;
        }
        _skillsButtonsHandler.DisableButtons();
        _miniGameSlotModel.SkillType = SkillType.Magic;
        _accountModel.DecreaseVault(_spinRewardConfig.MagicCost);

    }

    private void FireSkill()
    {
        //if (_miniGameSlotModel.SkillType != SkillType.None)
        //{
        //    _skillsButtonsHandler.DisableButtons();
        //    return;
        //}
        if (_accountModel.Vault - _spinRewardConfig.FireCost < 0)
        {
            Debug.LogError("No moneeeeeey");
            return;
        }
        _skillsButtonsHandler.DisableButtons();
        _miniGameSlotModel.SkillType = SkillType.Fire;
        _accountModel.DecreaseVault(_spinRewardConfig.FireCost);
    }

    private void FrozenSkill()
    {
        if (_accountModel.Vault - _spinRewardConfig.FrozenCost < 0)
        {
            Debug.LogError("No moneeeeeey");
            return;
        }
        _skillsButtonsHandler.DisableButtons();
        _miniGameSlotModel.SkillType = SkillType.Frozen;
        _accountModel.DecreaseVault(_spinRewardConfig.FrozenCost);
    }

    
}
