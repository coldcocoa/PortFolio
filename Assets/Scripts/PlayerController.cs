using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GameObject DeadPanel;
    public Text deadText;    

    public int PlayerHP;
    public int PlayerMaxHP;
    public int moveSpeed = 20;
    public float jumpUSpeed = 300f;
    public float jumpDSpeed = 130f;
    public float jumpLimitY = 3f;
    public int jumpCnt = 0;
    public int jumpCntMax = 1;
    
    public PLAYERSTATE playerState;

    
    private Vector2 StartPos = Vector2.zero;
    private Vector2 EndPos = Vector2.zero;
    private Vector2 deltaPos = Vector2.zero;
    public enum PLAYERSTATE
    {
        IDLE = 0,
        JUMP, //1
        DOWN, //2
        HIT, //3
        SLIDE, //4
        LEFT,//5
        RIGHT, //6
        DEAD //7
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime; // z축 움직이기

        deltaPos = Vector2.zero;
        switch (playerState)
        {
            case PLAYERSTATE.IDLE:
                jumpCnt = 0;

                break;
            case PLAYERSTATE.JUMP:
                if(transform.position.y > jumpLimitY)
                {
                    playerState = PLAYERSTATE.DOWN;
                }
                break;
            case PLAYERSTATE.DOWN:
                transform.Translate(0, -jumpDSpeed * Time.deltaTime, 0);
                if(transform.position.y <= 0.5)
                {
                    playerState = PLAYERSTATE.IDLE;
                }
                break;
            case PLAYERSTATE.HIT:
                break;
            case PLAYERSTATE.SLIDE:
                break;
            case PLAYERSTATE.LEFT:             
                break;
            case PLAYERSTATE.RIGHT:            
                break;
            case PLAYERSTATE.DEAD:
                moveSpeed = 0;               
                DeadPanel.SetActive(true);          
                
              break;

        }
        

        if(Input.touches.Length > 0 )
        {
            if (Input.touches[0].phase == TouchPhase.Began )
            {
                StartPos = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                EndPos = Input.touches[0].position;
                deltaPos = StartPos - EndPos;

                if (deltaPos.x < -100)
                    Right();
                else if (deltaPos.x > 100)
                    Left();
                else if (deltaPos.y < -100)
                {
                   
                    Jump();              
                }
            }
            
        }       
    }
    
    public void Jump()
    {

        if (jumpCnt < 1)
        {
            playerState = PLAYERSTATE.JUMP;
            transform.Translate(0, jumpUSpeed * Time.deltaTime, 0);         
            jumpCnt++;
        }
        
    }

    public void Right()
    {
        player.transform.position = new Vector3(player.transform.position.x +1, player.transform.position.y,player.transform.position.z);
    }

    public void Left()
    {
        player.transform.position = new Vector3(player.transform.position.x - 1, player.transform.position.y, player.transform.position.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {           
            playerState = PLAYERSTATE.DEAD;
        }
    }
}
