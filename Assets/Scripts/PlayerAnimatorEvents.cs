using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    [HideInInspector] public bool InAction;
    
    private PlayerController _playerController;
    private Animator _animator;

    public void Init(PlayerController pc, Animator anim)
    {
        _playerController = pc;
        _animator = anim;
    }

    private void Update()
    {
        InAction = _animator.GetBool("InAction");
    }

    public void AttachWeapon()
    {
        Debug.Log("[Animation Event] Attach Weapon");
        _playerController.AttachWeapon();
    }

    public void DetachWeapon()
    {
        Debug.Log("[Animation Event] Detach Weapon");
        _playerController.DetachWeapon();
    }
}
