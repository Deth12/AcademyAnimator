using UnityEngine;

public class RunningBehavior : StateMachineBehaviour
{
    private PlayerController _playerController;

    [SerializeField] private float _speedAfterTimeIncrease = 1f;
    [SerializeField] private float _timeToApplyMultiplier = 3f;

    private float _timeElapsed = 0f;
    private bool _isBoostApplied = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerController == null)
            _playerController = animator.gameObject.GetComponentInParent<PlayerController>();
        
        _timeElapsed = 0;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_isBoostApplied)
            return;
        
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >= _timeToApplyMultiplier)
        {
            _playerController.AddRunSpeed(_speedAfterTimeIncrease);
            _isBoostApplied = true;
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isBoostApplied)
        {
            _playerController.AddRunSpeed(-_speedAfterTimeIncrease);
            _isBoostApplied = false;
        }
    }
}
