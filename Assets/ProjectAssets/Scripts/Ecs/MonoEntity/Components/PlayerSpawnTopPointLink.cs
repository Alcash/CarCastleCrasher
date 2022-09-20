using System.Collections;
using System.Collections.Generic;
using GameCore.MonoEntities.Base;
using Leopotam.Ecs;
using UnityEngine;

namespace GameCore.MonoEntities.Components
{
    public class PlayerSpawnTopPointLink : MonoEntity<PlayerSpawnPointEntity>
    {
        public override void Make(ref EcsEntity entity)
        {
            entity.Get<PlayerSpawnPointEntity>() = new PlayerSpawnPointEntity
            {
                Value = this
            };
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Value.Value == null)
            {
                Value = new PlayerSpawnPointEntity
                {
                    Value = this
                };
            }
        }
#endif
    }
}
