using UnityEngine;
using Leopotam.EcsLite;
using DoorsEcsLeo.Server;

namespace DoorsEcsLeo.Client
{
    public class VObjectsMoverSystem : IEcsSystem, IEcsRunSystem, IEcsInitSystem
    {
        private SceneVObjectsTable _sceneVObjectsTable {get; set;}

        public void Init(EcsSystems systems)
        {
            _sceneVObjectsTable = systems.GetShared<SceneVObjectsTable>();
        }

        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var vObjects = world.GetPool<VObjectComponent>();
            var movables = world.GetPool<TransitiveMovableComponent>();
            var positions = world.GetPool<PositionComponent>();

            var entities = world.Filter<VObjectComponent>().End();

            foreach(var entity in entities)
            {
                if(movables.Has(entity))
                {
                    ref var pos = ref positions.Get(entity);
                    ref var VObjectComponent = ref vObjects.Get(entity);
                    if(_sceneVObjectsTable.ContainsVObject(VObjectComponent.SceneId))
                    {
                        var sceneVObject = _sceneVObjectsTable.GetVObject(VObjectComponent.SceneId);
                        var movableVObject = (MovableVObject) sceneVObject;
                        var offset = pos.Position - movableVObject.Rigidbody.position;
                        var dist = (offset).magnitude;
                        if(dist>5)
                        {
                            movableVObject.Rigidbody.position = pos.Position;
                        }
                        else if(dist>0.01f)
                        {
                            var speed = movables.Get(entity).Speed;
                            movableVObject.Rigidbody.MovePosition(pos.Position + offset.normalized * speed * Time.deltaTime);
                        }
                    }
                }
            }

        }
    }
}
