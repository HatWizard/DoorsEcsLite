using System.Collections;
using System.Collections.Generic;
using DoorsEcsLeo.Server;
using UnityEngine;
using Zenject;

namespace DoorsEcsLeo.Client
{
    public class LevelButton : MonoBehaviour
    {
        [Inject] private SceneVObjectsTable _sceneVObjectsTable {get; set;}
        [field: SerializeField] private VObject _activateVObject;
        [field: SerializeField] private Mover Mover {get; set;}
        [field: SerializeField] private float _delayBeetweenPressed;
        private bool _isPressed = false;

        private void OnTriggerEnter(Collider other) 
        {
            var vObject = other.attachedRigidbody.GetComponent<VObject>();
            if(vObject!=null && vObject.IsPlayer)
            {
                _isPressed = true;
            }    
        }

        private void OnTriggerExit(Collider other) 
        {
            if(other.attachedRigidbody!=null) _isPressed = false;
        }

        private void Update() 
        {
            if(!_isPressed)
            {
                Mover.MoveBackwards();
            }
            else
            {
                Mover.Move();
                Activate();
            }
        }

        private void Activate()
        {
            ref var target = ref _sceneVObjectsTable.GenerateEventEntity<VObjectInteractionComponent>();
            target.SceneId = _activateVObject.SceneId;
        }
    }
}
