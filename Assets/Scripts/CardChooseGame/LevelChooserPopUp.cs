using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelChooserPopUp : MonoBehaviour
{
    [SerializeField] private GameObject _popup;
    [SerializeField] private Button _openPopup;
    [SerializeField] private Button _closePopup;
    [SerializeField] private List<LevelButton> _levelButtons;
    private LevelService _levelService;
    private LevelModel _levelModel;

    [Inject]
    private void InjectDependencies(LevelService levelService, LevelModel levelModel)
    {
        _levelService = levelService;
        _levelModel = levelModel;
    }

    private void OnEnable()
    {
        _openPopup.onClick.AddListener(OpenPopup);
        _closePopup.onClick.AddListener(ClosePopup);
        foreach (LevelButton levelButton in _levelButtons)
        {
            Button button = levelButton.gameObject.GetComponent<Button>();
            if(levelButton.LevelButtonCount <= _levelModel.CurrentLevel)
                levelButton.gameObject.GetComponent<Button>().onClick.AddListener(() => GoToLevel(levelButton));
            else
            {
                button.interactable = false;
            }
        }
        _levelModel.OnCurrentLevelChanged += RecheckAllbuttonsLevel;
    }

    private void RecheckAllbuttonsLevel()
    {
        foreach (LevelButton levelButton in _levelButtons)
        {
            levelButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        foreach (LevelButton levelButton in _levelButtons)
        {
            Button button = levelButton.gameObject.GetComponent<Button>();
            if (levelButton.LevelButtonCount <= _levelModel.CurrentLevel)
            {
                button.interactable = true;
                levelButton.gameObject.GetComponent<Button>().onClick.AddListener(() => GoToLevel(levelButton));
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    private void OnDisable()
    {
        _levelModel.OnCurrentLevelChanged -= RecheckAllbuttonsLevel;
        _openPopup.onClick.RemoveAllListeners();
        _closePopup.onClick.RemoveAllListeners();
        foreach (LevelButton levelButton in _levelButtons)
        {
            levelButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
    private void OpenPopup()
    {
        _popup.SetActive(true);
    }

    private void ClosePopup()
    {
        _popup.SetActive(false); 
    }

    private void GoToLevel(LevelButton levelButton)
    {
        _levelService.SetLevel(levelButton.LevelButtonCount);
        _popup.SetActive(false);
    }
}
