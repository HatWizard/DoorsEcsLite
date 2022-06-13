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
            var movables = world.GetPool<MovableComponent>();
            var positions = world.GetPool<PositionComponent>();

            var entities = world.Filter<VObjectComponent>().Inc<DirtyComponent>().End();

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
                        var moving = movables.Get(entity);
                        var dist = (pos.Position - movableVObject.Rigidbody.position).magnitude;
                        if(moving.TransitionType==TransitionType.Transitive)
                            MoveTransitive(movableVObject, pos.Position, dist, moving.Speed);
                        else
                            MoveImmediate(movableVObject, pos.Position);
                    }
                }
            }

        }

        private void MoveTransitive(MovableVObject movableVObject, Vector3 position, float distance, float speed)
        {   
            var offset = position - movableVObject.Rigidbody.position;
            if(distance>5)
            {
                movableVObject.Rigidbody.position = position;
            }
            else if(distance>0.01f)
            {
                movableVObject.Rigidbody.MovePosition(position + offset.normalized * speed * Time.deltaTime);
            }
        }

        private void MoveImmediate(MovableVObject movableVObject, Vector3 position)
        {
            movableVObject.Rigidbody.position = position;
        }
    }
}
