using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using GameCore.Event;
using GameCore.Assets;
using GameCore.MonoEntities.Components;
using NotImplementedException = System.NotImplementedException;

namespace GameCore.System
{
    public class PlatformSpawnerSystem : IEcsRunSystem, IEcsInitSystem
    {
        private  EcsWorld _world = null;
        private  AssetCollection _assetCollection;
        private EcsFilter<SpawnPlatform> _spawnPlatformFilter;

        public void Run()
        {
            foreach (var index in _spawnPlatformFilter)
            {
                var info = _spawnPlatformFilter.Get1(index);
                for (int i = 0; i < info.Count; i++)
                { 
                    var entity = _world.NewEntity().Replace(new SpawnData
                    {
                        Prefab = _assetCollection.AssetList[info.Key],
                        Position = Vector3.zero,
                        Rotation = Quaternion.identity,
                        Parent = null
                    });
                }
                
                ref EcsEntity spawnEntity = ref _spawnPlatformFilter.GetEntity(index);
                spawnEntity.Del<SpawnPlatform>();
            }
        }

        public void Init()
        {
            
        }
    }
}
