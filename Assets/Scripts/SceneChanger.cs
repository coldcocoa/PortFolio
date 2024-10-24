using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
   
    public void SceneChange()
    {
        SceneManager.LoadScene(2);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoRobby()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}
