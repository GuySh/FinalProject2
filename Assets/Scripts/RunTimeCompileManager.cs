using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text;
using System;
using System.Reflection;
using System.IO;
using System.Xml;

public class RunTimeCompileManager : MonoBehaviour {
	
	GameObject player;
	
	
	
	
	// Use this for initialization
	void Start () {
		
		string className = "";				// for class naame from the xml file
		string codeFromFile = "";			// for class code from the xml file
		
		if (PlayerInit.mode.Equals (Modes.OtherPlayer))		// if the option "Play with new character" has been chosen
		{
			//read the MovementClass from the xml file
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(Application.persistentDataPath + "/" + "Info.xml");
			XmlNode movementClass = xmlDoc.DocumentElement.SelectSingleNode("/Info/MovementClass");
			XmlNodeList nodes = movementClass.ChildNodes;
			
			foreach (XmlNode node in nodes)
			{
				if(node.Name == "ClassName")
				{
					className = node.InnerText;			// get the class name
					
					
				}
				else if(node.Name == "ClassCode")
				{
					codeFromFile = node.InnerText;		// get the class code
				}
				
			}
			
			compileAndAddCode (codeFromFile, className);		// copile the MovementClass ad add the object to the Player
			setPlayerSprite ();									// change the Player image to the new character image
			
		}
		else if(PlayerInit.mode.Equals (Modes.SelectedAttributes))		// if the option "Build your character" has been chosen
		{
			
			
			foreach(AttributeClass ac in SelectPlayerAttributes.attributesList)		// get the attributes from new character, compile them and add objects to Player
			{
				if(ac.On)	// if attribute was chosen
				{
					compileAndAddCode (ac.Code, ac.Name);
				}
			}
			
			foreach(AttributeClass ac in SelectPlayerAttributes.regularPlayerList)	// get the attributes from regular character, add objects to Player
			{
				if(ac.On)	// if attribute was chosen
				{				
					var t = System.Type.GetType(ac.Name);
					gameObject.AddComponent (t); 
				}
			}
			
			// if attributes from the regular character were chosen, add  "PlayerPreSettings" object
			if(SelectPlayerAttributes.regularOn == true)		
			{
				var t = System.Type.GetType("PlayerPreSettings");
				gameObject.AddComponent (t);
			}
			else   								// if attributes from the regular character were not chosen, compile and add new character PreSettings
			{
				if(SelectPlayerAttributes.PreAC != null)
				{
					compileAndAddCode (SelectPlayerAttributes.PreAC.Code, SelectPlayerAttributes.PreAC.Name);
				}
				else                             		// preSttings class is missing
				{
					CommunicationMenu.compilationError = "PreSettings class not found ";
					Application.LoadLevel ("CommunicationMenu");
					return;
				}
				
			}
			
			if(SelectPlayerAttributes.useNewImage == true)		//if new character image selected
			{
				setPlayerSprite ();
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void compileAndAddCode(string code, string className)			// compile the code and add object to Player object
	{
		CompilerParameters parameters = new CompilerParameters();
		
		System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();		// add relevent assemblies
		foreach (var A in assemblies)
		{
			if(A.FullName.Contains("UnityEngine") || A.FullName.Contains("Assembly-CSharp-firstpass"))
			{
				parameters.ReferencedAssemblies.Add(A.Location);
				
				
			}
		}
		
		CompilerResults results;
		
		// link the CSharpCodeProvider object to Mono compiler 
		Modified.Mono.CSharp.CSharpCodeCompiler compi = new Modified.Mono.CSharp.CSharpCodeCompiler ();
		results = compi.CompileAssemblyFromSource (parameters, code);	// compile the code with the parameters and return the results.
		
		
		if (results.Errors.HasErrors)	// if the compilation had errors get all errors
		{
			
			StringBuilder sb = new StringBuilder();
			
			foreach (CompilerError error in results.Errors)
			{
				sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
			}
			
			CommunicationMenu.compilationError = sb.ToString();  // set the compilation message string in the main menu
			Application.LoadLevel ("MainMenu");         // return to main menu
			return;
			
		}
		
		
		Assembly assembly = results.CompiledAssembly;   // get the assembly
		Type program = assembly.GetType (className);	// get assembly type by class name
		
		
		var t = System.Type.GetType(program.AssemblyQualifiedName);			// get the Assembly Qualified Name
		gameObject.AddComponent (t); // add compiled class object to Player object
		
	}
	
	
	public void GetAllSubTypesVoid()   //<<<<< for testing>>>>>
	{
		//var result = new System.Collections.Generic.List<System.Type>();
		System.Reflection.Assembly[] AS = System.AppDomain.CurrentDomain.GetAssemblies();
		foreach (var A in AS)
		{
			System.Type[] types = A.GetTypes();
			foreach (var T in types)
			{
				if(T.Name.Contains("PlayerController"))
				{
					Debug.Log("THE TYPE: " + T.Name + ",THE ASSEMBLY: " + A.FullName);
				}
				
			}
		}
	}
	
	
	public void setPlayerSprite()			// change the character image to the new character image
	{
		
		player = GameObject.FindGameObjectWithTag ("Player");
		
		// load the image
		byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "/" +Modes.PlayerImage + ".png");
		
		if (bytes == null)
		{
			return;
		}
		
		// load the image to texture object
		Texture2D texture = new Texture2D(80, 80);
		texture.filterMode = FilterMode.Trilinear;
		texture.LoadImage(bytes);
		
		// set the new image
		Sprite sprite = Sprite.Create(texture, new Rect(0,0, 80, 80), new Vector2(.5f,.5f));
		player.GetComponent<SpriteRenderer>().sprite = sprite;
		
	}
	
}
