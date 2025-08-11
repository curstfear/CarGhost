using System;
using System.Collections;
using UnityEngine;

public class GameSceneRoot : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.5f);
        EventBus.Instance.GameSceneLoaded?.Invoke();
    }
}
