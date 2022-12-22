using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class UiController : MonoBehaviour
{
    [SerializeField] private ScreenUiPanel _screenUiPanel;
    [SerializeField] private MenuPanel _menuPanel;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private Player _player;

    public void Init()
    {
        _screenUiPanel.PauseButtonClicked += OnPauseClicked;
        _menuPanel.ContinueButtonClicked += OnContinueButtonClicked;
        _player.Won += OnWon;
        _menuPanel.gameObject.SetActive(false);
        _screenUiPanel.gameObject.SetActive(true);
        _finishPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _screenUiPanel.PauseButtonClicked -= OnPauseClicked;
        _menuPanel.ContinueButtonClicked -= OnContinueButtonClicked;
        _player.Won -= OnWon;
    }

    private void OnPauseClicked()
    {
        Time.timeScale = 0;
        _menuPanel.gameObject.SetActive(true);
        _screenUiPanel.gameObject.SetActive(false);
    }

    private void OnContinueButtonClicked()
    {
        Time.timeScale = 1;
        _menuPanel.gameObject.SetActive(false);
        _screenUiPanel.gameObject.SetActive(true);
    }

    private void OnWon()
    {
        ShowFinishPanel();
    }

    private void ShowFinishPanel()
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