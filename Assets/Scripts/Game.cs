using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Zenject;

namespace DoorsEcsLeo
{
    public class Game : MonoBehaviour
    {
        [field: SerializeField] public SceneContext SceneContext {get; private set;}
        [field: SerializeField] public SceneVObjectsTable SceneVObjectsTable {get; private set;}

        public EcsWorld World {get; private set;}


        private Server.ServerInstaller _serverInstaller {get; set;}
        private Client.ClientInstaller _clientInstaller {get; set;}

        private void Awake() 
        {
            SceneContext.Run();
            
            World = new EcsWorld();

            SceneVObjectsTable.Init(World);
            
            _serverInstaller = new Server.ServerInstaller();
            _clientInstaller = new Client.ClientInstaller();
            _serverInstaller.Install(World);
            _clientInstaller.Install(World, SceneVObjectsTable);

            _serverInstaller.Init();
            _clientInstaller.Init();
        }

        private void Update() 
        {
            _serverInstaller.Update();
            _clientInstaller.Update();
        }

        private void LateUpdate()
        {
            _serverInstaller.LateUpdate();
            _clientInstaller.LateUpdate();
        }
    }
}
