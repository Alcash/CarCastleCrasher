using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectCore.StateManager
{
    public interface IState
    {
        void StateInit();
        void StateUpdate(float delta);
        void StateStart();
        void StateEnd();
    }
}
