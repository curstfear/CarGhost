using System;
using UnityEngine;

public class ApplicationRoot : MonoBehaviour
{
    [Header("FPS Options")]
    [SerializeField] private int _defaultFPS = 144;
    [SerializeField] private bool _limitFPS = false;

    private void Start()
    {
        Setup();
        DontDestroyOnLoad(gameObject);
    }

    private void Setup()
    {
        Time.timeScale = 1f;
        if (_limitFPS)
        {
            Application.targetFrameRate = _defaultFPS;
        }
    }
}
