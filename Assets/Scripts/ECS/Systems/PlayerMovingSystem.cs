using Leopotam.EcsLite;
using UnityEngine;

namespace DoorsEcsLeo.Server
{
    public class PlayerMovingSystem : IEcsSystem, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            
            var entities = world.Filter<TransitiveMovableComponent>()
                .Inc<PositionComponent>()
                .Inc<TargetMovingComponent>()
                .End();

            var targets = world.GetPool<TargetMovingComponent>();
            var movables = world.GetPool<TransitiveMovableComponent>();
            var positions = world.GetPool<PositionComponent>();

            foreach(var entity in entities)
            {
               ref var pos = ref positions.Get(entity);
               ref var movable = ref movables.Get(entity);
               ref var target = ref targets.Get(entity);
               var offset = (target.Target- pos.Position);
               if(offset.magnitude>0.05f)
               {
                    pos.Position += offset.normalized * movable.Speed * Time.deltaTime;
               }
               

            }
        }
    }
}
