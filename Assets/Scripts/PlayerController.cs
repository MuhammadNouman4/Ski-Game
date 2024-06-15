using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool isLastFlagTrigger = false, isLeftFlagTrigger = false, isRightFlagTrigger = false, isStartFlagTrigger = false, isMiddleBoostFlagTrigger=false;
    public GameObject winDialogue, LoseDialogue;

    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    [SerializeField] public int moveSpeed = 50, boostSpeed = 20;
    private Rigidbody body;
    public float speed;
    public float rotationSpeed=20;
    private float vertical;
    private float horizontal;

    float temp,rotatespeed = 30f;
    bool isRotating;
    int horizontalDirection, verticalDirection;

    Animator anim;
    public bool isBoostClick = false;
   public bool isKeyPress =false;
    Rigidbody rb;
   

    void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();   
    }
    void FixedUpdate()
    {
  
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Rotate the sprite about the Y axis in the positive direction
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rotationSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Rotate the sprite about the Y axis in the negative direction
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotationSpeed, Space.World);
        }

        
    }

    void Update()
    {
        if (isBoostClick)
        {
            transform.Translate(Vector3.forward * boostSpeed * Time.deltaTime);
            anim.SetBool("boost", true);
        }
        else
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            anim.SetBool("boost", false);
        }

        if (isStartFlagTrigger)
        {
            TimerStart();
        }
    
        if (isLastFlagTrigger)
        {
           winDialogue.SetActive(true);
        }

        if (isLeftFlagTrigger)
        {
            Debug.Log("Add 1 second");
        }
        else if (isRightFlagTrigger)
        {
            Debug.Log("Add 1 Second");
        }

        if(isMiddleBoostFlagTrigger)
        {
            anim.SetBool("boost", true);
            StartCoroutine(BoostSpeed());
        }
        PlayerMove();
       // RotatePlayer();
      /*  if (Input.GetKeyDown(KeyCode.RightArrow) && !isRotating)
        {
            isRotating = true;
            horizontalDirection = 1;

            verticalDirection = 0;
            temp = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRotating)
        {
            isRotating = true;
            horizontalDirection = -1;

            verticalDirection = 0;
            temp = 0;
        }*/

       
    }

   /* public void RotatePlayer()
    {
       
           // transform.Rotate(Vector3.up * 50 * Time.fixedDeltaTime * horizontalDirection, Space.World);

        // transform.Rotate(Vector3.right * 90 * Time.fixedDeltaTime * verticalDirection, Space.World);
        temp += 50 * Time.fixedDeltaTime;
        
        if (temp > 50)
        {
            temp = 0;
           
            horizontalDirection = 0;
         //   verticalDirection = 0;
            isRotating = false;
        }
        
    }*/

    private void OnTriggerEnter(Collider collider)
    {
      if(collider.gameObject.tag == "LastFlag")
      {
            Debug.Log("Last Flag Trigger");
            isLastFlagTrigger = true;
         
            SetString("Timer", timerText.text);

            StartCoroutine(StopGameTimer());
      }

        if(collider.gameObject.tag == "Flag")
        {
            Debug.Log("TimeStart");
            isStartFlagTrigger=true;

        }

        if (collider.gameObject.tag =="BoostFlag")
        {
            isMiddleBoostFlagTrigger=true;
            StartCoroutine(BoostSpeed());
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "LastFlag")
        {
            Debug.Log("Last Flag Trigger Exit");
        }

        if (collider.gameObject.tag == "BoostFlag")
        {
            //isMiddleBoostFlagTrigger = false;
            StartCoroutine(StopBoostSpeed());
        }
    }

    IEnumerator StopGameTimer()
    {
        yield return new WaitForSeconds(0.2f);
        GetString("Timer");
        Debug.Log("PlayerPrefs:  " + GetString("Timer"));
        StopGame();
    }

    void StopGame()
    {
        Time.timeScale = 0;
    }

    void UnStopGame()
    {
        Time.timeScale = 1;
    }
    public void TimerStart()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
      

    }

    public void SetString(string KeyName, string Value)
    {
        PlayerPrefs.SetString(KeyName, Value);
    }

    public string GetString(string KeyName)
    {
        return PlayerPrefs.GetString(KeyName);
    }

    void PlayerMove()
    {
       // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        rb.AddForce(transform.forward * moveSpeed *Time.deltaTime , ForceMode.Acceleration) ;
    }

  IEnumerator BoostSpeed()
    {
        transform.Translate(Vector3.forward * boostSpeed * Time.deltaTime);
        yield return new WaitForSeconds(0.9f);
        anim.SetBool("boost", false);
       // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
       // StopCoroutine(BoostSpeed());
        //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    }


    public void boostSpeedBtnPress()
    {
        isBoostClick = true;
    }

    public void boostSpeedBtnUnPress()
    {
        isBoostClick = false;
       

    }
    IEnumerator StopBoostSpeed()
    {
        yield return new WaitForSeconds(0.9f);
        isMiddleBoostFlagTrigger = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isRotating = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isRotating = false;
        }
    }

  
}

