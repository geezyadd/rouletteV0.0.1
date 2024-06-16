using UnityEngine;
using UnityEngine.UI;

public class SettingsSystem : MonoBehaviour
{
    [SerializeField] private Button _closeSettingsButton;

    private void OnEnable()
    {
        _closeSettingsButton.onClick.AddListener(DisableSettingsWindow);
    }

    private void OnDisable()
    {
        _closeSettingsButton.onClick.RemoveListener(DisableSettingsWindow);
    }


    private void DisableSettingsWindow()
    {
        gameObject.SetActive(false);
    }
}
