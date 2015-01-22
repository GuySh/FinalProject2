using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Panel : MonoBehaviour {

	public Text text;
	//connectionMenu cm;
	//public GameObject go;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponentInChildren<Text>();
		//setText("miao miao");
		//go = GameObject.FindGameObjectWithTag ("connectionGameObject");
		//cm = go.GetComponent<connectionMenu>();
	}

	public void setText(string t)
	{
		text.text = t;
	}

	public void getIp()
	{
//		text.text = cm.myIp;
	}

	
	// Update is called once per frame
	void Update () {

	}


}
