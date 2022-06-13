using Leopotam.EcsLite;

namespace DoorsEcsLeo.Server
{
    public class ServerInstaller
    {

        private EcsSystems _updateSystems;
        private EcsSystems _lateUpdateSystems;

        public void Install(EcsWorld ecsWorld)
        {
            _updateSystems = new EcsSystems(ecsWorld);
            _lateUpdateSystems = new EcsSystems(ecsWorld);
            ConfigureServer(ecsWorld);


            _updateSystems.Add(new PlayerMovingSystem());
            _updateSystems.Add(new PlayerDestinationSystem());
            _updateSystems.Add(new InteractionSystem());
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

        //todo где-то здесь мы дергаем настройки уровня на сервере
        private void ConfigureServer(EcsWorld ecsWorld)
        {
            var movables = ecsWorld.GetPool<TransitiveMovableComponent>();
            var positions = ecsWorld.GetPool<PositionComponent>();
            var targets = ecsWorld.GetPool<TargetMovingComponent>();
            var vObjects = ecsWorld.GetPool<VObjectComponent>();
            var players = ecsWorld.GetPool<PlayerComponent>();
            var interactables = ecsWorld.GetPool<InteractableComponent>();

            var player = ecsWorld.NewEntity();
            var redDoor = ecsWorld.NewEntity();
            var greenDoor = ecsWorld.NewEntity();

            positions.Add(player);
            targets.Add(player);
            players.Add(player);
            ref var movable = ref movables.Add(player);

            ref var playerView = ref vObjects.Add(player);
            ref var redDoorView = ref vObjects.Add(redDoor);
            ref var greenDoorView = ref vObjects.Add(greenDoor);
            
            playerView.SceneId = "Player";
            redDoorView.SceneId = "RedDoor";
            greenDoorView.SceneId = "GreenDoor";

            movable.Speed = 5f;

            ref var interactableRed = ref interactables.Add(redDoor);
            ref var interactableGreen = ref interactables.Add(greenDoor);

            interactableRed.InteractionSpeed = 0.001f;
            interactableGreen.InteractionSpeed = 0.001f;
        }

    }
}
