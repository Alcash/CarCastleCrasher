using Diagnostics = System.Diagnostics;
using System.Collections.Generic;
using GameCore.Config;
using GameCore.Debugs;
using GameCore.MonoEntities.Components;
using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace GameCore
{
    public class SpawnPlayerAvatar : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private GameData _GameData;
        private EcsFilter<SpawnData> _spawnData;
        private EcsFilter<PlayerTag> _playerFilter;
        private bool _isInited;
        public void Init()
        {
            var entity = _world.NewEntity().Replace(new SpawnData
            {
                Prefab = _GameData.PlayerPrefab,
                Position = Vector3.zero,
                Rotation = Quaternion.identity,
                Parent = null
            });
            
        }

        public void Run()
        {
            if (_isInited == false)
            {
                foreach (var index in _playerFilter)
                {
                    _isInited = true;
                    var player = _playerFilter.GetEntity(index);
                   // player.Get<Rigidbody2DEntity>().Value.simulated = false;
                    player.Get<Rigidbody2DEntity>().Value.isKinematic = true;
                }
            }
        }
    }
}

