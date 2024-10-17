using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public GameManager speedMgr;
    Rigidbody rb;
    public Text coinText;
    public float coin = 0;

    public float booster = 0;
    public float boosterMaxCnt = 4;
    public float boosterCurTime = 0;
    public float boosterCoolTime = 20f;
    public Slider boosterSlider;
    bool isbooster = false;
    bool isGod = false;

    public GameObject player;
    public GameObject DeadPanel;
    public Text deadText;    

    public int PlayerHP = 2;
    public int PlayerMaxHP = 2;

    public int moveSpeed = 20;
    public float jumpUSpeed = 300f;
    public float jumpDSpeed = 130f;
    public float jumpLimitY = 3f;
    public int jumpCnt = 0;
    public int jumpCntMax = 1;
    
    public PLAYERSTATE playerState;
    MeshRenderer playerMaterial;

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
        rb = GetComponent<Rigidbody>();
        playerMaterial = GetComponent<MeshRenderer>();
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

                rb.AddForce(Vector3.down * jumpDSpeed, ForceMode.Impulse);

                if (transform.position.y <= 0.5)
                {
                    playerState = PLAYERSTATE.IDLE;
                }
                break;
            case PLAYERSTATE.HIT:
                OnDamaged();
                playerState = PLAYERSTATE.IDLE;
                break;
            case PLAYERSTATE.SLIDE:
                Invoke("NonSlide", 1);
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
                else if (deltaPos.y > 100)
                {
                    Slide();
                }
            }                                                                                                                            
            
        }

        if (isbooster == true)
        {
            isGod = true;
            Time.timeScale = 3f;
            boosterCurTime += Time.deltaTime;
            if (boosterCurTime > boosterCoolTime)
            {
                Time.timeScale = 1f;
                isbooster = false;
                booster = 0;
                boosterCurTime = 0;
                isGod = false;
                boosterSlider.value = 0;
            }
        }
        //playerEffect.SetActive(isGod);
    }

    public void Jump()
    {

        if (jumpCnt < 1)
        {
            playerState = PLAYERSTATE.JUMP;
            
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpUSpeed, ForceMode.Impulse);
            }
            jumpCnt++;
        }
        
    }

    public void Slide()
    {
        playerState = PLAYERSTATE.SLIDE;
        GetComponent<BoxCollider>().size = new Vector3(1f, 0.1f,1f);
        GetComponent<BoxCollider>().center = new Vector3(0, -0.44f, 0);
        
    }

    public void NonSlide()
    {
        playerState = PLAYERSTATE.IDLE;
        GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);
        GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
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
        if (other.gameObject.CompareTag("Enemy") && isGod == false)
        {

            PlayerHP--;
            playerState = PLAYERSTATE.HIT;
            if(PlayerHP <= 0)
            {
                Debug.Log("게임 오버");
                playerState = PLAYERSTATE.DEAD;
            }

        }

        if (other.gameObject.CompareTag("Coin"))
        {
            coinScore();
        }

        if (other.gameObject.tag == "Booster")
        {
            booster++;
            BoosterGage();
            if (booster >= boosterMaxCnt)
            {               
                isbooster = true;
            }
        }

        if(other.gameObject.tag == "HPup")
        {
            PlayerHP++;
        }
    }


    public void coinScore()
    {
        coin += 1;
        coinText.text = "골드 : " + coin.ToString();
    }

    public void BoosterGage()
    {
        boosterSlider.value += 0.25f;
    }

    public void OnDamaged()
    {
        playerMaterial.material.color = new Color(1f, 0, 0, 1f);
        isGod = true;
        Invoke("OffDamaged", 3);        
    }

    public void OffDamaged()
    {
        playerMaterial.material.color = new Color(0, 0, 0, 1f);
        isGod = false;
        
    }

   
}
