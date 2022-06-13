using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoorsEcsLeo
{
    [RequireComponent(typeof(Mover))]
    public class InteractableVObject : VObject
    {
        private Mover _mover;
        
        private void Awake() 
        {
            _mover = GetComponent<Mover>();
        }

        public void SetState(float state)
        {
            _mover?.SetDistance(state);
        }
    }
}
