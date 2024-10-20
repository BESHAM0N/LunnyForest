using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
{
    [SerializeField] private float _fadeTime = 0.5f;
    [SerializeField] private float _fadeDelay;
    private float _timeElapsed;
    private SpriteRenderer _spriteRenderer;
    private GameObject _gameObject;
    private Color _startColor;

    private float _fadeDelayElapsed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed = 0f;
        _spriteRenderer = animator.GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
        _gameObject = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_fadeDelay > _fadeDelayElapsed)
        {
            _fadeDelayElapsed += Time.deltaTime;
        }
        else
        {
            _timeElapsed += Time.deltaTime;
            var newAlpha = _startColor.a * (1 - (_timeElapsed / _fadeTime));
            _spriteRenderer.color = new Color(_startColor.r, _startColor.g, _startColor.b, newAlpha);
            if (_timeElapsed > _fadeTime)
                Destroy(_gameObject);
        }
    }
}