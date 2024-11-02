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

    public void Start()
    {
        countdownTime = 3;
    }

    public void CountDown()
    {
        StartCoroutine(CountdownToStart());
        //Invoke("SetactiveText()", 1f);
        
    }

   /* IEnumerator CountdownToStart()
    {
        Time.timeScale = 0f;
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;         
        }
        Time.timeScale = 1f;
        countdownDisplay.text = "���� ����";      
        yield return new WaitForSecondsRealtime(1f); // 1�� ���
        countdownDisplay.text = "";

        setUPcount();
    }*/

    IEnumerator CountdownToStart()
    {
        float timer = countdownTime; // ������ Ÿ�̸� ���� ���
        while (timer > 0)
        {
            countdownDisplay.text = Mathf.Ceil(timer).ToString(); // �ø��Ͽ� ǥ��

            yield return new WaitForSecondsRealtime(1f);
            timer--;
        }
        Time.timeScale = 1f;
        countdownDisplay.text = "���� ����";
        yield return new WaitForSecondsRealtime(1f); // 1�� ���
        countdownDisplay.text = "";
        
        setUPcount();
    }
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
    public void setUPcount()
    {
        countdownTime = 3;
    }
}
