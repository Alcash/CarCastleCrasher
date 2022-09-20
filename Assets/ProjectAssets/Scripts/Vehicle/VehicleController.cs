/*
 * This code is part of Arcade Car Physics for Unity by Saarg (2018)
 * 
 * This is distributed under the MIT Licence (see LICENSE.md for details)
 */
using System.Runtime.CompilerServices;
using UnityEngine;

#if MULTIOSCONTROLS
    using MOSC;
#endif
using VehicleBehaviour;
[assembly: InternalsVisibleTo("VehicleBehaviour.Dots")]
namespace ProjectCore.Vehicle
{
    [RequireComponent(typeof(Rigidbody))]
    public class VehicleController : MonoBehaviour
    {

        [Header("Inputs")]
#if MULTIOSCONTROLS
        [SerializeField] PlayerNumber playerId;
#endif

        private IVehicleControl _vehicleControl;
        [Header("Wheels")]
        [SerializeField] WheelCollider[] driveWheel = new WheelCollider[0];
        public WheelCollider[] DriveWheel => driveWheel;
        [SerializeField] WheelCollider[] turnWheel = new WheelCollider[0];

        public WheelCollider[] TurnWheel => turnWheel;

        // This code checks if the car is grounded only when needed and the data is old enough
        bool isGrounded = false;
        int lastGroundCheck = 0;
        public bool IsGrounded
        {
            get
            {
                if (lastGroundCheck == Time.frameCount)
                    return isGrounded;

                lastGroundCheck = Time.frameCount;
                isGrounded = true;
                foreach (WheelCollider wheel in wheels)
                {
                    if (!wheel.gameObject.activeSelf || !wheel.isGrounded)
                        isGrounded = false;
                }
                return isGrounded;
            }
        }

        [SerializeField] private VehicleCore _vehicleCore;            
       

        // Reset Values
        Vector3 spawnPosition;
        Quaternion spawnRotation;

        /*
         *  The center of mass is set at the start and changes the car behavior A LOT
         *  I recomment having it between the center of the wheels and the bottom of the car's body
         *  Move it a bit to the from or bottom according to where the engine is
         */
        [SerializeField] Transform centerOfMass = null;       
       
        private float steering;   
        private float throttle;
        private bool handbrake;       
        public bool Handbrake
        {
            get => handbrake;           
        }

        [HideInInspector] public bool allowDrift = true;
        private bool drift;
      

        // Use this to read the current car speed (you'll need this to make a speedometer)
        [SerializeField] float speed = 0.0f;
        public float Speed => speed;

        [Header("Particles")]
        // Exhaust fumes
        [SerializeField] ParticleSystem[] gasParticles = new ParticleSystem[0];

        [Header("Boost")]
        // Disable boost
        [HideInInspector] public bool allowBoost = true;      
      
        private float boost = 10f;
        private bool boosting = false;        
        private bool jumping = false;

        // Boost particles and sound
        [SerializeField] ParticleSystem[] boostParticles = new ParticleSystem[0];
        [SerializeField] AudioClip boostClip = default;
        [SerializeField] AudioSource boostSource = default;

        // Private variables set at the start
        Rigidbody rb = default;
        internal WheelCollider[] wheels = new WheelCollider[0];

        // Init rigidbody, center of mass, wheels and more
        void Start()
        {
#if MULTIOSCONTROLS
            Debug.Log("[ACP] Using MultiOSControls");
#endif
            if (boostClip != null)
            {
                boostSource.clip = boostClip;
            }

            boost = _vehicleCore.MaxBoost;

            rb = GetComponent<Rigidbody>();
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;

            if (rb != null && centerOfMass != null)
            {
                rb.centerOfMass = centerOfMass.localPosition;
            }

            wheels = GetComponentsInChildren<WheelCollider>();

            // Set the motor torque to a non null value because 0 means the wheels won't turn no matter what
            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = 0.0001f;
            }
        }

        // Visual feedbacks and boost regen
        void Update()
        {
            foreach (ParticleSystem gasParticle in gasParticles)
            {
                gasParticle.Play();
                ParticleSystem.EmissionModule em = gasParticle.emission;
                em.rateOverTime = handbrake ? 0 : Mathf.Lerp(em.rateOverTime.constant, Mathf.Clamp(150.0f * throttle, 30.0f, 100.0f), 0.1f);
            }

            if (allowBoost)
            {
                boost += Time.deltaTime * _vehicleCore.BoostRegen;
                if (boost > _vehicleCore.MaxBoost) { boost = _vehicleCore.MaxBoost; }
            }
        }

        // Update everything
        void FixedUpdate()
        {
            // Mesure current speed
            speed = transform.InverseTransformDirection(rb.velocity).z * 3.6f;

            // Get all the inputs!

            // Accelerate & brake

            throttle = _vehicleControl.ThrottleValue;

            // Boost
            boosting = _vehicleControl.BoostingValue;
            // Turn
            steering = _vehicleCore.TurnInputCurve.Evaluate(_vehicleControl.SteeringValue) * _vehicleCore.SteerAngle; 
            // Dirft
            drift = _vehicleControl.DriftValue && rb.velocity.sqrMagnitude > 100;
            // Jump
            jumping = _vehicleControl.JumpValue;


            // Direction
            foreach (WheelCollider wheel in turnWheel)
            {
                wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, steering, _vehicleCore.SteerSpeed);
            }

            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = 0.0001f;
                wheel.brakeTorque = 0;
            }

            // Handbrake
            if (handbrake)
            {
                foreach (WheelCollider wheel in wheels)
                {
                    // Don't zero out this value or the wheel completly lock up
                    wheel.motorTorque = 0.0001f;
                    wheel.brakeTorque = _vehicleCore.BrakeForce;
                }
            }
            else if (throttle != 0 && (Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(throttle)))
            {
                foreach (WheelCollider wheel in driveWheel)
                {
                    wheel.motorTorque = throttle * _vehicleCore.MotorTorqueCurve.Evaluate(speed) * _vehicleCore.DiffGearing / driveWheel.Length;
                }
            }
            else if (throttle != 0)
            {
                foreach (WheelCollider wheel in wheels)
                {
                    wheel.brakeTorque = Mathf.Abs(throttle) * _vehicleCore.BrakeForce;
                }
            }

            // Jump
            if (jumping)
            {
                if (!IsGrounded)
                    return;

                rb.velocity += transform.up * _vehicleCore.JumpVel;
            }

            // Boost
            if (boosting && allowBoost && boost > 0.1f)
            {
                rb.AddForce(transform.forward * _vehicleCore.BoostForce);

                boost -= Time.fixedDeltaTime;
                if (boost < 0f) { boost = 0f; }

                if (boostParticles.Length > 0 && !boostParticles[0].isPlaying)
                {
                    foreach (ParticleSystem boostParticle in boostParticles)
                    {
                        boostParticle.Play();
                    }
                }

                if (boostSource != null && !boostSource.isPlaying)
                {
                    boostSource.Play();
                }
            }
            else
            {
                if (boostParticles.Length > 0 && boostParticles[0].isPlaying)
                {
                    foreach (ParticleSystem boostParticle in boostParticles)
                    {
                        boostParticle.Stop();
                    }
                }

                if (boostSource != null && boostSource.isPlaying)
                {
                    boostSource.Stop();
                }
            }

            // Drift
            if (drift && allowDrift)
            {
                Vector3 driftForce = -transform.right;
                driftForce.y = 0.0f;
                driftForce.Normalize();

                if (steering != 0)
                    driftForce *= rb.mass * speed / 7f * throttle * steering / _vehicleCore.SteerAngle;
                Vector3 driftTorque = transform.up * 0.1f * steering / _vehicleCore.SteerAngle;


                rb.AddForce(driftForce * _vehicleCore.DriftIntensity, ForceMode.Force);
                rb.AddTorque(driftTorque * _vehicleCore.DriftIntensity, ForceMode.VelocityChange);
            }

            // Downforce
            rb.AddForce(-transform.up * speed * _vehicleCore.Downforce);
        }

        // Reposition the car to the start position
        public void ResetPos()
        {
            transform.position = spawnPosition;
            transform.rotation = spawnRotation;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

       

   

    }
}
