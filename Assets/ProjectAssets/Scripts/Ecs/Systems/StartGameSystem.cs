using System.Collections;
using System.Collections.Generic;
using GameCore.Event;
using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace GameCore
{
    public class StartGameSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<NewGameInit> _newGameFilter;
        public void Run()
        {
            foreach (var index in _newGameFilter)
            {
                ref var newGameInit =ref _newGameFilter.Get1(index);
                _ecsWorld.NewEntity().Replace(new SpawnPlatform() {Count = newGameInit.Difficult, Key = 0 });
                _newGameFilter.GetEntity(index).Destroy();
            }
        }

        public void Init()
        {
            _ecsWorld.NewEntity().Replace(new NewGameInit() {Difficult = 1 });
            
        }
    }
}
