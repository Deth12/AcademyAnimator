using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class UI_Button : MonoBehaviour, 
    IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _targetImage = default;
    
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _hoverColor = Color.white;
    [SerializeField] private Color _selectedColor = Color.white;

    [SerializeField] private float _colorTransitionTime = 0.2f;

    public UnityEvent OnClick;
    
    [Header("Editor Only")]
    [SerializeField] private bool _changeNormalColor = false;
    
    private void Reset()
    {
        _targetImage = GetComponent<Image>();
    }

    private void OnValidate()
    {
        if (_targetImage != null && _changeNormalColor)
            _targetImage.color = _normalColor;
    }

    private void Awake()
    {
        if (_targetImage == null)
            _targetImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer Up");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _targetImage.DOColor(_hoverColor, _colorTransitionTime).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _targetImage.DOColor(_normalColor, _colorTransitionTime).SetUpdate(true);
    }
}
