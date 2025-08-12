using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RaceHandler : MonoBehaviour
{
    [SerializeField] private RaceUI _raceUI;
    [SerializeField] private CarSpawner _carSpawner;
    [SerializeField] private float _respawnDelay = 2f;
    [SerializeField] private AudioClip _playerFinishedAudio;
    [SerializeField] private GameObject _ghostPrefab;
    [SerializeField] private GhostRecorder _ghostRecorder;

    private GhostData _lastRaceData;
    private GameObject _currentGhost;
    private AudioSource _audioSource;
    private int _raceNumber = 1;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _raceUI.SetRace(_raceNumber);
        _raceUI.Hide();
    }

    private void OnEnable()
    {
        EventBus.Instance.GameStarted += OnGameStarted;
        EventBus.Instance.PlayerFinished += OnPlayerFinished;
    }

    private void OnDisable()
    {
        EventBus.Instance.GameStarted -= OnGameStarted;
        EventBus.Instance.PlayerFinished -= OnPlayerFinished;
    }

    private void OnGameStarted()
    {
        _raceUI.ShowRace();
        var playerCar = _carSpawner.PlayerCar;

        if (_raceNumber <= 1)
        {
            _ghostRecorder.StartRecording(playerCar.transform);
        }
        else
        {
            SpawnGhost();
        }
    }

    private void SpawnGhost()
    {
        if (_lastRaceData != null && _lastRaceData.positions != null && _lastRaceData.positions.Length > 0)
        {
            if (_currentGhost != null)
            {
                Destroy(_currentGhost);
            }

            // спавним призрака
            Vector3 spawnPosition = _lastRaceData.positions[0];
            spawnPosition.y -= 0.8f; 

            _currentGhost = Instantiate(_ghostPrefab, spawnPosition, _lastRaceData.rotations[0]);

            GhostPlayer ghostPlayer = _currentGhost.GetComponent<GhostPlayer>();
            ghostPlayer.Play(_lastRaceData, 1f);
        }
    }

    private void OnPlayerFinished()
    {
        _raceUI.Hide();
        _raceNumber++;
        _raceUI.SetRace(_raceNumber);
        _audioSource.PlayOneShot(_playerFinishedAudio);
        _lastRaceData = _ghostRecorder.StopRecording();

        // останавливаем призрака если он есть
        if (_currentGhost != null)
        {
            GhostPlayer ghostPlayer = _currentGhost.GetComponent<GhostPlayer>();
            if (ghostPlayer != null)
            {
                ghostPlayer.StopPlayback();
            }
        }

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(_respawnDelay);
        EventBus.Instance.GamePrefareToStart?.Invoke();
        _carSpawner.RespawnCar();
    }
}