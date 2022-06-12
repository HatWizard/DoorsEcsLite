using Leopotam.EcsLite;
using UnityEngine;

namespace DoorsEcsLeo.Client
{
    public class DisposeSystem : IEcsSystem, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var entities = world.Filter<DisposableComponent>().End();
            var disposables = world.GetPool<DisposableComponent>();

            foreach(var entity in entities)
            {
                ref var disposable = ref disposables.Get(entity);
                if(disposable.DeleteFlag)
                {
                    world.DelEntity(entity);
                }
                else
                {
                    disposable.DeleteFlag = true;
                }
                
            }
        }
    }
}
