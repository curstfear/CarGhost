using System;
using System.Collections;
using UnityEngine;

public class GameSceneRoot : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;

        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        EventBus.Instance.GameSceneLoaded?.Invoke();
    }
}
