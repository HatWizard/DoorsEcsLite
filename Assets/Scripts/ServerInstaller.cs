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
            _lateUpdateSystems.Add(new DisposeSystem());
            _lateUpdateSystems.Add(new DirtyFlagSystem());
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
            var movables = ecsWorld.GetPool<MovableComponent>();
            var positions = ecsWorld.GetPool<PositionComponent>();
            var targets = ecsWorld.GetPool<TargetMovingComponent>();
            var vObjects = ecsWorld.GetPool<VObjectComponent>();
            var players = ecsWorld.GetPool<PlayerComponent>();
            var interactables = ecsWorld.GetPool<InteractableComponent>();
            var dirties = ecsWorld.GetPool<DirtyComponent>();

            var player = ecsWorld.NewEntity();
            var redDoor = ecsWorld.NewEntity();
            var greenDoor = ecsWorld.NewEntity();
            var target = ecsWorld.NewEntity();
  

            positions.Add(player);
            targets.Add(player);
            players.Add(player);
            positions.Add(target);
            targets.Add(target);
            players.Add(target);
            dirties.Add(target);
            dirties.Add(player);
            ref var targetMoving = ref movables.Add(target);
            ref var playerMoving = ref movables.Add(player);

            ref var playerView = ref vObjects.Add(player);
            ref var redDoorView = ref vObjects.Add(redDoor);
            ref var greenDoorView = ref vObjects.Add(greenDoor);
            ref var targetView = ref vObjects.Add(target);
            
            playerView.SceneId = SceneIdentifiers.Player.ToString();
            redDoorView.SceneId =  SceneIdentifiers.RedDoor.ToString();
            greenDoorView.SceneId =  SceneIdentifiers.GreenDoor.ToString();
            targetView.SceneId =  SceneIdentifiers.Target.ToString();

            playerMoving.Speed = 5f;
            targetMoving.TransitionType = TransitionType.Immediate;

            ref var interactableRed = ref interactables.Add(redDoor);
            ref var interactableGreen = ref interactables.Add(greenDoor);

            interactableRed.InteractionSpeed = 0.001f;
            interactableGreen.InteractionSpeed = 0.001f;
        }

    }
}
