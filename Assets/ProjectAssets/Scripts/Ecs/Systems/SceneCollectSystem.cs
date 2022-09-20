using System.Linq;
using GameCore.MonoEntities.Base;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class SceneCollectSystem : IEcsInitSystem
    {
        private SceneItemsCollector m_SceneCollector;
        private EcsWorld _ecsWorld;

        public void Init()
        {
            var monoLinks = m_SceneCollector.GetCollectItems().GroupBy(x=> x.gameObject);
          
            foreach (var monoLink in monoLinks)
            {
                var entityWorld = _ecsWorld.NewEntity();
                foreach (var mono in monoLink)
                {
                    mono.Make(ref entityWorld);
                }
                

            }

        }
    }
}