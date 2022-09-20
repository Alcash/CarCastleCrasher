using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class SimpleVehicleControl : MonoBehaviour
{
    [SerializeField] private WheelVehicle wheelVehicle;
    private bool isMoving = false;
    [ContextMenu("start move")]
    public void StartMove()
    {
        isMoving = true;
        wheelVehicle.Throttle = 1;
        wheelVehicle.Boost = 1;
    }

}
