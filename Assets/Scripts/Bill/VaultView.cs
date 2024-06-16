using System;
using TMPro;
using UnityEngine;
using Zenject;

public class VaultView : MonoBehaviour
{
    [SerializeField] private TMP_Text _vaultText;
    private AccountModel _accountModel;

    [Inject]
    private void InjectDependencies(AccountModel accountModel)
    {
        _accountModel = accountModel;
    }

    private void OnEnable()
    {
        SetVaultText();
        _accountModel.OnVaultChanged += SetVaultText;
    }

    private void OnDisable()
    {
        _accountModel.OnVaultChanged -= SetVaultText;
    }
    private void SetVaultText()
    {
        _vaultText.SetText(_accountModel.Vault + "$");
    }

}
