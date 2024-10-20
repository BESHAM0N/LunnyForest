using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayOneShotBehavior : StateMachineBehaviour
{
    [SerializeField] private AudioClip _soundToPlay;
    [SerializeField] private float _volume = 1f;
    [SerializeField] private bool _playOnEnter = true;
    [SerializeField] private bool _playOnExit;
    [SerializeField] private bool _playAfterDelay;
    [SerializeField] private float _playDelay = 0.25f;
    private float _timeSinceEntered;
    private bool _hasDelayedSoundPlayed;
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playOnEnter)
        {
            AudioSource.PlayClipAtPoint(_soundToPlay, animator.gameObject.transform.position, _volume);
        }

        _timeSinceEntered = 0;
        _hasDelayedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playAfterDelay && !_hasDelayedSoundPlayed)
        {
            _timeSinceEntered += Time.deltaTime;
            if (_timeSinceEntered > _playDelay)
            {
                AudioSource.PlayClipAtPoint(_soundToPlay, animator.gameObject.transform.position, _volume);
                _hasDelayedSoundPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playOnExit)
        {
            AudioSource.PlayClipAtPoint(_soundToPlay, animator.gameObject.transform.position, _volume);
        }
    }
}
