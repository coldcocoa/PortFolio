using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public Text scoreText;
    public GameObject player;
    public float score = 0;
    public TextMeshProUGUI deadscoreText;
    public TextMeshProUGUI deadcoinText;
    private PlayerController coinscore;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText();
        player = GameObject.Find("Player").GetComponent<PlayerController>().player;
        coinscore = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        score = Mathf.FloorToInt(player.transform.position.z);
        UpdateScoreText();
        
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Á¡¼ö : " + score.ToString() + "M"; 
    }

    public void DeadScore()
    {
        deadscoreText.text = "ÃÖÁ¾Á¡¼ö : " + score.ToString() + "M";
        deadcoinText.text = "È¹µæ °ñµå : " + coinscore.DeadcoinScore().ToString();
    }
}
