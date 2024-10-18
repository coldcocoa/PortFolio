using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Slider slider;
    public string SceneName = "01_Robby";
    private float time = 0;
    private void Start()
    {
        slider = GameObject.Find("LoadingBar").GetComponent<Slider>();
        StartCoroutine(LoadingAsync());
    }

    IEnumerator LoadingAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            yield return null;
            time += Time.deltaTime;
            if (asyncOperation.progress < 0.9f)
            {
                slider.value = Mathf.Lerp(asyncOperation.progress, 2f, time);
                if (slider.value >= asyncOperation.progress)
                {
                    time = 0;
                }
            }
            else
            {
                slider.value = Mathf.Lerp(asyncOperation.progress, 1f, time);
                if (slider.value >= 0.9f)
                    asyncOperation.allowSceneActivation = true;
            }
        }
    }
}
