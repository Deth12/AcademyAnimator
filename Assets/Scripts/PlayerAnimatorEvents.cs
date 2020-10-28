using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    private PlayerController _playerController;
    private Animator _animator;
    
    public bool InAction
    {
        get => _animator.GetBool(AnimatorHashes.Bool_PlayerAction);
        private set => _animator.SetBool(AnimatorHashes.Bool_PlayerAction, value);
    }
    
    public bool IsArmed
    {
        get => _animator.GetBool(AnimatorHashes.Bool_PlayerArmed);
        private set => _animator.SetBool(AnimatorHashes.Bool_PlayerArmed, value);
    }

    public System.Action OnWeaponEquip;
    public System.Action OnWeaponUnequip;
    
    public bool IsRunning;
    public bool IsAttackAvailiable;

    public float Vertical = 0f;
    public float Horizontal = 0f;

    private int _comboIndex = 0;
    
    public float MoveAmount => Mathf.Clamp01(Mathf.Abs(Horizontal) + Mathf.Abs(Vertical));
    
    public void Init(PlayerController pc, Animator anim)
    {
        _playerController = pc;
        _animator = anim;
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

    public int GetNextAttack()
    {
        int attackHash;
        switch (_comboIndex)
        {
            case 0:
                attackHash = AnimatorHashes.State_PlayerAttack_0;
                break;
            case 1:
                attackHash = AnimatorHashes.State_PlayerAttack_1;
                break;
            case 2:
                attackHash = AnimatorHashes.State_PlayerAttack_2;
                break;
            default:
                return -1;
        }
        _comboIndex = _comboIndex == 2 ? 0 : _comboIndex + 1;
        IsAttackAvailiable = false;
        return attackHash;
    }

    public int GetNextArmedState()
    {
        return IsArmed ? AnimatorHashes.State_PlayerDisarm : AnimatorHashes.State_PlayerEquip;
    }

    public void OpenForCombo()
    {
        IsAttackAvailiable = true;
    }
    
    public void AttachWeapon()
    {
        _playerController.AttachWeapon();
        IsArmed = true;
        IsAttackAvailiable = true;
        OnWeaponEquip?.Invoke();
    }

    public void DetachWeapon()
    {
        _playerController.DetachWeapon();
        IsArmed = false;
        OnWeaponUnequip?.Invoke();
    }
}
