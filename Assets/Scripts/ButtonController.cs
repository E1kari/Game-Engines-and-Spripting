using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    public string sceneToLoad_;
    // Start is called before the first frame update
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void restartButton()
    {
        SceneManager.LoadScene(sceneToLoad_);
    }

    public void quitButton()
    {
        Application.Quit();
    }
}
