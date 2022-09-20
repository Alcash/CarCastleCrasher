using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Debugs;
using Leopotam.Ecs;
using UnityEngine;
#if UNITY_EDITOR
public class TestDebugSystem : MonoBehaviour,IEcsWorldDebugListener
{
    public void OnEntityCreated(EcsEntity entity)
    {
        var stackTrace = new System.Diagnostics.StackTrace();
        ref var sourceComponent = ref entity.Get<SourceComponent>();
        sourceComponent.StackTrace = stackTrace;
    }

    public void OnEntityDestroyed(EcsEntity entity)
    {
       
        
    }

    public void OnFilterCreated(EcsFilter filter)
    {
       
    }

    public void OnComponentListChanged(EcsEntity entity)
    {
        var stackTrace = new System.Diagnostics.StackTrace();
        ref var sourceComponent = ref entity.Get<SourceComponent>();
        sourceComponent.StackTrace = stackTrace;
    }

    public void OnWorldDestroyed(EcsWorld world)
    {
       
    }
}
#endif
