using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private int _delayBeforeStart = 3;
    [SerializeField] private TMP_Text _countText;

    private void Start()
    {
        _countText.enabled = false;
    }

    private void OnEnable()
    {
        EventBus.Instance.GameSceneLoaded += StartCountdown;
    }

    private void OnDisable()
    {
        EventBus.Instance.GameSceneLoaded -= StartCountdown;
    }

    private IEnumerator StartCount()
    {
        int seconds = _delayBeforeStart;
        _countText.text = seconds.ToString();
        _countText.enabled = true;
        while (seconds > 0)
        {
            _countText.text = seconds.ToString();
            yield return new WaitForSeconds(1f);
            --seconds;
        }
        _countText.enabled = false;
        EventBus.Instance.GameStarted?.Invoke();

    }
    private void StartCountdown()
    {
        StartCoroutine(StartCount());
    }
}
