using Leopotam.EcsLite;
using UnityEngine;

namespace DoorsEcsLeo.Server
{
    public class PlayerMovingSystem : IEcsSystem, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            
            var entities = world.Filter<MovableComponent>()
                .Inc<PositionComponent>()
                .Inc<TargetMovingComponent>()
                .End();

            var targets = world.GetPool<TargetMovingComponent>();
            var movables = world.GetPool<MovableComponent>();
            var positions = world.GetPool<PositionComponent>();
            var dirties = world.GetPool<DirtyComponent>();

            foreach(var entity in entities)
            {
                ref var pos = ref positions.Get(entity);
                ref var movable = ref movables.Get(entity);
                ref var target = ref targets.Get(entity);
                var offset = (target.Target- pos.Position);
                if(offset.magnitude>0.05f)
                {
                    if(movable.TransitionType==TransitionType.Transitive)
                    {
                        DirtyFlagSystem.SetAsDirty(entity, dirties);
                        pos.Position += offset.normalized * movable.Speed * Time.deltaTime;
                    }
                        else
                    {
                        DirtyFlagSystem.SetAsDirty(entity, dirties);
                        pos.Position = target.Target;
                    }
                }
            }
        }

        
    }
}
