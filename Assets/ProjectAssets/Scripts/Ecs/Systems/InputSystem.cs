using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using GameCore.Event;

namespace GameCore
{
    public class InputSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        public void Run()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
               
            }
        }

       
    }
}
