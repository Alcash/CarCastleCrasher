using System.Collections;
using System.Collections.Generic;
using GameCore.MonoEntities.Base;
using UnityEngine;
using Leopotam.Ecs;

namespace GameCore
{
    public class PrefabFactory : MonoBehaviour
    {
        private EcsWorld _world;

        public void SetWorld(EcsWorld world)
        {
            _world = world;
        }

        public void Spawn(SpawnData spawnData)
        {
            var spawnedObject = Instantiate(spawnData.Prefab, spawnData.Position, spawnData.Rotation, spawnData.Parent);
            var monoEntity = spawnedObject.GetComponents<MonoEntityBase>();
            if (monoEntity == null)
                return;
            EcsEntity ecsEntity = _world.NewEntity();
     
            foreach (var monoEntityBase in monoEntity)
            {
                monoEntityBase.Make(ref ecsEntity);
            } 
        }
}
}