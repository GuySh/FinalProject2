using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

public class SelectPlayerAttributes : MonoBehaviour {

	Canvas selectAttributesCanvas;
	public static List <AttributeClass> attributesList;				// list for all the new character attributes classes
	public static List <AttributeClass> regularPlayerList;			// list for all the regular character attributes classes
	
	public static bool useNewImage;
	
	// options toggles
	GameObject newCharacterToggle;
	GameObject regularCharacterToggle;
	GameObject newCharacterToggleImage;
	
	public static AttributeClass PreAC;		// for the preSettings class of the new character
	
	// to check what kind of atrributes selected
	public static bool regularOn;		
	public static bool selectedOn;
	// Use this for initialization
	void Start ()
	{
		regularOn = false;
		selectedOn = false;
		
		useNewImage = false;
		
		attributesList = new List<AttributeClass>();
		regularPlayerList = new List<AttributeClass>();
		
		// if the xml daata exist, get all the atrributes classes and add the code and class name to the attributesList list
		if (File.Exists (Application.persistentDataPath + "/" + "Info.xml"))		
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load (Application.persistentDataPath + "/" + "Info.xml");
			XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes ("/Info/Class");
			
			string name = "", code = "";
			foreach(XmlNode node in nodes)
			{
				name = node.SelectSingleNode("ClassName").InnerText;
				code = node.SelectSingleNode("ClassCode").InnerText;
				
				if(name.Contains("Pre"))
				{
					PreAC = new AttributeClass(name, code);
				}
				else
				{
					AttributeClass c = new AttributeClass(name, code);
					attributesList.Add(c);
				}
			}
			
		}
		
		//et all the atrributes classes of the regular Character from datafolder and add the class name to the regularPlayerList list
		string[] fileEntries = Directory.GetFiles(Application.dataPath + "/StreamingAssets/MovementClasses");
		if(fileEntries != null)
		{
			foreach (string s in fileEntries) 
			{
				if(!s.Contains("meta"))
				{
					if(!s.Contains("Pre"))
					{
						AttributeClass c = new AttributeClass(Path.GetFileNameWithoutExtension(s), "");
						regularPlayerList.Add(c);
					}
				}
			}
		}
		

		setRegularCharacterToggle ();		// set the regular character Image toggle
		setNewCharacterToggle ();			// set the new character Image toggles
		
		
		
		GameObject panel = GameObject.FindGameObjectWithTag ("PanelNewCharacterAttributes");
		RectTransform panelrectTrans = panel.GetComponent<RectTransform>();
		
		GameObject panelRegular = GameObject.FindGameObjectWithTag ("PanelRegularCharacterAttributes");
		RectTransform panelRegularRectTrans = panelRegular.GetComponent<RectTransform>();
		
		int gap = 30;	// gap between toggles
		int i = 0;
		
		//for each attribute class of the new character create a selection toggle
		foreach(AttributeClass ac in attributesList)
		{
			// create toggle
			GameObject toggle = Instantiate (Resources.Load ("Toggle")) as GameObject; 
			toggle.transform.SetParent (panel.transform);
			Vector3 vec = new Vector3 ();
			vec.x = 1;
			vec.y = 1;
			vec.z = 1;
			toggle.transform.localScale = vec;
			
			
			RectTransform rectTrans = toggle.GetComponent<RectTransform>();
			rectTrans.position = panelrectTrans.position;
			rectTrans.localPosition = panelrectTrans.localPosition;
			
			// set the toggle position
			rectTrans.localPosition = new Vector3(rectTrans.localPosition.x + rectTrans.sizeDelta.x,rectTrans.localPosition.y - rectTrans.sizeDelta.y - (gap*i));
			
			
			// set the toggle off
			Toggle t = toggle.GetComponent<Toggle>();
			t.name = i.ToString();
			t.isOn = false;
			
			Text text = t.GetComponentInChildren<Text>();
			text.text = ac.Name;
			
			// add on click listener to toggle
			t.onValueChanged.AddListener((b) => {selectToggle(t);});
			
			i++;
		}
		
		
		int j = 0;
		//for each attribute class of the regular character create a selection toggle
		foreach(AttributeClass ac in regularPlayerList)
		{
			// create toggle
			GameObject toggle = Instantiate (Resources.Load ("Toggle")) as GameObject; 
			toggle.transform.SetParent (panelRegular.transform);
			Vector3 vec = new Vector3 ();
			vec.x = 1;
			vec.y = 1;
			vec.z = 1;
			toggle.transform.localScale = vec;
			
			
			RectTransform rectTrans = toggle.GetComponent<RectTransform>();
			rectTrans.position = panelRegularRectTrans.position;
			
			// set the toggle position
			rectTrans.localPosition = new Vector3(rectTrans.localPosition.x + rectTrans.sizeDelta.x,rectTrans.localPosition.y - rectTrans.sizeDelta.y - (gap*j));
			
			// set the toggle off
			Toggle t = toggle.GetComponent<Toggle>();
			t.name = j.ToString();
			t.isOn = false;
			
			Text text = t.GetComponentInChildren<Text>();
			text.text = ac.Name;
			
			// add on click listener to toggle
			t.onValueChanged.AddListener((b) => {selectToggleRegular(t);});
			
			j++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// if new character attribute toggle selected, set flag to the attribute in the attributesList
	void selectToggle(Toggle t)
	{
		selectedOn = true;
		int index;
		Int32.TryParse(t.name, out index);
		attributesList [index].On = t.isOn;
	}
	
	// if regular character attribute toggle selected, set flag the to attribute in the regularPlayerList
	void selectToggleRegular(Toggle t)
	{
		regularOn = true;
		int index;
		Int32.TryParse(t.name, out index);
		regularPlayerList [index].On = t.isOn;
	}
	
	// set the image toggle of the new character
	void setNewCharacterToggle ()
	{
		newCharacterToggle = GameObject.FindGameObjectWithTag ("NewCharacterImageToggle");
		Toggle t = newCharacterToggle.GetComponent<Toggle> ();
		t.isOn = false;
		t.name = "newCharacterToggle";
		
		// set on click listener
		t.onValueChanged.AddListener((b) => {selectCharacterImage(t);});
		
		newCharacterToggleImage = GameObject.FindGameObjectWithTag ("NewNewCharacterSetToggleImage");
		
		Image newImage = newCharacterToggleImage.GetComponent<Image> ();
		
		byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "/" + Modes.PlayerImage + ".png");
		Texture2D texture = new Texture2D(80, 80);
		texture.filterMode = FilterMode.Trilinear;
		texture.LoadImage(bytes);
		Sprite sprite = Sprite.Create(texture, new Rect(0,0, 80, 80), new Vector2(.5f,.5f));
		newImage.sprite = sprite;
		
	}
	
	// set the image toggle of the regualar character
	void setRegularCharacterToggle()
	{
		regularCharacterToggle = GameObject.FindGameObjectWithTag ("RegularCharacterToggle");
		Toggle t = regularCharacterToggle.GetComponent<Toggle> ();
		t.name = "regularCharacterToggle";
		
		// set on click listener
		t.onValueChanged.AddListener((b) => {selectCharacterImage(t);});
	}
	
	// when image toggle is selected, turn off the other image toggle
	void selectCharacterImage(Toggle t)
	{
		if (t.name.Equals ("newCharacterToggle")) 
		{
			if(t.isOn)
			{
				useNewImage = true;
				regularCharacterToggle.GetComponent<Toggle> ().isOn = false;
			}
			else
			{
				regularCharacterToggle.GetComponent<Toggle> ().isOn = true;
			}
		}
		else
		{
			if(t.isOn)
			{
				useNewImage = false;
				newCharacterToggle.GetComponent<Toggle> ().isOn = false;
			}
			else
			{
				newCharacterToggle.GetComponent<Toggle> ().isOn = true;
			}
		}
		
	}
	
	// for testing
	void select(bool name)
	{
		Debug.Log (name);
	}
	
}
