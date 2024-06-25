using System;
using System.Collections;
using UnityEngine;

public class SlotSpin : MonoBehaviour
{
    [SerializeField] private float _speedMax;
    [SerializeField] private float _speedMin;
    [SerializeField] private GameObject _diamond;
    [SerializeField] private GameObject _slotArea;
    [SerializeField] private float _speed;
    [SerializeField] private float _bringSpeed;

    private Vector3 _startSlotPosition;
    private bool _isSpinMove;
    private bool _isSpin;
    private bool _bringToWin;

    public event Action OnStopSpinMove;
    public event Action<SlotType> OnStopSpin;

    private void Awake()
    {
        _startSlotPosition = transform.localPosition;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _isSpin = false;
        _isSpinMove = false;
    }
    public void Spin()
    {
        if(_isSpinMove || _bringToWin)
        {
            return;
        }
        _isSpin = true;
        _speed = UnityEngine.Random.Range(_speedMax, _speedMin);
        StartCoroutine(SpeedDecrease());
    }

    private void FixedUpdate()
    {
        if(_speed > 0.3f && _isSpinMove)
        {
            if (_diamond.transform.position.y < _slotArea.transform.position.y)
            {
                MoveSlotOnDefaultPosition();
            }
            transform.Translate(Vector2.down * Time.deltaTime * _speed);
        }
    }

    public void BringToTheWinSlot(Slot winSlot, Vector3 endPoint)
    {
        _bringToWin = true;
        StartCoroutine(BringToTheWinSlotMove(winSlot, endPoint));
    }

    private IEnumerator BringToTheWinSlotMove(Slot winSlot, Vector3 endPoint)
    {
        while (Vector3.Distance(winSlot.GetGameObject().transform.position, endPoint) > 0.01)
        {
            float bringSpeed = _bringSpeed;
            if (winSlot.GetGameObject().transform.position.y < _slotArea.transform.position.y)
            {
                bringSpeed = -bringSpeed;
            }
            transform.Translate(Vector2.down * Time.deltaTime * bringSpeed);
            yield return null;
        }
        _bringToWin = false;
        _isSpin = false;
        OnStopSpin?.Invoke(winSlot.SlotType);
    }

    private IEnumerator SpeedDecrease()
    {
        _isSpinMove = true;
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.005f);
        while (_speed > 0.3f)
        {
            _speed = _speed / 1.04f;
            yield return waitForSeconds;
        }
        _isSpinMove = false;
        OnStopSpinMove?.Invoke();
    }

    private void MoveSlotOnDefaultPosition()
    {
        transform.localPosition = _startSlotPosition;
    }

}

