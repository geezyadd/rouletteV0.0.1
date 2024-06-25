using System;

public class LevelModel 
{
    private int _currentLevel = 1;

    public event Action OnCurrentLevelChanged;
    public int CurrentLevel
    {
        get =>
            _currentLevel;
        set
        {
            _currentLevel = value;
            OnCurrentLevelChanged?.Invoke();
        }
    }
}
