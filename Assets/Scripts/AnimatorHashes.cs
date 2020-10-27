using UnityEngine;

public static class AnimatorHashes
{
    public static int State_PlayerEquip = Animator.StringToHash("Equip");
    public static int State_PlayerDisarm = Animator.StringToHash("Disarm");
    public static int State_PlayerTaunt = Animator.StringToHash("Taunt");

    public static int Trigger_PlayerJump = Animator.StringToHash("Jump");
    
    public static int Bool_PlayerAction = Animator.StringToHash("InAction");
    public static int Bool_PlayerCrouch = Animator.StringToHash("IsCrouching");
    public static int Bool_PlayerMove = Animator.StringToHash("IsMoving");
    public static int Bool_PlayerRun = Animator.StringToHash("IsRunning");

}
