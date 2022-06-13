using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace DoorsEcsLeo.Client
{
    public class ClientInstaller
    {
        private EcsSystems _updateSystems;
        private EcsSystems _lateUpdateSystems;

        public void Install(EcsWorld ecsWorld, SceneVObjectsTable table)
        {
            _updateSystems = new EcsSystems(ecsWorld, table);
            _lateUpdateSystems = new EcsSystems(ecsWorld, table);

            _updateSystems.Add(new PlayerInputSystem());
            _updateSystems.Add(new VObjectsMoverSystem());
            _updateSystems.Add(new VObjectsInteractionSystem());
            _updateSystems.Add(new TargetViewSystem());
        }

        public void Init()
        {
            _updateSystems.Init();
            _lateUpdateSystems.Init();
        }

        public void Update()
        {
            _updateSystems.Run();
        }

        public void LateUpdate()
        {
            _lateUpdateSystems.Run();
        }
    }
}
