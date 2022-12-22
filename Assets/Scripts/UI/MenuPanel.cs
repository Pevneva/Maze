using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;

    public event UnityAction ContinueButtonClicked;

    private string _menuPanelId; 
    private string _exitButtonId;
    private string _continueButtonId;
    
    private void Start()
    {
        _continueButton.onClick.AddListener(OnContinueClicked);
        _exitButton.onClick.AddListener(OnExitClicked);

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

    private void OnContinueClicked()
    {
        ContinueButtonClicked?.Invoke();
    }

    private void OnExitClicked()
    {
        Application.Quit();
    }
}