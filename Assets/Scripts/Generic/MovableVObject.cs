using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoorsEcsLeo
{
    public class MovableVObject : VObject
    {
        private Rigidbody _rigidBody;

        public Rigidbody Rigidbody 
        {
            get
            {
                if(_rigidBody==null)
                    _rigidBody = GetComponent<Rigidbody>();
                return _rigidBody;
            }
        }
    }
}
