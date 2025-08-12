using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private string _gameSceneName;

    public void Play()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
