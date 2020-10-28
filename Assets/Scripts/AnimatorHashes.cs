using UnityEngine;

public static class AnimatorHashes
{
    public static readonly int Float_PlayerMovement = Animator.StringToHash("MoveAmount");
    //public static int Float_PlayerAttackType = Animator.StringToHash("AttackType");
  
    public static readonly int State_PlayerEquip = Animator.StringToHash("Equip");
    public static readonly int State_PlayerDisarm = Animator.StringToHash("Disarm");
    public static readonly int State_PlayerTaunt = Animator.StringToHash("Taunt");
    public static readonly int State_PlayerJump = Animator.StringToHash("Jump");
    
    public static readonly int State_PlayerAttack_0 = Animator.StringToHash("Attack_0");
    public static readonly int State_PlayerAttack_1 = Animator.StringToHash("Attack_1");
    public static readonly int State_PlayerAttack_2 = Animator.StringToHash("Attack_2");
    
    public static readonly int Bool_PlayerAction = Animator.StringToHash("InAction");
    public static readonly int Bool_PlayerRun = Animator.StringToHash("IsRunning");
    public static readonly int Bool_PlayerArmed = Animator.StringToHash("IsArmed");
}
