using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlaneController : MonoBehaviour
{
    public GameObject playerGO;

    float zUpperLimit = 120f;
    float zLowerLimit = 70f;
    float moveSpeed = 5.0f;

    bool isMoveFwd = false;
    bool isMoveBack = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerGO.GetComponent<PlayerController>().isHitBox) 
        {
            if (isMoveBack && !isMoveFwd)
            {
                if (transform.position.z >= zLowerLimit)
                {
                    transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
                }
                else
                {
                    isMoveBack = !isMoveBack;
                    isMoveFwd = !isMoveFwd;
                }
            }

            if (isMoveFwd && !isMoveBack)
            {
                if (transform.position.z <= zUpperLimit)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
                }
                else
                {
                    isMoveBack = !isMoveBack;
                    isMoveFwd = !isMoveFwd;
                }
            }
        }
    }

    //Let player stay on moivng plane
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerGO.transform.parent = transform;
        }
    }

    //Let player stay on final platform
    private void OnTriggerExit(Collider other)
    {
            playerGO.transform.parent = null;
    }
}
