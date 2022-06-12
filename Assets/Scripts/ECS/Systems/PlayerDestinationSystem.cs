using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;

namespace DoorsEcsLeo.Server
{
    public class PlayerDestinationSystem : IEcsSystem, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var destinations = world.GetPool<PlayerDestinationPointComponent>();
            var clientEvents = world.Filter<ClientEventComponent>().Inc<PlayerDestinationPointComponent>().End();
            var targets = world.GetPool<TargetMovingComponent>();
            var movablePlayers = world.Filter<PlayerComponent>().Inc<TargetMovingComponent>().End();


            foreach(var eventEntity in clientEvents)
            {
                ref var dest = ref destinations.Get(eventEntity);
                foreach(var player in movablePlayers)
                {
                    ref var target = ref targets.Get(player);
                    target.Target = dest.Point;
                }
            }
        }
    }
}
