using UnityEngine;

public class KeepBool : StateMachineBehaviour
{
    public string boolName;
    public bool boolStatus;

    /*
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(boolName, boolStatus);  
    }
    */
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(boolName, boolStatus);    
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(boolName, !boolStatus);  
    }
}
