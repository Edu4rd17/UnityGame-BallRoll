using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string Url = "https://github.com/Edu4rd17/FinalGame-B00125295";
    public void GoToSceneLevel1()
    {
        SceneManager.LoadScene("Eduard_GameLevel1");
    }
    public void LoadSourceCodeUrl()
    {
        Application.OpenURL(Url);
    }
}
