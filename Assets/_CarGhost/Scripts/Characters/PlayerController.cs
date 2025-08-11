using System;
using Ashsvp;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SimcadeVehicleController _vehicleController;
    private float _playerCarAcceleration;

    private void Start()
    {
        _vehicleController = GetComponent<SimcadeVehicleController>();
        _playerCarAcceleration = _vehicleController.Acceleration;
        _vehicleController.Acceleration = 0;
    }

    private void OnEnable()
    {
        EventBus.Instance.GameStarted += PlayerSetup;
    }
    private void OnDisable()
    {
        EventBus.Instance.GameStarted -= PlayerSetup;
    }

    private void PlayerSetup()
    {
        _vehicleController.Acceleration = _playerCarAcceleration;   
    }
}
