using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TooltipsController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _tooltipsTip = default;
    [SerializeField] private RectTransform _tooltipsPanel = default;
    
    private Vector3 _activePos;
    private Vector3 _hiddenPos;

    private Tweener _tipTweener;
    private Sequence _sequence;

    private void Start()
    {
        _hiddenPos = _tooltipsPanel.anchoredPosition;
        _activePos = new Vector3(_hiddenPos.x, _hiddenPos.y - 300f);
        _tipTweener = _tooltipsTip.DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _tooltipsPanel.DOAnchorPos(_activePos, 0.4f);
            /*
            _tipTweener.Pause();
            _sequence = DOTween.Sequence()
                .Append(_tooltipsTip.DOFade(0, 0.1f).OnComplete(() => { _tipTweener.Pause();}))
                .Join(_tooltipsPanel.DOAnchorPos(_activePos, 0.4f));
        */
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            _tooltipsPanel.DOAnchorPos(_hiddenPos, 0.4f);
            /*
            _tipTweener.Play();
            _sequence = DOTween.Sequence()
                .Append(_tooltipsTip.DOFade(1, 0.1f).OnComplete(() => { _tipTweener.Play();}))
                .Join(_tooltipsPanel.DOAnchorPos(_hiddenPos, 0.4f));
                */
        }
    }
}
