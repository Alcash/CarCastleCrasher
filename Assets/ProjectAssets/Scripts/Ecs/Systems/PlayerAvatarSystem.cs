using GameCore.MonoEntities.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems 
{
    sealed class PlayerAvatarSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        //private EcsFilter<GameObjectEntity> _gameOFilter;
        private EcsFilter<PlayerTag> _playerFilter;
        private bool _isInited;

        public void Init()
        {
            foreach (var playerIndex in _playerFilter)
            {
                ref var playerEntity = ref _playerFilter.GetEntity(playerIndex);
                var rd2d = playerEntity.Get<Rigidbody2DEntity>();
                rd2d.Value.simulated = false;
                rd2d.Value.isKinematic = true;
                Debug.Log($"Init");
            }
        }

        public void Run()
        {
            if (_isInited == false)
            {
                foreach (var playerIndex in _playerFilter)
                {
                    _isInited = true;
                    ref var playerEntity = ref _playerFilter.GetEntity(playerIndex);
                    var rd2d = playerEntity.Get<Rigidbody2DEntity>();
                    rd2d.Value.simulated = false;
                    rd2d.Value.isKinematic = true;

                }
            }
        }
    }
}