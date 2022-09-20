using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

namespace ProjectCore.Vehicle
{
    public class PlayerVehicleControl : MonoBehaviour, IVehicleControl
    {      
        [SerializeField] internal VehicleInputs m_Inputs;
        string throttleInput => m_Inputs.ThrottleInput;
        string brakeInput => m_Inputs.BrakeInput;
        string turnInput => m_Inputs.TurnInput;
        string jumpInput => m_Inputs.JumpInput;
        string driftInput => m_Inputs.DriftInput;
        string boostInput => m_Inputs.BoostInput;


        public float ThrottleValue => GetInput(throttleInput) - GetInput(brakeInput);
        public bool BoostingValue => (GetInput(boostInput) > 0.5f);
        public float SteeringValue => GetInput(turnInput);
        public bool DriftValue => GetInput(driftInput) > 0;

        private bool handbrake = false;
        public bool HandbrakeValue => handbrake;

        public void ToogleHandbrake(bool h)
        {
            handbrake = h;
        }

        public bool JumpValue => GetInput(jumpInput) != 0;


        // MULTIOSCONTROLS is another package I'm working on ignore it I don't know if it will get a release.
#if MULTIOSCONTROLS
        private static MultiOSControls _controls;
#endif

        // Use this method if you want to use your own input manager
        private float GetInput(string input)
        {
#if MULTIOSCONTROLS
        return MultiOSControls.GetValue(input, playerId);
#else
            return Input.GetAxis(input);
#endif
        }
    }
}
