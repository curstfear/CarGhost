using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptionsButton : MonoBehaviour
{
    [SerializeField] private string _menuSceneName;
    [SerializeField] private GameObject _menuPanel;

    private bool _menuPanelIsOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _menuPanelIsOpen == false)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _menuPanelIsOpen)
        {
            UnpauseGame();
        }
    }

    public void Resume()
    {
        UnpauseGame();
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        SceneManager.LoadScene(_menuSceneName);
    }


    private void PauseGame()
    {
        _menuPanel.SetActive(true);
        _menuPanelIsOpen = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UnpauseGame()
    {
        _menuPanel.SetActive(false);
        _menuPanelIsOpen = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
