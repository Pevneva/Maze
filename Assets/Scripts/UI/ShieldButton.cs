using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShieldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private readonly float _unpressedTime = 2;
    private Coroutine _pressedTimer;

    public event UnityAction ShieldPressed;
    public event UnityAction ShieldUnpressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        ShieldPressed?.Invoke();
        _pressedTimer = StartCoroutine(StartHoldTimer());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UnpressButton();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnpressButton();
    }


    private IEnumerator StartHoldTimer()
    {
        yield return new WaitForSeconds(_unpressedTime);
        UnpressButton();
    }

    private void UnpressButton()
    {
        ShieldUnpressed?.Invoke();
        if (_pressedTimer != null)
            StopCoroutine(_pressedTimer);
    }
}