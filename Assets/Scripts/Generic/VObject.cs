using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoorsEcsLeo
{
    public class VObject : MonoBehaviour
    {
        [field: SerializeField] public string SceneId {get; set;}

        public bool IsPlayer 
        {
            get
            {
                return SceneId == SceneIdentifiers.Player.ToString();
            }
        }
    }
}
