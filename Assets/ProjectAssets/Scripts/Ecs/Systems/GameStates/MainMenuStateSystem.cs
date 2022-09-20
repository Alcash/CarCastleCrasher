using GameCore.Component;
using Leopotam.Ecs;
using ProjectCore.InterfaceManger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectCore.Ecs.GameState
{
    public class MainMenuStateSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world = null;
        [SerializeField] private InterfaceManager m_InterfaceManager;

        private const string systemKey = "MainMenuState";
        EcsFilter<ChangeStateEntity> stateFilter;
        private bool stateActive = false;

        public void Init()
        {
            
        }

        public void Run()
        {
            foreach (var index in stateFilter)
            {
                ref EcsEntity stateEntity = ref stateFilter.GetEntity(index);
                var state = stateEntity.Get<ChangeStateEntity>();
                if (state.NewStateName == systemKey)
                {
                    stateActive = true;
                    stateEntity.Del<ChangeStateEntity>();
                }
            }

            if (stateActive)
            {
                //loading
               
            }
        }
    }
}
