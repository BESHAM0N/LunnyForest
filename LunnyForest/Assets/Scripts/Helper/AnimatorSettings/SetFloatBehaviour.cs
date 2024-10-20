using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    [SerializeField] private string _floatName;

    [SerializeField] private bool _updateOnStateEnter, _updateOnStateExit;

    [SerializeField] private bool _updateOnStateMachineEnter, _updateOnStateMachineExit;

    [SerializeField] private float _valueOnEnter, _valueOnExit;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_updateOnStateEnter)
        {
            animator.SetFloat(_floatName, _valueOnEnter);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_updateOnStateExit)
        {
            animator.SetFloat(_floatName, _valueOnExit);
        }
    }
    
    override public void OnStateMachineEnter(Animator animator,  int stateMachinePathHash)
    {
        if(_updateOnStateMachineEnter)
            animator.SetFloat(_floatName, _valueOnEnter);
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateMachineExit(Animator animator,  int stateMachinePathHash)
    {
       if(_updateOnStateMachineExit)
           animator.SetFloat(_floatName, _valueOnExit);
    }
}
