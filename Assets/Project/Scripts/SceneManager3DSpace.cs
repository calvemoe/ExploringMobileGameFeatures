﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager3DSpace : MonoBehaviour {

    public Text infoText;
    public Player3D player;

	// Update is called once per frame
	void Update () {
        infoText.text = "No input detected";

        List<Vector2> touchCoordinates = new List<Vector2>();

        //actual device touches
        foreach(Touch touch in Input.touches)
            touchCoordinates.Add(touch.position);

        //dummy touches
       if (Input.GetMouseButton(0))
            touchCoordinates.Add(Input.mousePosition);

        if (Input.GetKey(KeyCode.Space))
            touchCoordinates.Add(new Vector2(42, 42));

        if (Input.GetKey(KeyCode.V))
            touchCoordinates.Add(new Vector2(48, 48));
 
        //print dummy touches on screen
        if(touchCoordinates.Count > 0)
        {
            infoText.text = "";
            for (int i = 0; i < touchCoordinates.Count; i++)
                infoText.text += string.Format("Input {0}: {1}, {2}\n", i + 1, touchCoordinates[i].x, touchCoordinates[i].y);
        }

        //powerups activation: 
        //Invincibility require 2 simultaneous inputs: mouse0 and Space/V OR Space and V
        //SpeedUp require 3 simultaneous inputs: mouse0 and Space and V
        if (touchCoordinates.Count == 2)
            player.ActivateInvincibility();
        else if (touchCoordinates.Count == 3)
            player.ActivateSpeedUp();
    }
}
