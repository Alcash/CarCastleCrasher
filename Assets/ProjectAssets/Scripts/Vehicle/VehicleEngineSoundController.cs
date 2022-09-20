using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

namespace ProjectCore.Vehicle
{
    public class VehicleEngineSoundController : MonoBehaviour
    {
        [Header("AudioClips")]
        public AudioClip starting;
        public AudioClip rolling;
        public AudioClip stopping;

        [Header("pitch parameter")]
        public float flatoutSpeed = 20.0f;
        [Range(0.0f, 3.0f)]
        public float minPitch = 0.7f;
        [Range(0.0f, 0.1f)]
        public float pitchSpeed = 0.05f;

        private AudioSource source;
        private VehicleController vehicle;

        void Start()
        {
            source = GetComponent<AudioSource>();
            vehicle = GetComponent<VehicleController>();
        }

        void Update()
        {
            if (vehicle.Handbrake && source.clip == rolling)
            {
                source.clip = stopping;
                source.Play();
            }

            if (!vehicle.Handbrake && (source.clip == stopping || source.clip == null))
            {
                source.clip = starting;
                source.Play();

                source.pitch = 1;
            }

            if (!vehicle.Handbrake && !source.isPlaying)
            {
                source.clip = rolling;
                source.Play();
            }

            if (source.clip == rolling)
            {
                source.pitch = Mathf.Lerp(source.pitch, minPitch + Mathf.Abs(vehicle.Speed) / flatoutSpeed, pitchSpeed);
            }
        }
    }
}
