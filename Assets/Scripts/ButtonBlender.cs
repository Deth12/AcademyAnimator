using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]
public class ButtonBlender : MonoBehaviour
{
    private readonly int _triggerClick = Animator.StringToHash("Clicked");
    private readonly int _floatColor = Animator.StringToHash("Color");
    private readonly int _floatRotation = Animator.StringToHash("Rotation");
    private readonly int _floatScale = Animator.StringToHash("Scale");
    private readonly int _floatPosition = Animator.StringToHash("Position");

    [SerializeField] private Button _button;
    [SerializeField] private Animator _animator;
    
    private void Reset()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(RandomBlend);
    }

    private void RandomBlend()
    {
        _animator.SetTrigger(_triggerClick);
        _animator.SetFloat(_floatColor, Random.Range(0f, 1f));
        _animator.SetFloat(_floatRotation, Random.Range(0f, 1f));
        _animator.SetFloat(_floatScale, Random.Range(0f, 1f));
        _animator.SetFloat(_floatPosition, Random.Range(0f, 1f));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(RandomBlend);
    }
}
