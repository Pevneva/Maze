using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScreenUiPanel : MonoBehaviour
{
    [SerializeField] private ShieldButton _shieldButton;
    [SerializeField] private GameObject _pauseButton;

    public event UnityAction ShieldButtonPressed;
    public event UnityAction ShieldButtonUnpressed;
    public event UnityAction PauseButtonClicked;
    
    private void Start()
    {
        _shieldButton.ShieldPressed += OnShieldPressed;
        _shieldButton.ShieldUnpressed += OnShieldUnpressed;
        _pauseButton.GetComponent<Button>().onClick.AddListener(OnPauseButtonClicked);
    }

    private void OnShieldPressed()
    {
        ShieldButtonPressed?.Invoke();
    }

    private void OnShieldUnpressed()
    {
        ShieldButtonUnpressed?.Invoke();
    }

    private void OnPauseButtonClicked()
    {
        PauseButtonClicked?.Invoke();
    }
}
