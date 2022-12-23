using Sirenix.OdinInspector;
using UnityEngine;

public class Mediator : MonoBehaviour
{
    [Required] public UiController UiController;
    [Required] public Player Player;

    [Button, DisableInEditorMode] public void OnClickContinueButton() => UiController.OnContinueButtonClicked();
    [Button, DisableInEditorMode] public void OnClickPauseButton() => UiController.OnPauseClicked();
    [Button, DisableInEditorMode] public void OnClickExitButton() => Application.Quit();
    [Button, DisableInEditorMode] public void OnPressShieldButton() => Player.SetProtectedState();
    [Button, DisableInEditorMode] public void OnUnpressShieldButton() => Player.SetUsualState();
}