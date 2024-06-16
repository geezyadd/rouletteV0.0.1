using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameSpin : MonoBehaviour
{
    [SerializeField] private float _maxInitialRotationSpeed = 540f;
    [SerializeField] private float _minInitialRotationSpeed = 400f;
    [SerializeField] private float _decelerationRate = 0.95f;

    [SerializeField] private float _speed;
    private bool _isSpin;

    public event Action OnStopSpin; 


    public void StartSpin()
    {
        _isSpin = true;
        _speed = UnityEngine.Random.Range(_maxInitialRotationSpeed, _minInitialRotationSpeed);
        StartCoroutine(SpeedDecrease());
    }

    private void Update()
    {
        if (_isSpin)
        {
            if (_speed > 2f)
            {
                transform.Rotate(0, 0, _speed * Time.deltaTime);

            }
            else
            {
                _speed = 0;
            }
        }
    }

    private IEnumerator SpeedDecrease()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
        while (_speed > 2f)
        {
            _speed = _speed / _decelerationRate;
            yield return waitForSeconds;
        }
        OnStopSpin?.Invoke();
        _isSpin = false;
    }
}
