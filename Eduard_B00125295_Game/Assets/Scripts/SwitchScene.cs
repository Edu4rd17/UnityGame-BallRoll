using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    //when touching the trophy object it will switch to scene2
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(2);
    }
}
