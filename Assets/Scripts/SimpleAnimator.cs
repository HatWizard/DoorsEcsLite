using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAnimator : MonoBehaviour
{
    [SerializeField] private Rigidbody _rididBody;
    [SerializeField] private Animator _animator;

    private void Update() 
    {
        var speed = _rididBody.velocity.magnitude;
        print(speed);
        _animator.SetFloat("MoveSpeed", speed);
    }
}
