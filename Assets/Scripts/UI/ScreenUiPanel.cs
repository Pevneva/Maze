using UnityEngine;
using UnityEngine.UI;

public class ScreenUiPanel : MonoBehaviour
{
    [SerializeField] private ShieldButton _shieldButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Mediator _mediator;

    private void Start()
    {
        _shieldButton.ShieldPressed += _mediator.OnPressShieldButton;
        _shieldButton.ShieldUnpressed += _mediator.OnUnpressShieldButton;
        _pauseButton.onClick.AddListener(_mediator.OnClickPauseButton);
    }
}
