using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DoorsEcsLeo
{
    public class SceneInjector : MonoInstaller
    {
        [field: SerializeField] SceneVObjectsTable SceneVObjectsTable {get; set;}

        public override void InstallBindings()
        {
            Container.Bind<SceneVObjectsTable>().FromInstance(SceneVObjectsTable).AsSingle();
        }
    }
}
