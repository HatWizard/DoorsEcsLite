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

            var movables = ecsWorld.GetPool<TransitiveMovableComponent>();
            var positions = ecsWorld.GetPool<PositionComponent>();
            var targets = ecsWorld.GetPool<TargetMovingComponent>();
            var vObjects = ecsWorld.GetPool<VObjectComponent>();
            var players = ecsWorld.GetPool<PlayerComponent>();
            var player = ecsWorld.NewEntity();
            positions.Add(player);
            targets.Add(player);
            players.Add(player);
            ref var movable = ref movables.Add(player);
            ref var vObject = ref vObjects.Add(player);
            vObject.SceneId = "Player";
            movable.Speed = 5f;


            _updateSystems.Add(new PlayerMovingSystem());
            _updateSystems.Add(new PlayerDestinationSystem());
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
