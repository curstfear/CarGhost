using System;
using Ashsvp;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SimcadeVehicleController _vehicleController;
    private GearSystem _gearSystem;
    private float _playerCarAcceleration;

    private void Start()
    {
        _vehicleController = GetComponent<SimcadeVehicleController>();
        _gearSystem = GetComponent<GearSystem>();

        _playerCarAcceleration = _vehicleController.Acceleration;
        _vehicleController.Acceleration = 0;
    }

    private void OnEnable()
    {
        EventBus.Instance.GameStarted += () => _vehicleController.Acceleration = _playerCarAcceleration;
        EventBus.Instance.PlayerFinished += OnPlayerFinished;
    }
    private void OnDisable()
    {
        EventBus.Instance.GameStarted -= () => _vehicleController.Acceleration = _playerCarAcceleration;
        EventBus.Instance.PlayerFinished -= OnPlayerFinished;
    }

    private void OnPlayerFinished()
    {
        _vehicleController.Acceleration = 0;
        _gearSystem.carSpeed = 0;
    }
}
