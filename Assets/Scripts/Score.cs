using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public Text scoreText;
    public GameObject player;
    public float score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText();
        player = GameObject.Find("Player").GetComponent<PlayerController>().player;

    }

    // Update is called once per frame
    void Update()
    {
        score = Mathf.FloorToInt(player.transform.position.z);
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Á¡¼ö: " + score.ToString(); 
    }
}
