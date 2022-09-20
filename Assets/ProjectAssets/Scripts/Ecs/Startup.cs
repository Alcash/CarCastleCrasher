using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using GameCore.System;
using GameCore.Assets;
using GameCore.Config;
using GameCore.Event;
using GameCore.MonoEntities.Base;
using ProjectCore.Ecs.GameState;
using ProjectCore.InterfaceManger;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif 
using Systems;

namespace GameCore
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private AssetCollection m_AssetCollection;
        [SerializeField] private GameData _GameData;
        private EcsWorld _world;
        private EcsSystems _ecsSystems;
        private EcsSystems _runEcsSystems;
        [SerializeField] private PrefabFactory m_PrefabFactory;

        [SerializeField] private SceneItemsCollector m_SceneCollector;
        [SerializeField] private InterfaceManager m_InterfaceManager;
#if UNITY_EDITOR
        [SerializeField] private TestDebugSystem testDebug;

#endif 
        private void Start()
        {
            _world = new EcsWorld();
#if UNITY_EDITOR
               EcsWorldObserver.Create (_world);

            _world.AddDebugListener(testDebug);
#endif 
            m_PrefabFactory.SetWorld(_world);
            _ecsSystems = new EcsSystems(_world, "First main")
                    .Add(new SpawnPlayerAvatar())
                    .Add(new PlayerAvatarSystem())
                    .Add(new SceneCollectSystem())
                ;

            Injects(ref _ecsSystems);
#if UNITY_EDITOR
            EcsSystemsObserver.Create (_ecsSystems);
#endif
            _ecsSystems.Init();

            _runEcsSystems = GetUpdateSystems();
            Injects(ref _runEcsSystems);
#if UNITY_EDITOR
            EcsSystemsObserver.Create (_runEcsSystems);
#endif
            _runEcsSystems.Init();

        }

        private void Injects(ref EcsSystems system)
        {
            system.Inject(m_AssetCollection)
                .Inject(_GameData)
                .Inject(m_PrefabFactory)
                .Inject(m_SceneCollector)
                ;
        }
        
        private EcsSystems GetUpdateSystems()
        {
            return  new EcsSystems(_world, "First run Update")
                    .Add(new SpawnerSystem())
                    .Add(new PlatformSpawnerSystem())
                    .Add(new InputSystem())
                    .Add(new StartGameSystem())
                    .Add(new MainMenuStateSystem())
                    .Add(new LoadingStateSystem())
                    .Add(new LoadingSceneSystem())
                    .Add(new PrepareActionStateSystem())

                ;
        }
        
        private void Update()
        {
            _ecsSystems.Run();
            _runEcsSystems.Run();
        }

        private void OnDestroy()
        {

            _ecsSystems.Destroy();
            _runEcsSystems.Destroy();
            _world.Destroy();
        }
    }
}