using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.MonoEntities.Base
{
    public abstract class MonoEntity<T> : MonoEntityBase where T : struct
    {
        public T Value;

        public override void Make(ref EcsEntity ecsEntity)
        {
            ecsEntity.Get<T>() = Value;
        }
    }
}
