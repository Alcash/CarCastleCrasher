using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectCore.InterfaceManger;

namespace ProjectCore.StateManager
{
    public class GamePlayManager : MonoBehaviour
    {
        private Dictionary<Type,IState> _states;
        private InterfaceManager _interfaceManger;
        private IState _currentState;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;

            _states = new Dictionary<Type,IState>();           
            _states.Add(typeof(LoadingGameState),new LoadingGameState(this, _interfaceManger));
            _states.Add(typeof(LoadingScene), new LoadingScene());
            _states.Add(typeof(MainMenuState), new MainMenuState());
            _states.Add(typeof(AimToCastleState), new AimToCastleState());
            _states.Add(typeof(DestroyerState), new DestroyerState());
            _states.Add(typeof(DestroyResultState), new DestroyResultState());

            _currentState = _states[typeof(LoadingGameState)];
        }

        private void Update()
        {
            _currentState.StateUpdate(Time.deltaTime);
        }

        public void StartState(Type stateName)
        {
            _currentState.StateEnd();
            _currentState = _states[stateName];
            _currentState.StateStart();
        }
    }
}