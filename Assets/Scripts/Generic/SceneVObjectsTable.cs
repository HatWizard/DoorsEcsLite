using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoorsEcsLeo
{
    public class SceneVObjectsTable : MonoBehaviour
    {
        [field: SerializeField] private List<VObject> VObjects;
        private Dictionary<string, VObject> _pregeneratedDict;

        public void Init()
        {
            _pregeneratedDict = new Dictionary<string, VObject>();
            foreach(var vObject in VObjects)
            {
                _pregeneratedDict.Add(vObject.SceneId, vObject);
            }
        }

        public bool ContainsVObject(string id)
        {
            return _pregeneratedDict.ContainsKey(id);
        }

        public VObject GetVObject(string id)
        {
            if(ContainsVObject(id))
                return _pregeneratedDict[id];
            return null;
        }

    }
}
