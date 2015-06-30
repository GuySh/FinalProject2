using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Panel : MonoBehaviour {		// get the text component of any Panel object that compose this class

	public Text text;		// new text pointer
	//connectionMenu cm;
	//public GameObject go;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponentInChildren<Text>();  // get the text object of the current object that compose this class
	}

	public void setText(string t)		// set the text field
	{
		text.text = t;
	}


	// Update is called once per frame
	void Update () {

	}


}
