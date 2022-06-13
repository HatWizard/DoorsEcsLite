using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using DoorsEcsLeo.Server;

namespace DoorsEcsLeo.Client
{
    public class TargetViewSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem
    {
        private SceneVObjectsTable _sceneVObjectsTable;

        public void Init(EcsSystems systems)
        {
            _sceneVObjectsTable = systems.GetShared<SceneVObjectsTable>();
        }

        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var targetViewsEntities = world.Filter<VObjectComponent>()
            .Inc<PositionComponent>()
            .End();
            var vObjects = world.GetPool<VObjectComponent>();
            var dirties = world.GetPool<DirtyComponent>();
            var playerPosition = _sceneVObjectsTable.GetVObject(SceneIdentifiers.Player.ToString()).transform.position;
            var isDirty = dirties.Has(_sceneVObjectsTable.GetEntity(SceneIdentifiers.Player.ToString()));
            foreach(var entity in targetViewsEntities)
            {
                var view = vObjects.Get(entity);
                if(view.SceneId==SceneIdentifiers.Target.ToString())
                {
                    var viewObject = (MovableVObject)_sceneVObjectsTable.GetVObject(view.SceneId);
                    if(viewObject==null) return;
                    if(!isDirty) 
                        viewObject.gameObject.SetActive(false);
                    else 
                        viewObject.gameObject.SetActive(Vector3.Distance(playerPosition, viewObject.Rigidbody.position)>0.2f);
                }
            }
        }

        
    }
}
