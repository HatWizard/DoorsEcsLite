using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public bool IsMoved {get; private set;} = false;
    

    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private float _movingSpeed = 0.05f;
    [SerializeField] private bool _disableOnMoved;
    [SerializeField] private float _distance;
    private Vector3 _startPosition;
    private void Awake() 
    {
        _startPosition = transform.position;
    }

    public void SetDistance(float distance)
    {
        _distance = distance;
        UpdatePosition();
        IsMoved = _distance==1;
    }

    public void Move()
    {
        if(_distance<1)
        {
            _distance +=_movingSpeed;
            UpdatePosition(); 
        }
        else
        {
            _distance=1;
            IsMoved = true;
        }
    }

    public void MoveBackwards()
    {

        if(_distance>0)
        {
            _distance -=_movingSpeed;
            UpdatePosition();
        }
        else
        {
            _distance=0;
            IsMoved = false;
        }
        
    }

    private void UpdatePosition()
    {
        _distance = Mathf.Clamp(_distance, 0,1);
        transform.position = _startPosition + _targetPosition * _distance;
    }
}
