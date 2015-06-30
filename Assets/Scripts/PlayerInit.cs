using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class PlayerInit : MonoBehaviour {
	
	// Use this for initialization
	
	public static string mode = "";
	
	void Start () {
		
		if (mode.Equals (Modes.Regular) || mode.Equals ("")) 	// if play with the regular game charecter was chosen
		{
			gameObject.AddComponent<PlayerMovement> ();		// add the regular movement class to the Player object
		}
		else 													//if play new charecter or build your character was chosen
		{
			if (File.Exists (Application.persistentDataPath + "/" + "Info.xml"))	// if data exist
			{
				gameObject.AddComponent<RunTimeCompileManager>();		// add the RunTimeCompileManager object to the Player
			}
			else
			{
				
			}
		}
	}
}
