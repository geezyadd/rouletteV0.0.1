using System;

public class SlotSpinModel 
{
    private bool _isSpin;
    private bool _isMiniGameSpin;
    private bool _isMiniGame;
    private int _spinCounter;
    private int _spinsToMiniGame = 3;
    private bool _isRewarClaimed;

    public event Action OnIsSpinChanged;
    public event Action OnIsMiniGameSpinChanged;
    public event Action OnIsMiniGameChanged;
    public event Action OnSpinCounterChangedChanged;
    public event Action OnSpinsToMiniGameChanged;
    public event Action OnIsRewarClaimedChanged;

    public bool IsSpin
    {
        get =>
            _isSpin;
        set
        {
            _isSpin = value;
            OnIsSpinChanged?.Invoke();
        }
    }

    public bool IsMiniGameSpin
    {
        get =>
            _isMiniGameSpin;
        set
        {
            _isMiniGameSpin = value;
            OnIsMiniGameSpinChanged?.Invoke();
        }
    }
    public bool IsMiniGame
    {
        get =>
            _isMiniGame;
        set
        {
            _isMiniGame = value;
            OnIsMiniGameChanged?.Invoke();
        }
    }


    public int SpinCounter
    {
        get =>
            _spinCounter;
        set
        {
            _spinCounter = value;
            OnSpinCounterChangedChanged?.Invoke();
        }
    }

    public int SpinsToMiniGame
    {
        get =>
            _spinsToMiniGame;
        set
        {
            _spinsToMiniGame = value;
            OnSpinsToMiniGameChanged?.Invoke();
        }
    }

    public bool IsRewarCounted 
    {
        get =>
            _isRewarClaimed;
        set
        {
            _isRewarClaimed = value;
            OnIsRewarClaimedChanged?.Invoke();
        } 
    }
}
