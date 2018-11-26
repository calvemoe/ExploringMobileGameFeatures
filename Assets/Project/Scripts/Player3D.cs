using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3D : MonoBehaviour {

    //player parameters
    public float speed = 8f;
    public float invincibilityDuration = 10f;
    public float speedUpDuration = 5f;

    //powerups settings
    public float speedUpMultiplier = 2f;
    public float invincibilityScale = 0.5f;
    bool usedInvincibility = false;
    bool usedSpeedUp = false;

    private float invincibilityTimer;
    private float speedUpTimer;

    // Update is called once per frame
    void Update () {

        float speedMultiplier = speedUpTimer > 0 ? speedUpMultiplier : 1f;


        if (Input.GetMouseButton(0))
        {
            Vector2 relativePosition = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

            if (relativePosition.x < 0.5f)
            {
                //move left
                GetComponent<Rigidbody>().velocity = new Vector3(-speed * speedMultiplier, 0, 0);
            }
            else if (relativePosition.x > 0.5f)
            {
                //move right
                GetComponent<Rigidbody>().velocity = new Vector3(speed * speedMultiplier, 0, 0);
            }
        }
        else
        {
            //stay still
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        //keep player within bounce
        float zDistance = Camera.main.transform.position.z - transform.position.z;

        float leftLimit = Camera.main.ScreenToWorldPoint(new Vector3(
            0, 
            0, 
            -zDistance / (Mathf.Cos(Camera.main.transform.localEulerAngles.x * Mathf.Deg2Rad))
        )).x;

        float rightLimit = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width,
            0,
            -zDistance / (Mathf.Cos(Camera.main.transform.localEulerAngles.x * Mathf.Deg2Rad))
        )).x;

        if (transform.position.x < leftLimit)
        {
            transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > rightLimit)
        {
            transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
        }

        //using powerups
        if (invincibilityTimer > 0f)
        {
            invincibilityTimer -= Time.deltaTime;
            transform.localScale = new Vector3(invincibilityScale, invincibilityScale, invincibilityScale);
        }
        else
            transform.localScale = Vector3.one;

        if (speedUpTimer > 0f)
            speedUpTimer -= Time.deltaTime;
    }

    public void ActivateInvincibility()
    {
        if (usedInvincibility)
            return;

        usedInvincibility = true;

        invincibilityTimer = invincibilityDuration;
    }

    public void ActivateSpeedUp()
    {
        if (usedSpeedUp)
            return;

        usedSpeedUp = true;

        speedUpTimer = speedUpDuration;
    }
}
