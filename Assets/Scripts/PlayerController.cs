using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _animator;
    private PlayerAnimatorEvents _animEvents;
    private PlayerCamera _playerCamera;

    [SerializeField] private  Vector3 targetDirection = Vector3.zero;
    [SerializeField] private  float moveSpeed = 5f;
    [SerializeField] private  float runSpeed = 8f;
    [SerializeField] private float rotateSpeed = 8f;

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
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _animEvents = GetComponentInChildren<PlayerAnimatorEvents>();
        _animEvents.Init(this, _animator);
        _playerCamera = Camera.main.GetComponentInParent<PlayerCamera>();
    }
    
    private void Update()
    {
        HandleInput();
        HandleMovement();
    }
    
    private void HandleMovement()
    {
        float targetSpeed = _animEvents.IsRunning ? runSpeed : moveSpeed;
        Vector3 v = _animEvents.Vertical * _playerCamera.transform.forward;
        Vector3 h = _animEvents.Horizontal * _playerCamera.transform.right;
        v.y = 0;
        h.y = 0;
        targetDirection = (v + h).normalized;
        Vector3 movement = targetDirection * (targetSpeed * _animEvents.MoveAmount) * Time.deltaTime;
        Debug.DrawRay(transform.position, targetDirection, Color.red, Time.deltaTime);
        _controller.Move(movement);
        
        Vector3 moveDirection = targetDirection;
        moveDirection.y = 0;
        if(moveDirection == Vector3.zero)
            moveDirection = transform.forward;
        
        HandleRotation(moveDirection);
    }

    private void HandleRotation(Vector3 moveDirection)
    {
        Quaternion tr = Quaternion.LookRotation(moveDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * rotateSpeed); // faster
        transform.rotation = targetRotation;
    }

    private void HandleInput()
    {
        _animEvents.Vertical = Input.GetAxis("Vertical");
        _animEvents.Horizontal = Input.GetAxis("Horizontal");
        
        // It is better to use blend trees and horizontal/vertical input to blend with correct directions
        // But for now, I hope this approach will be OK
        _animEvents.IsRunning = Input.GetKey(KeyCode.LeftShift);
        
        if (_animEvents.InAction)
            return;
        
        if (Input.GetKeyDown(KeyCode.Q))
            _animator.CrossFade(_animEvents.GetNextArmedState(), .2f);

        if (Input.GetKeyDown(KeyCode.Space))
            _animator.CrossFade(AnimatorHashes.State_PlayerJump, .2f);
        
        if (Input.GetKeyDown(KeyCode.T))
            _animator.CrossFade(AnimatorHashes.State_PlayerTaunt, 0.2f);

        if (!_animEvents.IsArmed)
            return;
        
        if (Input.GetMouseButtonDown(0) && _animEvents.IsAttackAvailiable)
        {
            _animator.CrossFade(_animEvents.GetNextAttack(), .2f);
        }
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

    public void AddRunSpeed(float value)
    {
        Debug.Log($"Applying run boost -> Default:{runSpeed} Target:{runSpeed + value}");
        runSpeed += value;
    }
}
