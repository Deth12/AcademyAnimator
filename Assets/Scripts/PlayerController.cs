using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private PlayerAnimatorEvents _animEvents;

    [Header("States")] 
    private bool isMoving;
    private bool isRunning;
    private bool isCrouching;
    
    private bool _isArmed = true;
    public bool IsArmed
    {
        get => _isArmed;
        set
        {
            _isArmed = value;
            _animator.CrossFade
                (value ? AnimatorHashes.State_PlayerEquip : AnimatorHashes.State_PlayerDisarm, 0.2f);
        }
    }

    [Header("Weapon")]
    [SerializeField] private Transform _weapon = default;
    
    [SerializeField] private Transform _weaponHolder = default;
    [SerializeField] private Transform _holster = default;
    
    [Header("Positioning")]
    [SerializeField] private Vector3 _inHandPos = default;
    [SerializeField] private Vector3 _inHandRot = default;

    [SerializeField] private Vector3 _inHolsterPos = default;
    [SerializeField] private Vector3 _inHolsterRot = default;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animEvents = GetComponentInChildren<PlayerAnimatorEvents>();

        _animEvents.Init(this, _animator);
    }
    
    private void Update()
    {
        if (_animEvents.InAction)
            return;
        
        HandleInput();
        UpdateAnimatorStates();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IsArmed = !IsArmed;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger(AnimatorHashes.Trigger_PlayerJump);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            _animator.CrossFade
                (AnimatorHashes.State_PlayerTaunt, 0.2f);
        }
        
        // It is better to use blend trees and horizontal/vertical input to blend with correct directions
        // But for now, I hope this approach will be OK
        isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 ? true : false;
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isCrouching = Input.GetKey(KeyCode.LeftControl);
    }

    private void UpdateAnimatorStates()
    {
        _animator.SetBool(AnimatorHashes.Bool_PlayerMove, isMoving);
        _animator.SetBool(AnimatorHashes.Bool_PlayerRun, isRunning);
        _animator.SetBool(AnimatorHashes.Bool_PlayerCrouch, isCrouching);
    }

    public void AttachWeapon()
    {
        _weapon.parent = _weaponHolder;
        _weapon.localPosition = _inHandPos;
        _weapon.localRotation = Quaternion.Euler(_inHandRot);
    }

    public void DetachWeapon()
    {
        _weapon.parent = _holster;
        _weapon.localPosition = _inHolsterPos;
        _weapon.localRotation = Quaternion.Euler(_inHolsterRot);
    }
}
