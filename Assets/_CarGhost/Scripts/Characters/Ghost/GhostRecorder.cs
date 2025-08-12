using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    [SerializeField] private float recordInterval = 0.05f;

    private Transform _target;
    private List<Vector3> _positions = new List<Vector3>();
    private List<Quaternion> _rotations = new List<Quaternion>();
    private List<float> _timeStamps = new List<float>();
    private float _recordStartTime;
    private float _lastRecordTime;
    private bool _isRecording;

    public void StartRecording(Transform target)
    {
        _target = target;
        _positions.Clear();
        _rotations.Clear();
        _timeStamps.Clear();

        _recordStartTime = Time.time;
        _lastRecordTime = Time.time;
        _isRecording = true;

        RecordCurrentPosition();
    }

    public GhostData StopRecording()
    {
        _isRecording = false;

        if (Time.time - _lastRecordTime > recordInterval * 0.5f)
        {
            RecordCurrentPosition();
        }

        if (_positions.Count > 0)
        {
            Debug.Log($"First position: {_positions[0]}, Last position: {_positions[_positions.Count - 1]}");
        }

        return new GhostData(_positions.ToArray(), _rotations.ToArray(), _timeStamps.ToArray());
    }

    private void Update()
    {
        if (!_isRecording || _target == null) return;

        if (Time.time - _lastRecordTime >= recordInterval)
        {
            RecordCurrentPosition();
        }
    }

    private void RecordCurrentPosition()
    {
        Vector3 positionToRecord = _target.position;

        _positions.Add(positionToRecord);
        _rotations.Add(_target.rotation);
        _timeStamps.Add(Time.time - _recordStartTime);
        _lastRecordTime = Time.time;
    }
}