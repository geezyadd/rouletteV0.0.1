using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StatesController : MonoBehaviour
{
    [SerializeField] private Button _slotsButton;
    [SerializeField] private Button _slotsCardsButton;
    [SerializeField] private GameObject _slotCanvas;
    [SerializeField] private GameObject _cardCanvas;
    [SerializeField] private GameObject _spinButton;
    [SerializeField] private GameObject _rouleteCanvas;
    private SlotSpinModel _slotSpinModel;

    [Inject]
    private void InjectDependencies(SlotSpinModel slotSpinModel)
    {
        _slotSpinModel = slotSpinModel;
    }

    private void OnEnable()
    {
        _slotSpinModel.OnIsSpinChanged += DisableCardButton;
        _slotsButton.onClick.AddListener(SetSlotState);
        _slotsCardsButton.onClick.AddListener(SetCardState);
    }

    private void DisableCardButton()
    {
        if (!_slotSpinModel.IsSpin)
        {
            _slotsCardsButton.gameObject.SetActive(true);
        }
        else
        {
            _slotsCardsButton.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _slotSpinModel.OnIsSpinChanged -= DisableCardButton;
        _slotsButton.onClick.RemoveAllListeners();
    }

    private void SetSlotState()
    {
        _slotCanvas.SetActive(true);
        _cardCanvas.SetActive(false);
        _spinButton.SetActive(true);
    }

    private void SetCardState()
    {
        _rouleteCanvas.SetActive(false);
        _slotCanvas.SetActive(false);
        _spinButton.SetActive(false);
        _cardCanvas.SetActive(true);
    }
}
