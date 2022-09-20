using Leopotam.Ecs;
using UnityEngine;

namespace GameCore.MonoEntities.Base
{
    public abstract class MonoEntityBase : MonoBehaviour
    {
        public abstract void Make(ref EcsEntity ecsEntity);
    }
}
