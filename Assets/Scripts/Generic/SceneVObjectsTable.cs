using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using DoorsEcsLeo.Server;
using UnityEngine;

namespace DoorsEcsLeo
{
    public class SceneVObjectsTable : MonoBehaviour
    {
        [field: SerializeField] private List<VObject> VObjects;
        private Dictionary<string, VObject> _pregeneratedDict;
        private EcsWorld _world;

        public void Init(EcsWorld ecsWorld)
        {
            _pregeneratedDict = new Dictionary<string, VObject>();
            foreach(var vObject in VObjects)
            {
                _pregeneratedDict.Add(vObject.SceneId, vObject);
            }
            _world = ecsWorld;
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

        public ref T RaiseEvent<T>() where T : struct
        {
            var entity = _world.NewEntity();
            var pool = _world.GetPool<T>();
            var clientEvents = _world.GetPool<ClientEventComponent>();
            var disposables = _world.GetPool<DisposableComponent>();
            clientEvents.Add(entity);
            disposables.Add(entity);
            ref var eventComponent = ref pool.Add(entity);
            return ref eventComponent;
        }

    }
}
