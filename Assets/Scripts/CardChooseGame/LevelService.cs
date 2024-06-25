using System.Collections.Generic;
using UnityEngine;

public class LevelService : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;
    private int _currentLevel;

    public void SetLevel(int levelCount)
    {
        foreach(Level level in _levels) 
        {
            if(level.LevelCount == levelCount)
            {
                level.gameObject.SetActive(true);
                continue;
            }

            level.gameObject.SetActive(false);
        }
    }
    
}
