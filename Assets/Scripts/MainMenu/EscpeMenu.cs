using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EscpeMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputSystem.actions["Escape"].performed += ChangeVisiblility;
        gameObject.SetActive(false);
    }

    private void ChangeVisiblility(InputAction.CallbackContext obj)
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            InputSystem.actions.actionMaps[0].Enable();
        }
        else
        {
            gameObject.SetActive(true);
            InputSystem.actions.actionMaps[0].Disable();
            Time.timeScale = 0;
        }
    }

    public void OnContinue()
    {
        InputSystem.actions.actionMaps[0].Enable();
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        InputSystem.actions.actionMaps[0].Enable();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        InputSystem.actions.actionMaps[0].Enable();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
