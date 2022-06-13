using Leopotam.EcsLite;
using UnityEngine;

namespace DoorsEcsLeo.Server
{
    public class InteractionSystem : IEcsSystem, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var eventsEntities = world.Filter<ClientEventComponent>().Inc<VObjectInteractionComponent>().End();
            var VObjectsEntities = world.Filter<VObjectComponent>().End();
            
            var VObjects = world.GetPool<VObjectComponent>();
            var events = world.GetPool<VObjectInteractionComponent>();
            var interactables = world.GetPool<InteractableComponent>();
            foreach(var eventEntity in eventsEntities)
            {
                var interactionEvent = events.Get(eventEntity);
                foreach(var VObjectEntity in VObjectsEntities)
                {
                    if(VObjects.Get(VObjectEntity).SceneId==interactionEvent.SceneId && interactables.Has(VObjectEntity))
                    {
                        ref var interactable = ref interactables.Get(VObjectEntity);
                        interactable.State+=interactable.InteractionSpeed;
                    }
                }
            }
        }
    }
}
