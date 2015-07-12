using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	GameObject go;
	Text messageText;
	public static bool win;

	float messegeTime = 3f;
	bool boolTime;
	// Use this for initialization
	void Start () {
		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
		}
	
		boolTime = true;
		win = false;

		go = GameObject.FindGameObjectWithTag ("MessageText");
		messageText = go.GetComponent <Text>();
		messageText.text = "Find The Blue Flag!";


	}
	
	// Update is called once per frame
	void Update () {

		if (boolTime)
		{
			messegeTime -= Time.deltaTime;
			if (messegeTime< 0)
			{
				messageText.text = "";
				boolTime = false;
			}
		}


		if (win)
		{
			Time.timeScale = 0;
			messageText.text = "Winner!";	
		}
	
	}
}
