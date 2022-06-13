using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using DoorsEcsLeo.Server;

namespace DoorsEcsLeo.Client
{
    public class VObjectsInteractionSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsSystems _systems;
        private SceneVObjectsTable _sceneVObjectsTable;

        public void Init(EcsSystems systems)
        {
            _systems = systems;
            _sceneVObjectsTable = systems.GetShared<SceneVObjectsTable>();
        }

        void IEcsRunSystem.Run(EcsSystems systems)
        {
            var world = _systems.GetWorld();
            var vObjectsEntities = world.Filter<VObjectComponent>().Inc<InteractableComponent>().End();

                        
            var vObjects = world.GetPool<VObjectComponent>();
            var interactables = world.GetPool<InteractableComponent>();

            foreach(var entity in vObjectsEntities)
            {
                var id = vObjects.Get(entity).SceneId;
                var interaction = interactables.Get(entity);
                if(_sceneVObjectsTable.ContainsVObject(id))
                {
                    var interactableView = (InteractableVObject)_sceneVObjectsTable.GetVObject(id);
                    interactableView?.SetState(interaction.State);
                }
            }
        }
    }
}
