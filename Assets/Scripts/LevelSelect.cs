﻿using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartLevel1()
	{
		Application.LoadLevel ("Level1");
	}

	public void restart()
	{
		Application.LoadLevel (Application.loadedLevelName);
	}
}
