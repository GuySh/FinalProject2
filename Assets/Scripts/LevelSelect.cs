using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System.Xml;

public class LevelSelect : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void restart()		// load the current scene  
	{
		Application.LoadLevel (Application.loadedLevelName);
	}
	
	public void StartLevel1()		// load the PushBlockStaging scene  
	{
		PlayerInit.mode = Modes.Regular;
		Application.LoadLevel ("Level1");
	}
	
	public void StartLevel1NewPlayer()		// load the PushBlockStaging scene  
	{
		
		PlayerInit.mode = Modes.OtherPlayer;
		Application.LoadLevel ("Level1");
		
	}
	
	public void StartLevelSelectedAttributes()		// load the PushBlockStaging scene  
	{
		
		PlayerInit.mode = Modes.SelectedAttributes;
		Application.LoadLevel ("Level1");
		
	}
	
	
	public void StartLevelSChoiceMenu()		// load the PushBlockStaging scene  
	{
		Application.LoadLevel ("ChoiceMenu");
		
	}
	
	public void StartLevelCommunicationMenu()		// load the PushBlockStaging scene  
	{
		Application.LoadLevel ("CommunicationMenu");
		
	}
	
	public void StartLevelMainMenu()		// load the PushBlockStaging scene  
	{
		Application.LoadLevel ("MainMenu");
		
	}
	
	public void StartOtherAvatar1()		// load the PushBlockStaging scene  
	{
		Application.LoadLevel ("otherAvater");
	}
	
}
