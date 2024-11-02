using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    public Text hpText;
    public GameObject bgm;
    
    private Vector3 targetPosition; // 목표 위치
    private bool isMoving = false; // 이동 중인지 여부

    public GameObject NON;
    //public GameObject hitcolor;

    private Score scoreScript;
    //public GameManager speedMgr;
    Rigidbody rb;
    public Text coinText;
    public float coin = 0;

    public float limitX = 1f;
    
    public float booster = 0;
    public float boosterMaxCnt = 4;
    public float boosterCurTime = 0;
    public float boosterCoolTime = 20f;
    public Slider boosterSlider;

    bool isbooster = false;
    bool isGod = false;
    bool isDir250 = true;
    bool isDir1000 = true;
    
    
    public Transform Player; // 플레이어의 Transform
    public GameObject player;
    public GameObject DeadPanel;
    public Text deadText;    

    public int PlayerHP = 2;
    public int PlayerMaxHP = 2;

    public float moveSpeed = 10;
    public float rotateSpeed = 10;
    public float speedUp = 3f;
    public float maxSpeed = 60;

    public float jumpUSpeed = 300f;
    public float jumpDSpeed = 130f;
    public float jumpLimitY = 3f;
    public int jumpCnt = 0;
    public int jumpCntMax = 1;

    public float resurrection = 1;
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
        DEAD, //7
        RESURRECTION //8
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Transform>();
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody>();
        playerMaterial = GetComponent<MeshRenderer>();
        scoreScript = FindObjectOfType<Score>();
        //hitcolor = GetComponentInChildren<Material>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime; // z축 움직이기

        deltaPos = Vector2.zero;

        Checkdir();

        HP();
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
                scoreScript.DeadScore();
                bgm.SetActive(false);
                SoundManager.Instance.DeadSound();
                isGod = true;
                Non();
              break;
            case PLAYERSTATE.RESURRECTION:
                
                DeadPanel.SetActive(false);
                bgm.SetActive(true);
                moveSpeed = 10;
                Invoke("Resurrectionoption", 3f);
                NON.SetActive(true);
                break;

        }
        

        if (Input.touches.Length > 0 )
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

        if (isMoving)
        {
            // 현재 위치와 목표 위치를 보간
            player.transform.position = new Vector3(
                Mathf.Lerp(player.transform.position.x, targetPosition.x, Time.deltaTime * moveSpeed),
                player.transform.position.y,
                player.transform.position.z
            );

            // 목표 위치에 도달했는지 확인
            if (Mathf.Abs(player.transform.position.x - targetPosition.x) < 0.15f)
            {
                isMoving = false; // 이동 완료
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
   
    private void LateUpdate()
    {
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -limitX, limitX),transform.position.y,transform.position.z);
    }
    public void Jump()
    {
       if(playerState != PLAYERSTATE.HIT & playerState != PLAYERSTATE.SLIDE)
        {
            if (jumpCnt < 1)
            {
                playerState = PLAYERSTATE.JUMP;
                SoundManager.Instance.PlayJumpSound();
                if (rb != null)
                {
                    rb.AddForce(Vector3.up * jumpUSpeed, ForceMode.Impulse);
                }
                jumpCnt++;
            }
        }      
    }

    public void Slide()
    {
        if(playerState != PLAYERSTATE.DOWN && playerState != PLAYERSTATE.JUMP)
        {
            playerState = PLAYERSTATE.SLIDE;
            GetComponent<BoxCollider>().size = new Vector3(1f, 0.1f, 1f);
            GetComponent<BoxCollider>().center = new Vector3(0, -0.44f, 0);
            GetComponent<Transform>().localScale = new Vector3(1f, 0.6f, 1f);
        }                           
    }

    public void NonSlide()
    {
        playerState = PLAYERSTATE.IDLE;
        GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);
        GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
        GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);

    }
    public void Right()
    {
        transform.localRotation = Quaternion.Euler(0, 45, 0);
        targetPosition = new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z);
        isMoving = true; // 이동 시작
        //transform.localRotation = Quaternion.Euler(0, 45, 0);       
        //player.transform.position = new Vector3(player.transform.position.x +1, player.transform.position.y,player.transform.position.z);
    }

    public void Left()
    {
        transform.localRotation = Quaternion.Euler(0, -45, 0);
        targetPosition = new Vector3(player.transform.position.x - 1, player.transform.position.y, player.transform.position.z);
        isMoving = true; // 이동 시작
        //transform.localRotation = Quaternion.Euler(0, -45, 0);        
        //player.transform.position = new Vector3(player.transform.position.x - 1, player.transform.position.y, player.transform.position.z);

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
            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("Coin"))
        {
            coinScore();
            DeadcoinScore();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Booster")
        {
            booster++;
            SoundManager.Instance.BoosterGet();
            BoosterGage();
            if (booster >= boosterMaxCnt)
            {               
                isbooster = true;
                SoundManager.Instance.BoosterOn();
            }
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "HPup")
        {
            if (PlayerHP < PlayerMaxHP)
            {
                PlayerHP++;
            }
            
            Destroy(other.gameObject);
            SoundManager.Instance.HPGET();
        }
    }


    public void coinScore()
    {
        SoundManager.Instance.ItemGet();
        coin += 1;
        coinText.text = "골드 : " + coin.ToString();
    }

    public float DeadcoinScore()
    {      
        return coin;
    }

    public void BoosterGage()
    {
        boosterSlider.value += 0.25f;
    }

    public void OnDamaged()
    {
        playerMaterial.material.color = new Color(1, 1, 1, 0.7f);
        SoundManager.Instance.PlayHitSound();
        isGod = true;
        Invoke("OffDamaged", 3);        
    }

    public void OffDamaged()
    {
        playerMaterial.material.color = new Color(1, 1, 1, 1f);
        isGod = false;
        
    }

    public void Checkdir()
    {
        if (transform.position.z > 250f && isDir250 == true)
        {
            moveSpeed = moveSpeed * speedUp;
            isDir250 = false;
        }
        if (transform.position.z > 1000f &&  isDir1000 == true)
        {
            moveSpeed = moveSpeed * speedUp;
            isDir1000 = false;
        }
    }
   
    public void ResurrectionBtn()
    {
       
        if (resurrection >= 1)
        {
            resurrection--;
            playerState = PLAYERSTATE.RESURRECTION;
        }
        
    }
    public void Resurrectionoption()
    {
        
        playerState = PLAYERSTATE.IDLE;
        isGod = false;
    }

    public void Non()
    {
        NON.SetActive(false);
    }

    public void HP()
    {
        hpText.text = "HP   : " + PlayerHP.ToString();
    }
}
