using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Animator playerAnim;

    public GameObject PlayPlaneB;

    public GameObject Timer;

    public GameObject Coin;
    public GameObject CoinCollected;

    private int coinLeft; //total number of coin collected
    private int totalCoin; //total number of coin in the scene currently

    float moveSpeed = 10.0f;

    float fTimerCount;

    float iCount;

    bool isTimerStart = false;
    public bool isHitBox = false;

    // Start is called before the first frame update
    void Start()
    {
        totalCoin = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    // Update is called once per frame
    void Update()
    {
        //Forward
        if(Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            StartRun();
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.SetBool("isRun",false);
        }
        //Backward
        if(Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            StartRun();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.SetBool("isRun", false);
        }
        //Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            StartRun();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.SetBool("isRun", false);
        }
        //Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            StartRun();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.SetBool("isRun", false);
        }
        //Losing Condition
        if(transform.position.y < -5)
        {
            print("You Lose!");
            SceneManager.LoadScene("LoseScene");
        }
        //UI indicating timer to start counting from 0 secs
        if (fTimerCount < 10 && isTimerStart)
        {
            fTimerCount += Time.deltaTime;
            iCount = Mathf.RoundToInt(fTimerCount);
            Timer.GetComponent<Text>().text = "Timer Countup: " + iCount.ToString();
        }
        //Plane B to move back after 10secs
        else if (fTimerCount >= 10 && isTimerStart)
        {
            fTimerCount = 0;
            iCount = 0;
            isTimerStart = false;
            Timer.GetComponent<Text>().text = "Timer Countup: " + iCount.ToString();
            PlayPlaneB.GetComponent<Transform>().Rotate(0, 90, 0);
        }


    }

    //Animation for run and idle
    void StartRun()
    {
        playerAnim.SetBool("isRun",true);
        playerAnim.SetFloat("startRun",0.26f);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // detect contact with cone 
        if (other.gameObject.CompareTag("TagCone"))
        {
            // check if the number if coin collected is 4 
            if (coinLeft == totalCoin && isTimerStart == false)
            {
                // set isTimerStart to true 
                isTimerStart = true;

                // rotate my platform
                Debug.Log("Condition has been made(All 4 power-ups is collected)");
                PlayPlaneB.transform.Rotate(0f, 90f, 0f);
            }
            else
            {
                Debug.Log("Condition has not been accomplished");
            }
        }

        //Increment of coins collected and updating of UI
        if(other.gameObject.CompareTag("Coin"))
        {
            coinLeft++;
            CoinCollected.GetComponent<Text>().text = "Coin Collected: " + coinLeft; //GetComponent of Text and set text with String and Variable
            Destroy(other.gameObject);
        }
    }

    //Trigger for the moving platform to start moving
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("TagBox"))
        {
            Debug.Log("Collided with Box!");
            isHitBox = true;
        }
    }
}
