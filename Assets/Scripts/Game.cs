using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Zenject;

namespace DoorsEcsLeo
{
    public class Game : MonoBehaviour
    {
        [field: SerializeField] private SceneContext SceneContext {get; set;}
        [field: SerializeField] private SceneVObjectsTable SceneVObjectsTable {get; set;}

        public EcsWorld World {get; private set;}


        private Server.ServerInstaller _serverInstaller {get; set;}
        private Client.ClientInstaller _clientInstaller {get; set;}

        private void Awake() 
        {
            SceneContext.Run();
            SceneVObjectsTable.Init();

            World = new EcsWorld();
            
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
