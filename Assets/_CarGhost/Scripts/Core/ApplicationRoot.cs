using System;
using UnityEngine;

public class ApplicationRoot : MonoBehaviour
{
    [SerializeField] private int _defaultFPS = 144;

    private void Start()
    {
        Setup();
        DontDestroyOnLoad(gameObject);
    }

    private void Setup()
    {
        Time.timeScale = 1f;
        Application.targetFrameRate = _defaultFPS;  
    }
}
