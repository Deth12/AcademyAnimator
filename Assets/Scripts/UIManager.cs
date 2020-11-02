using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

enum PauseScreen
{
    None,
    Main,
    Settings
}

public class UIManager : MonoBehaviour
{
    [Header("Pause")]
    public bool IsPauseActive;
    
    private PauseScreen _currentPauseScreen = PauseScreen.None;
    
    [SerializeField] private CanvasGroup _pauseScreen = default;

    [SerializeField] private UI_Button _resumeButton = default;
    [SerializeField] private UI_Button _settingsButton = default;
    [SerializeField] private UI_Button _exitButton = default;
    [SerializeField] private UI_Button _backButton = default;

    [SerializeField] private UI_Panel _pauseMenu = default;
    [SerializeField] private UI_Panel _settingsMenu = default;

    [Header("Global")]
    [SerializeField] private float _panelsFadeTime = 0.3f;

    private Tweener _activeTweener = null;
    
    private void Awake()
    {
        _resumeButton.OnClick.AddListener(UnpauseCallback);
        _settingsButton.OnClick.AddListener(SettingsCallback);
        _exitButton.OnClick.AddListener(ExitCallback);
        _backButton.OnClick.AddListener(BackToMain);
    }

    private void Start()
    {
        _pauseScreen.alpha = 0;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TriggerPause();
        }
    }

    private void TriggerPause()
    {
        if (IsPauseActive == false)
        {
            PauseCallback();
            return;
        }
        switch (_currentPauseScreen)
        {
            case PauseScreen.Main:
                UnpauseCallback();
                break;
            case PauseScreen.Settings:
                BackToMain();
                break;
            default:
                UnpauseCallback();
                break;
        }
    }

    private void PauseCallback()
    {
        _activeTweener = _pauseScreen
            .DOFade(1, _panelsFadeTime)
            .SetUpdate(true)
            .OnStart(() =>
            {
                _pauseScreen.blocksRaycasts = true;
                // TOREMOVE
                Time.timeScale = 0f;
                GlobalEvents.OnGamePause?.Invoke();
            })
            .OnComplete(() =>
            {
                IsPauseActive = true;
                _currentPauseScreen = PauseScreen.Main;
            });
    }

    private void UnpauseCallback()
    {
        _activeTweener = _pauseScreen
            .DOFade(0, _panelsFadeTime)
            .SetUpdate(true)
            .OnStart(() =>
            {
                _pauseScreen.blocksRaycasts = false;
                // TOREMOVE
                Time.timeScale = 1f;
                GlobalEvents.OnGameUnpause?.Invoke();
            })
            .OnComplete(() =>
            {
                IsPauseActive = false;
                _currentPauseScreen = PauseScreen.None;
            });
    }

    private void SettingsCallback()
    {
        _pauseMenu.Hide(_panelsFadeTime);
        _settingsMenu.Show(_panelsFadeTime);
        _currentPauseScreen = PauseScreen.Settings;
        _backButton.gameObject.SetActive(true);
    }

    private void BackToMain()
    {
        _settingsMenu.Hide(_panelsFadeTime);
        _pauseMenu.Show(_panelsFadeTime);
        _currentPauseScreen = PauseScreen.Main;
        // TODO: Add own method for hide/show
        _backButton.gameObject.SetActive(false);
    }

    private void ExitCallback()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); 
        #endif
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
