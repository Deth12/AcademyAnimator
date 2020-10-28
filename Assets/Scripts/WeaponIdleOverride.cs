using UnityEngine;

public class WeaponIdleOverride : MonoBehaviour
{
    private PlayerAnimatorEvents _animatorEvents;
    private Animator _animator;
    
    [SerializeField] private AnimationClip _unarmedIdleAnimation = default;
    [SerializeField] private AnimationClip _armedIdleAnimation = default;
    
    private AnimatorOverrideController _animatorOverrideController;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animatorEvents = GetComponentInChildren<PlayerAnimatorEvents>();
        _animatorEvents.OnWeaponEquip += ApplyArmedIdle;
        _animatorEvents.OnWeaponUnequip += ApplyUnarmedIdle;
    }

    public void Start()
    {
        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;
    }

    private void ApplyArmedIdle()
    {
        _animatorOverrideController["Standing Idle"] = _armedIdleAnimation;
    }

    private void ApplyUnarmedIdle()
    {
        _animatorOverrideController["Standing Idle"] = _unarmedIdleAnimation;
    }
}
