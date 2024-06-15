using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] private float _firstSliderStep;
    [SerializeField] private float _secondSliderStep;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private UnityEngine.UI.Slider _loadingSlider;
    [SerializeField] private UnityEngine.UI.Image _loadingArea;
    [SerializeField] private TMP_Text _gameNameText;
    [SerializeField] private TMP_Text _loadingText;

    private void Start()
    {
        StartCoroutine(ChangeSliderValue());
    }

    private IEnumerator ChangeSliderValue()
    {
        yield return StartCoroutine(ChangeValue(0, _firstSliderStep, _waitTime));

        yield return new WaitForSeconds(_waitTime);

        yield return StartCoroutine(ChangeValue(_firstSliderStep, _secondSliderStep, _waitTime));

        _gameNameText.DOFade(0, _fadeDuration);
        _loadingText.DOFade(0, _fadeDuration);
        _loadingSlider.gameObject.SetActive(false);

        yield return new WaitForSeconds(_fadeDuration);

        _loadingArea.gameObject.SetActive(false);
    }

    private IEnumerator ChangeValue(float startValue, float endValue, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _loadingSlider.value = Mathf.Lerp(startValue, endValue, elapsed / duration);
            yield return null;
        }
        _loadingSlider.value = endValue; 
    }
}
