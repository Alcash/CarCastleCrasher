using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCore.Vehicle
{
    [CreateAssetMenu(fileName = "Vehicle Core", menuName = "VehicleBehaviour/Core", order = 2)]
    public class VehicleCore : ScriptableObject
    {
        [SerializeField] AnimationCurve turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);
        public AnimationCurve TurnInputCurve
        {
            get => turnInputCurve;
            set => turnInputCurve = value;
        }
        [SerializeField] AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
        public AnimationCurve MotorTorqueCurve
        {
            get => motorTorque;
            set => motorTorque = value;
        }
        /*
        *  Motor torque represent the torque sent to the wheels by the motor with x: speed in km/h and y: torque
        *  The curve should start at x=0 and y>0 and should end with x>topspeed and y<0
        *  The higher the torque the faster it accelerate
        *  the longer the curve the faster it gets
        */
        // Differential gearing ratio
        [Range(2, 16)]
        [SerializeField] float diffGearing = 4.0f;
        public float DiffGearing
        {
            get => diffGearing;
            set => diffGearing = value;
        }

        // Basicaly how hard it brakes
        [SerializeField] float brakeForce = 1500.0f;
        public float BrakeForce
        {
            get => brakeForce;
            set => brakeForce = value;
        }

        // Max steering hangle, usualy higher for drift car
        [Range(0f, 50.0f)]
        [SerializeField] float steerAngle = 30.0f;
        public float SteerAngle
        {
            get => steerAngle;
            set => steerAngle = Mathf.Clamp(value, 0.0f, 50.0f);
        }

        // The value used in the steering Lerp, 1 is instant (Strong power steering), and 0 is not turning at all
        [Range(0.001f, 1.0f)]
        [SerializeField] float steerSpeed = 0.2f;
        public float SteerSpeed
        {
            get => steerSpeed;
            set => steerSpeed = Mathf.Clamp(value, 0.001f, 1.0f);
        }

        // How hight do you want to jump?
        [Range(1f, 1.5f)]
        [SerializeField] float jumpVel = 1.3f;
        public float JumpVel
        {
            get => jumpVel;
            set => jumpVel = Mathf.Clamp(value, 1.0f, 1.5f);
        }

        // How hard do you want to drift?
        [Range(0.0f, 2f)]
        [SerializeField] float driftIntensity = 1f;
        public float DriftIntensity
        {
            get => driftIntensity;
            set => driftIntensity = Mathf.Clamp(value, 0.0f, 2.0f);
        }

        // Force aplied downwards on the car, proportional to the car speed
        [Range(0.5f, 10f)]
        [SerializeField] float downforce = 1.0f;

        public float Downforce
        {
            get => downforce;
            set => downforce = Mathf.Clamp(value, 0, 5);
        }

        // When IsPlayer is false you can use this to control the steering
        float steering;
        public float Steering
        {
            get => steering;
            set => steering = Mathf.Clamp(value, -1f, 1f);
        }

        // When IsPlayer is false you can use this to control the throttle
        float throttle;
        public float Throttle
        {
            get => throttle;
            set => throttle = Mathf.Clamp(value, -1f, 1f);
        }

        // Like your own car handbrake, if it's true the car will not move
        [SerializeField] bool handbrake;
        public bool Handbrake
        {
            get => handbrake;
            set => handbrake = value;
        }

        [Header("Boost")]
        // Disable boost
        [HideInInspector] public bool allowBoost = true;

        // Maximum boost available
        [SerializeField] float maxBoost = 10f;
        public float MaxBoost
        {
            get => maxBoost;
            set => maxBoost = value;
        }

        // Regen boostRegen per second until it's back to maxBoost
        [Range(0f, 1f)]
        [SerializeField] float boostRegen = 0.2f;
        public float BoostRegen
        {
            get => boostRegen;
            set => boostRegen = Mathf.Clamp01(value);
        }

        /*
         *  The force applied to the car when boosting
         *  NOTE: the boost does not care if the car is grounded or not
         */
        [SerializeField] float boostForce = 5000;
        public float BoostForce
        {
            get => boostForce;
            set => boostForce = value;
        }
    }
}
