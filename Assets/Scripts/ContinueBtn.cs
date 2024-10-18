using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ContinueBtn : MonoBehaviour
{
    public int countdownTime;
    public TextMeshProUGUI countdownDisplay;
    //public TextMeshProUGUI countimage;
    

   
    public void CountDown()
    {
        StartCoroutine(CountdownToStart());
        //Invoke("SetactiveText()", 1f);
    }

    IEnumerator CountdownToStart()
    {
        Time.timeScale = 0f;
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;         
        }
        Time.timeScale = 1f;
        countdownDisplay.text = "게임 시작";      
        yield return new WaitForSecondsRealtime(1f); // 1초 대기
        countdownDisplay.text = "";


    }

    /*IEnumerator FadeCoroutine()
    {
        float fadeC = 1f;
        while ( fadeC < 0.05f)
        {
            fadeC -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            countimage.color = new Color(0,0,0,fadeC);
        }
    }*/
    public void stop()
    {
        Time.timeScale = 0;
    }
    public void continueBtn()
    {
        Time.timeScale = 1.0f;
    }
    public void SetactiveText()
    {
        countdownDisplay.text = "";
    }
}
