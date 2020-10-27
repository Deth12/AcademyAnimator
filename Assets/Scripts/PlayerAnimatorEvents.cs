using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    [HideInInspector] public bool InAction;
    [HideInInspector] public bool IsRunning;

    [HideInInspector] public float Vertical = 0f;
    [HideInInspector] public float Horizontal = 0f;

    private int _attackIndex = 0;
    
    public float MoveAmount => Mathf.Clamp01(Mathf.Abs(Horizontal) + Mathf.Abs(Vertical));

    private PlayerController _playerController;
    private Animator _animator;

    public void Init(PlayerController pc, Animator anim)
    {
        _playerController = pc;
        _animator = anim;
        pc.OnAttack += NextAttackType;
    }

    private void Update()
    {
        InAction = _animator.GetBool(AnimatorHashes.Bool_PlayerAction);
        UpdateAnimatorStates();
    }

    private void UpdateAnimatorStates()
    {
        _animator.SetFloat(AnimatorHashes.Float_PlayerMovement, MoveAmount);
        _animator.SetBool(AnimatorHashes.Bool_PlayerRun, IsRunning);
    }

    private void NextAttackType()
    {
        _animator.SetFloat(AnimatorHashes.Float_PlayerAttackType, _attackIndex);
        _attackIndex = _attackIndex > 1 ? 0 : _attackIndex + 1;
    }
    
    public void AttachWeapon()
    {
        _playerController.AttachWeapon();
    }

    public void DetachWeapon()
    {
        _playerController.DetachWeapon();
    }
}
