using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class UiController : MonoBehaviour
{
    [SerializeField] private ScreenUiPanel _screenUiPanel;
    [SerializeField] private MenuPanel _menuPanel;
    [SerializeField] private GameObject _finishPanel;

    public void Init()
    {
        _menuPanel.gameObject.SetActive(false);
        _screenUiPanel.gameObject.SetActive(true);
        _finishPanel.gameObject.SetActive(false);
    }

    public void OnPauseClicked()
    {
        Time.timeScale = 0;
        _menuPanel.gameObject.SetActive(true);
        _screenUiPanel.gameObject.SetActive(false);
    }

    public void OnContinueButtonClicked()
    {
        Time.timeScale = 1;
        _menuPanel.gameObject.SetActive(false);
        _screenUiPanel.gameObject.SetActive(true);
    }

    public void ShowFinishPanel()
    {
        _finishPanel.gameObject.SetActive(true);
        _finishPanel.GetComponent<Image>().DOFade(0, 0);
        _finishPanel.GetComponent<Image>().DOFade(1, ParamsController.Player.DELAY_FADE).OnComplete(() =>
        {
            _finishPanel.GetComponent<Image>().DOFade(0, ParamsController.Player.DELAY_FADE).OnComplete(() =>
            {
                _finishPanel.gameObject.SetActive(false);
            });
        });
        
    }
}