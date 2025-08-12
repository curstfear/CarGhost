using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private GameObject _playerCarObject;

    public GameObject PlayerCar
    {
        get
        {
            return _playerCarObject;
        }
    }

    public void RespawnCar()
    {
        if (_playerCarObject.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        _playerCarObject.transform.SetPositionAndRotation(
            _startPoint.position,
            _startPoint.rotation
        );
    }
}
