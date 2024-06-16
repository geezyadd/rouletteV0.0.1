using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SkillsButtonsHandler : MonoBehaviour
{
    [SerializeField] private Button _frozenSkill;
    [SerializeField] private Button _fireSkill;
    [SerializeField] private Button _magicSkill;
    private SlotSpinModel _slotSpinModel;

    public event Action OnFrozenSkill;
    public event Action OnFireSkill;
    public event Action OnMagicSkill;

    [Inject]
    private void InjectDependencies(SlotSpinModel slotSpinModel)
    {
        _slotSpinModel = slotSpinModel;
    }

    public void DisableButtons()
    {
        _frozenSkill.interactable = false;
        _fireSkill.interactable = false;
        _magicSkill.interactable = false;
    }

    public void EnableButtons()
    {
        _frozenSkill.interactable = true;
        _fireSkill.interactable = true;
        _magicSkill.interactable = true;
    }

    private void OnEnable()
    {
        _slotSpinModel.OnIsRewarClaimedChanged += ButtonsDisabler;
        _frozenSkill.onClick.AddListener(FrozenSkill);
        _fireSkill.onClick.AddListener(FireSkill);
        _magicSkill.onClick.AddListener(MagicSkill);
    }

    private void OnDisable()
    {
        _slotSpinModel.OnIsRewarClaimedChanged += ButtonsDisabler;
        _frozenSkill.onClick.RemoveListener(FrozenSkill);
        _fireSkill.onClick.RemoveListener(FireSkill);
        _magicSkill.onClick.RemoveListener(MagicSkill);
    }

    private void FrozenSkill()
    {
        OnFrozenSkill?.Invoke();
    }

    private void FireSkill()
    {
        OnFireSkill?.Invoke();
    }

    private void MagicSkill()
    {
        OnMagicSkill?.Invoke();
    }

    private void ButtonsDisabler()
    {
        if (!_slotSpinModel.IsRewarCounted)
        {
            DisableButtons();
        }
        else
        {
            EnableButtons();
        }
    }
}
