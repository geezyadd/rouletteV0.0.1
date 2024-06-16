using System;

public class AccountModel 
{
    private int _vault = 1000;
    public event Action OnVaultChanged;
    public int Vault
    {
        get =>
            _vault;
        set
        {
            _vault = value;
            OnVaultChanged?.Invoke();
        }
    }

    public void IncreaseVault(int amount)
    {
        Vault += amount;
    }

    public void DecreaseVault(int amount)
    {
        Vault -= amount;
    }
}
