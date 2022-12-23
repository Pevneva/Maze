using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Mediator _mediator;
    
    private string _menuPanelId; 
    private string _exitButtonId;
    private string _continueButtonId;
    
    private void Start()
    {
        _continueButton.onClick.AddListener(_mediator.OnClickContinueButton);
        _exitButton.onClick.AddListener(_mediator.OnClickExitButton);

        InitializeLocalization();
        SetLocalizationText();
    }

    private void InitializeLocalization()
    {
        _menuPanelId = ParamsController.Localization.MENU_PANEL_ID;
        _exitButtonId = ParamsController.Localization.EXIT_BUTTON_ID;
        _continueButtonId = ParamsController.Localization.CONTINUE_BUTTON_ID;
    }

    private void SetLocalizationText()
    {
        _exitButton.GetComponentInChildren<Text>().text = LanguageManager.Text(_menuPanelId, _exitButtonId);
        _continueButton.GetComponentInChildren<Text>().text = LanguageManager.Text(_menuPanelId, _continueButtonId);
    }
}