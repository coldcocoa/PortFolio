using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>().player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveSpeedControl()
    {      
        GameObject.Find("Player").GetComponent<PlayerController>().moveSpeed = 0;
    }
    
    public void dir()
    {
        
    }
}
