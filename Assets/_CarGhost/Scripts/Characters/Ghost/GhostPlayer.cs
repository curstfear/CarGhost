using System.Collections;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    private Vector3[] _positions;
    private Quaternion[] _rotations;
    private float[] _timeStamps;
    private int _currentIndex = 0;
    private bool _isPlaying = false;
    private float _heightOffset = -0.8f;

    public void Play(GhostData data, float playbackSpeed = 1f)
    {
        _positions = data.positions;
        _rotations = data.rotations;
        _timeStamps = data.timeStamps;
        _currentIndex = 0;
        _isPlaying = true;

        if (_positions != null && _positions.Length > 0)
        {
            Vector3 startPos = _positions[0];
            startPos.y += _heightOffset;
            transform.position = startPos;
            transform.rotation = _rotations[0];
            StartCoroutine(PlaybackRoutine(playbackSpeed));
        }
    }

    private IEnumerator PlaybackRoutine(float playbackSpeed)
    {
        if (_positions == null || _positions.Length <= 1) yield break;

        float startTime = Time.time;

        while (_isPlaying && _currentIndex < _positions.Length - 1)
        {
            float elapsedTime = (Time.time - startTime) * playbackSpeed;

           
            while (_currentIndex < _timeStamps.Length - 1 &&
                   elapsedTime > _timeStamps[_currentIndex + 1])
            {
                _currentIndex++;
            }

            if (_currentIndex >= _positions.Length - 1) break;

            float segmentStartTime = _timeStamps[_currentIndex];
            float segmentEndTime = _timeStamps[_currentIndex + 1];
            float segmentDuration = segmentEndTime - segmentStartTime;

            if (segmentDuration > 0)
            {
                float t = Mathf.Clamp01((elapsedTime - segmentStartTime) / segmentDuration);

                Vector3 currentPos = Vector3.Lerp(_positions[_currentIndex], _positions[_currentIndex + 1], t);
                currentPos.y += _heightOffset;

                Quaternion currentRot = Quaternion.Slerp(_rotations[_currentIndex], _rotations[_currentIndex + 1], t);

                transform.position = currentPos;
                transform.rotation = currentRot;
            }

            yield return null;
        }
    }

    public void StopPlayback()
    {
        _isPlaying = false;
        StopAllCoroutines();
    }
}