using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class PlayerController : MonoBehaviour
{
    public int PlayerHP;
    public int PlayerMaxHP;
    public int moveSpeed = 20;
    public int jumpUSpeed = 700;
    public float jumpDSpeed = 130f;
    public PLAYERSTATE playerState;

    public enum PLAYERSTATE
    {
        IDLE = 0,
        JUMP, //1
        DOWN, //2
        HIT, //3
        SLIDE, //4
        LEFT,//5
        RIGHT //6
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerState)
        {
            case PLAYERSTATE.IDLE:
                break;
            case PLAYERSTATE.JUMP:
                break;
            case PLAYERSTATE.DOWN:
                break;
            case PLAYERSTATE.HIT:
                break;
            case PLAYERSTATE.LEFT:             
                break;
            case PLAYERSTATE.RIGHT:            
                break;
        }
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime; // z축 움직이기
    
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerHP--;           
            if (PlayerHP < 0)
            {
                Debug.Log("게임오버");
            }
        }
    }
}
