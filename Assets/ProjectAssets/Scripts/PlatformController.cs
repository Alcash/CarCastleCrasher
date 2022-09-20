using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform m_TransformPitch;
    [SerializeField] private Transform m_TransformYaw;


    [SerializeField] private float m_PitchSpeed;
    [SerializeField] private float m_YawSpeed;

    [SerializeField] private float _pitchValue;
    public float PitchValue { get { return _pitchValue; } set { _pitchValue = value; } }
    [SerializeField] private float _yawValue;
    public float YawValue { get { return _yawValue; } set { _yawValue = value; } }

    private void FixedUpdate()
    {
        m_TransformPitch.localRotation = Quaternion.RotateTowards(m_TransformPitch.localRotation, Quaternion.Euler(-_pitchValue, 0, 0f), m_PitchSpeed);        
        m_TransformYaw.localRotation = Quaternion.RotateTowards(m_TransformYaw.localRotation, Quaternion.Euler(0, _yawValue, 0), m_YawSpeed);
    }


}
