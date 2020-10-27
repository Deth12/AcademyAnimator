using UnityEngine;

public static class AnimatorHashes
{
    public static int Float_PlayerMovement = Animator.StringToHash("MoveAmount");
    public static int Float_PlayerAttackType = Animator.StringToHash("AttackType");
  
    public static int State_PlayerEquip = Animator.StringToHash("Equip");
    public static int State_PlayerDisarm = Animator.StringToHash("Disarm");
    public static int State_PlayerTaunt = Animator.StringToHash("Taunt");
    public static int State_PlayerJump = Animator.StringToHash("Jump");
    public static int State_PlayerAttack = Animator.StringToHash("Attack");
    
    
    public static int Bool_PlayerAction = Animator.StringToHash("InAction");
    public static int Bool_PlayerRun = Animator.StringToHash("IsRunning");
}
