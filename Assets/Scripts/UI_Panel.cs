using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UI_Panel : MonoBehaviour
{
    private CanvasGroup _cg  = default;
    
    [SerializeField] private bool _isHiddenByDefault = false;

    private void Awake()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if(_isHiddenByDefault)
            Hide(0f);
    }

    public void Show(float fadeTime = 0.3f)
    {
        _cg
            .DOFade(1, fadeTime)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                _cg.blocksRaycasts = true;
            });
    }
    
    public void Hide(float fadeTime = 0.3f)
    {
        _cg
            .DOFade(0, fadeTime)
            .SetUpdate(true)
            .OnStart(() =>
            {
                _cg.blocksRaycasts = false;
            });
    }
}
