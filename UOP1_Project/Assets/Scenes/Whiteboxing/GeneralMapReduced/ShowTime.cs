﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTime : MonoBehaviour
{
	public float currentTime=0;

    // Update is called once per frame
    void Update()
    {
		currentTime = Time.timeSinceLevelLoad;
		Text text=gameObject.GetComponent<Text>();
		text.text = "Time:" + currentTime;
	}
}
