using ProjectCore.InterfaceManger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCore.StateManager
{
    public class LoadingGameState : IState
    {
        private readonly GamePlayManager _gamePlayManager;
        private readonly InterfaceManager _interfaceManager;
        private InterfaceWindow loadingWindow;
        private float loading = 0;
        private float loadingTime = 2;

        public LoadingGameState(GamePlayManager manager , InterfaceManager interfaceManager)
        {
            _gamePlayManager  = manager;
            _interfaceManager = interfaceManager;
        }

        public void StateEnd()
        {

        }

        public void StateInit()
        {

        }

        public void StateStart()
        {
            if (InterfaceManager.OpenView("loading", out loadingWindow))
            {
                //loadingWindow
            }
        }

        public void StateUpdate(float delta)
        {
            loading += delta;

            if (loading >= loadingTime)
            {
                _gamePlayManager.StartState(typeof(MainMenuState));
            }
        }


    }
}
