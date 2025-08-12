using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private string _textAfterStart;
    [SerializeField] AudioClip _signalBeforeStart, _signalAfterStart;
    [SerializeField] private int _delayBeforeStart = 3;
    [SerializeField] private TMP_Text _countText;

    private AudioSource _audioSource;

    private void Start()
    {
        _countText.gameObject.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventBus.Instance.GameSceneLoaded += StartCountdown;
        EventBus.Instance.GamePrefareToStart += StartCountdown;
    }

    private void OnDisable()
    {
        EventBus.Instance.GameSceneLoaded -= StartCountdown;
        EventBus.Instance.GamePrefareToStart -= StartCountdown;
    }

    // корутина для отсчета до начала заезда
    private IEnumerator StartCount()
    {
        int seconds = _delayBeforeStart;

        _countText.text = seconds.ToString();
        _countText.gameObject.SetActive(true);

        while (seconds > 0)
        {
            _countText.text = seconds.ToString();
            _audioSource?.PlayOneShot(_signalBeforeStart);
            yield return new WaitForSeconds(1f);
            --seconds;
        }

        _countText.text = _textAfterStart;
        _audioSource?.PlayOneShot(_signalAfterStart);

        EventBus.Instance.GameStarted?.Invoke();

        yield return new WaitForSeconds(1.5f);
        _countText.gameObject.SetActive(false);
    }

    private void StartCountdown()
    {
        StartCoroutine(StartCount());
    }
}
