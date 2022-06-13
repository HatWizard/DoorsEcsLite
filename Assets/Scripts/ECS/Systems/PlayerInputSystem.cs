using Leopotam.EcsLite;
using DoorsEcsLeo.Server;
using UnityEngine;

namespace DoorsEcsLeo.Client
{
    public class PlayerInputSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem
    {
        private EcsSystems _systems;
        private SceneVObjectsTable _sceneVObjectsTable;

        public void Init(EcsSystems systems)
        {
            _systems = systems;
            _sceneVObjectsTable = systems.GetShared<SceneVObjectsTable>();
        }

        public void Run(EcsSystems systems)
        {
            if(Input.GetMouseButtonDown(0))
            {
                var screenPos = Input.mousePosition;
                var ray = Camera.main.ScreenPointToRay(screenPos);
                if(Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    var end = hit.point;
                    CheckPath(end);
                }
                
            }
        }

        private void CheckPath(Vector3 endPoint)
        {
            var world = _systems.GetWorld();

            var players = world.Filter<PlayerComponent>().Inc<PositionComponent>().End();
            var clientEvents = world.GetPool<ClientEventComponent>();
            var playerDestinations = world.GetPool<PlayerDestinationPointComponent>();
            var disposables = world.GetPool<DisposableComponent>();
            var positions = world.GetPool<PositionComponent>();
            foreach(var entity in players)
            {
                ref var pos = ref positions.Get(entity);
                if(CheckObstacles(pos.Position, endPoint))
                {
                    ref var destination = ref _sceneVObjectsTable.RaiseEvent<PlayerDestinationPointComponent>();
                    destination.Point = endPoint;
                }
            }
        }

        private bool CheckObstacles(Vector3 start, Vector3 end)
        {
            var dist = (end-start).magnitude;
            //выглядит не очень, но иначе нужна еще интеграция с физикой или pathfinding
            var hits = Physics.SphereCast(start, 0.2f, (end-start),
                out RaycastHit ray, dist, LayerMask.GetMask("SceneObstacle"), QueryTriggerInteraction.Ignore);
            return Mathf.Abs(end.y)<0.3f && !hits;
        }
    }
}
