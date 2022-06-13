using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;

namespace DoorsEcsLeo.Server
{
    public class DirtyFlagSystem : IEcsSystem, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var dirtyEntities = world.Filter<DirtyComponent>().End();
            var dirties = world.GetPool<DirtyComponent>();

            foreach(var dirtyEntity in dirtyEntities)
            {
                ref var flag = ref dirties.Get(dirtyEntity);
                if(flag.IsRaisedToClear)
                {
                    dirties.Del(dirtyEntity);
                }
                else flag.IsRaisedToClear = true;
            }
        }

        public static void SetAsDirty(int entity, EcsPool<DirtyComponent> pool)
        {
            if(!pool.Has(entity)) pool.Add(entity);
        }
    }
}
