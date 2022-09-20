using System;
using GameCore;
using GameCore.Debugs;
using GameCore.Event;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class SpawnerSystem: IEcsRunSystem
    {
        private EcsFilter<SpawnData> _spawnFilter;
        private PrefabFactory m_PrefabFactory;

        public void Run()
        {
            foreach (int index in _spawnFilter)
            {
                ref EcsEntity spawnEntity = ref _spawnFilter.GetEntity(index);
                var spawnPrefabData = spawnEntity.Get<SpawnData>();
                m_PrefabFactory.Spawn(spawnPrefabData);
                spawnEntity.Del<SpawnData>();
            }
        }
    }
}