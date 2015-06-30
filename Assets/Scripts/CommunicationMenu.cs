using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Xml;
using System.Threading;
using System.Net;

public class CommunicationMenu : MonoBehaviour {
	
	
	public GameObject go;
	public Panel p;
	public GameObject goConnectedRed;
	public Image connectedRed;
	public Text myIp;
	public Button button;
	//	StreamReader fileReader = null;
	
	public string connectionIp = "127.0.0.1";   //initial ip	
	public int portNumber = 8632;				//initial ip
	private bool connected = false;				// connected flag
	public InputField portIn, ipIn;				// port and ip from the to set from the input fields
	public string ip = " ";
	
	
	private WWW loadFile;	
	public string rpcString;
	public string classText = " ";
	public string classNameToSend = " ";
	public string xmlFile = "testing ";
	public string getXmlFile = " ";
	
	byte[] xmlBytes;
	public byte[] imageBytes = null;
	
	public static string compilationError = "";
	
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Use this for first initialization
	void Awake()
	{
		
		
		go = GameObject.FindGameObjectWithTag ("CommunicationMenuCanvas");	//find the objct "CommunicationMenuCanvas"
		p = go.GetComponentInChildren<Panel>();								// get the Panel component of the "CommunicationMenuCanvas" object
		
		if (!compilationError.Equals (""))			//If an compilation error occurred a relevant message will be printed on screen 
		{
			p.GetComponentInChildren<Text>().text ="Compilation Error: <<" + compilationError + ">>\nPlease check your files or resend data from other game.";	
			compilationError = "";
		}
		
		go = GameObject.FindGameObjectWithTag ("PortInputField");			//find the objct "PortInputField"
		portIn = go.GetComponentInChildren<InputField>();					// get the InputField component of the "PortInputField" object
		
		go = GameObject.FindGameObjectWithTag ("IpInputField");				//find the objct "IpInputField"
		ipIn = go.GetComponentInChildren<InputField>();						// get the InputFields component of the "CommunicationMenuCanvas" object
		
		goConnectedRed = GameObject.FindGameObjectWithTag ("ConnectedRed");		//find the objct "ConnectedRed"
		connectedRed = goConnectedRed.GetComponentInChildren<Image>();			// get the Image component of the "ConnectedRed" object
		
		go = GameObject.FindGameObjectWithTag ("MyIp");		//find the objct "MyIp"
		myIp = go.GetComponentInChildren<Text>();			// get the Text component of the "MyIp" object
		myIp.text += Network.player.ipAddress;				// set the ip number of this machine
		
		classNameToSend = "PlayerMovement.cs";		// for testing
		rpcString =  Application.productName;		// for connection testing
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Network.connections.Length > 0)			// Check if there is connection and set the connection color to green
		{
			//goConnectedRed.SetActive (false);
			connectedRed.enabled = false;						
			setButtonInteractable("SendAllDataButton", true);
			
		}
		else  										// if there is no connection set the connection color to red
		{
			//goConnectedRed.SetActive (true);
			connectedRed.enabled = true;
			setButtonInteractable("SendAllDataButton", false);
		}
		
		
		if (checkIfFilesExist ())				// if new character data exist enable new character buttons 
		{
			setButtonInteractable("PlayWithNewCharacterButton", true);
			
			setButtonInteractable("BuildYourCharacterButton", true);
		}
		else 									// if new character data do not exist disable new character buttons 
		{
			setButtonInteractable("PlayWithNewCharacterButton", false);
			
			setButtonInteractable("BuildYourCharacterButton", false);
		}
		
	}
	
	public void disconnect()		// disconnect from server
	{
		p.setText ("");
		Network.Disconnect ();
	}
	
	
	private void OnConnectedToServer()	// check if connected to server
	{
		p.setText ("");
		connected = true; 
	}
	
	private void OnServerInitialized()	// when starting server set the connected flag
	{
		connected = true; 
	}
	
	private void OnDisconnectedFromServer()	// check if disonnected 
	{
		connected = false;
	}
	
	
	public void sendPlayerAllData()			// send all character data to other machine
	{
		loadImageBytes ();			// load the character image
		rpcSendImage ();			// send the character image to other machine
		createXml ();				// create the xml file containing all the classes data
		rpcSendXmlButton ();		// send the xml file to other machine
	}
	
	
	public void showMyIp()
	{
		ip = Network.player.ipAddress;		// get the ip of the device and print to screen
		p.setText(ip);
	}
	
	public void connect()		// connect to server
	{
		if (connected)
		{
			return;		
		}
		
		bool portOk = Int32.TryParse(portIn.text, out portNumber);	// get the port from input field
		connectionIp = ipIn.text;						// get the ip from input field
		
		if (portNumber < 1024 || portNumber > 65536)	// port number validation
		{
			portOk = false;
		}
		
		bool ipOk = true;
		try 											// Ip number validation
		{
			IPAddress.Parse(connectionIp);
		}
		catch
		{
			ipOk = false;
			
		}
		
		if (!portOk && !ipOk)							//check port and ip number validation, if not valid print error message to screen
		{
			p.setText ("Error: Port number and Ip number are not valid please try again.");
		}
		else if (!portOk)
		{
			p.setText ("Error: Port number is not valid. please enter a number between 1024 - 65536 and try again.");
		}
		else if(!ipOk)
		{
			p.setText ("Error: Ip number is not valid please try again.");
		}
		
		if (ipOk && portOk)						// if port and ip number are valid, try to connect to server
		{
			p.setText ("Connecting....");
			
			Network.Connect (connectionIp, portNumber);	
			
		}
		
	}
	
	void OnFailedToConnect(NetworkConnectionError info)		// On Failed To Connect print error message to screen
	{
		p.setText ("Could not connect to master server: " + info + ". Please check your ip and port number and try again. Make sure that the host is connected.");
	}
	
	public void host()		// be the server
	{
		if (connected)
		{
			return;		
		}
		
		bool portOk = Int32.TryParse(portIn.text, out portNumber);	//get the port from the input field
		
		if (portNumber < 1024 || portNumber > 65536)		// port number validation
		{
			portOk = false;
		}
		
		if (!portOk)
		{
			p.setText ("Error: Port number is not valid. please enter a number between 1024 - 65536 and try again.");
		}
		else
		{
			Network.InitializeServer (1, portNumber, true);	// start the server
			p.setText ("Now Hosting");
		}
	}
	
	public void printNumOfConnections()		// print the number of connections
	{
		p.setText ("Connections: " + Network.connections.Length.ToString ());
	}
	
	
	public void rpcSendButton()			// send testing string
	{
		if (connected) 
		{
			GetComponent<NetworkView>().RPC ("sendString", RPCMode.OthersBuffered, new object[]{rpcString});
		}
	}
	
	
	public void rpcSendXmlButton()		// send the xml file
	{
		if (connected) 
		{
			GetComponent<NetworkView>().RPC ("sendXml", RPCMode.Others, new object[]{xmlBytes});
		}
		
	}
	
	public void saveXmlFile()		//save the xml on hard disk
	{
		File.WriteAllBytes (Application.persistentDataPath + "/" + "Info.xml", xmlBytes);
	}
	
	
	[RPC]
	public void sendXml(byte[] bytes)		// RPC get the xml file and save it to disk
	{
		xmlBytes = bytes;
		
		if (xmlBytes != null)
		{
			saveXmlFile();		
		}
	}
	
	[RPC]
	public void sendString(string str)		//RPC get the testing string
	{
		p.setText ("In sendString");
		rpcString = str;
	}
	
	[RPC]
	public void sendClassString(string str)		//RPC get the class testing string
	{
		classText = str;
	}
	
	
	[RPC]
	public void sendPlayerImage(byte[] bytes)		//RPC get the image 
	{
		imageBytes = bytes;
		
		if (imageBytes != null)
		{
			saveImageToFile();		
		}
	}
	
	
	public void loadImageBytes()			// load the character image to byte array
	{
		imageBytes = File.ReadAllBytes(Application.dataPath + "/StreamingAssets/" + Modes.PlayerImage + ".png");
	}
	
	public void saveImageToFile()			// save the received image to disk
	{
		Stream fileWriter = null;
		//StreamWriter fileWriter = null;
		string fileName = Application.persistentDataPath + "/" + Modes.PlayerImage + ".png";
		fileWriter = File.Create(fileName);
		for(int i=0; i < imageBytes.Length; i++)
		{
			fileWriter.WriteByte(imageBytes[i]);
		}
		//fileWriter.WriteByte(loadFile.bytes);
		fileWriter.Close();
	}
	
	
	public void rpcSendImage()			//send the image
	{
		if (connected) 
		{
			GetComponent<NetworkView>().RPC ("sendPlayerImage", RPCMode.Others, new object[]{imageBytes});
		}
	}
	
	
	public void printRpcString()	// print the testing string
	{
		p.setText (rpcString);
	}
	
	public void printClassTextString()		// print the testing class string
	{
		p.setText (classText);
	}
	
	public void rpcSendClassText()			// send the test class string
	{
		if (connected) 
		{
			GetComponent<NetworkView>().RPC ("sendClassString", RPCMode.Others, new object[]{classText});
		}
	}
	
	
	public void getClassText()		// sed the string to client
	{
		if (File.Exists (Application.dataPath + "/StreamingAssets/" + classNameToSend))
		{
			classText = File.ReadAllText(Application.dataPath + "/StreamingAssets/" + classNameToSend);
		}
		else
		{
			p.setText ("File not exist");
		}
		
		
	}
	
	
	public void wwwGetfileContent()			// --- testing --- getting test.cs file from android device and print the file content to screen
	{
		loadFile = new WWW("jar:file://" + Application.dataPath + "!/assets/HelloWorld.cs");
		while (!loadFile.isDone) {
		}
		
		
		rpcString = loadFile.text;
		p.setText(loadFile.text);
		
	}
	
	
	public void makeTestFile()			// --- testing --- 
	{
		StreamWriter fileWriter = null;
		string fileName = Application.persistentDataPath + "/" + "HelloWorldTest.cs";
		fileWriter = File.CreateText(fileName);
		fileWriter.WriteLine("Hello world");
		fileWriter.Close();
		
		p.setText (Application.persistentDataPath);
		//p.setText("created HelloWorldTTest file");
		
	}
	
	
	public void addHelloWorldComponent()			// --- testing --- 
	{
		p.setText ("In addHelloWorldComponent");
		//loadFile = new WWW("jar:file://" + Application.dataPath + "!/assets/test.cs");
		//p.setText (loadFile.url);
		if (File.Exists (Application.persistentDataPath + "/" + "HelloWorldTest.cs"))
		{
			p.setText ("File exist");
		}
		else
		{
			p.setText ("Not exist");
			loadFile = new WWW("jar:file://" + Application.dataPath + "!/assets/HelloWorld.cs");
			while (!loadFile.isDone) {
			}
			
			StreamWriter fileWriter = null;
			string fileName = Application.persistentDataPath + "/" + "HelloWorldTest.cs";
			fileWriter = File.CreateText(fileName);
			fileWriter.WriteLine(loadFile.text);
			fileWriter.Close();
			
			
			//File.Copy (loadFile.url, Application.persistentDataPath + "/" + "HelloWorldTest.cs");
		}
		//go.AddComponent<HelloWorld>();
	}
	
	
	public void copyPngPC()			// --- testing --- 		
	{
		p.setText ("In copyJpgPC");
		
		if (File.Exists (Application.persistentDataPath + "/" + Modes.PlayerImage + ".png"))
		{
			p.setText ("File exist \n " + Application.persistentDataPath);
		}
		else
		{
			//p.setText ("Not exist \n " + Application.dataPath + "/StreamingAssets/scaledPositive.png");
			
			byte[] image = File.ReadAllBytes(Application.dataPath + "/" + Modes.PlayerImage + ".png");
			
			if(image != null)
			{
				Debug.Log("image not null");
			}
			
			
			Stream fileWriter = null;
			//StreamWriter fileWriter = null;
			string fileName = Application.persistentDataPath + "/" + Modes.PlayerImage + ".png";
			fileWriter = File.Create(fileName);
			for(int i=0; i < image.Length; i++)
			{
				fileWriter.WriteByte(image[i]);
			}
			//fileWriter.WriteByte(loadFile.bytes);
			fileWriter.Close();
			
			
			//File.Copy (loadFile.url, Application.persistentDataPath + "/" + "HelloWorldTest.cs");
		}
		//go.AddComponent<HelloWorld>();
	} 
	
	
	/*
	public void copyPng()			// --- testing --- 
	{
		p.setText ("In copyJpg");
		//loadFile = new WWW("jar:file://" + Application.dataPath + "!/assets/test.cs");
		//p.setText (loadFile.url);
		if (File.Exists (Application.persistentDataPath + "/" + Modes.PlayerImage + ".png"))
		{
			p.setText ("File exist");
		}
		else
		{
			p.setText ("Not exist");
			loadFile = new WWW("jar:file://" + Application.dataPath + "!/assets/scaledPositive.png");
			while (!loadFile.isDone) {
			}
			
			Stream fileWriter = null;
			//StreamWriter fileWriter = null;
			string fileName = Application.persistentDataPath + "/" + "PositiveCopy.png";
			fileWriter = File.Create(fileName);
			for(int i=0; i < loadFile.bytes.Length; i++)
			{
				fileWriter.WriteByte(loadFile.bytes[i]);
			}
			//fileWriter.WriteByte(loadFile.bytes);
			fileWriter.Close();
			
			
			//File.Copy (loadFile.url, Application.persistentDataPath + "/" + "HelloWorldTest.cs");
		}
		//go.AddComponent<HelloWorld>();
	}
	*/
	
	
	public void createXml()   //create the data xml to be sent
	{
		// create the xml
		XmlTextWriter writer = new XmlTextWriter(Application.persistentDataPath + "/" + "MyInfo.xml", System.Text.Encoding.UTF8);
		writer.WriteStartDocument(true);
		writer.Formatting = Formatting.Indented;
		writer.Indentation = 2;
		
		writer.WriteStartElement("Info");
		
		writer.WriteElementString("Mode", "Regular");
		
		
		string [] fileEntries = Directory.GetFiles(Application.dataPath + "/StreamingAssets/MovementClass");
		// add the movement class
		foreach (string s in fileEntries) 
		{
			if(!s.Contains("meta"))
			{
				string content = File.ReadAllText(s);
				writer.WriteStartElement("MovementClass");
				writer.WriteElementString("ClassName", Path.GetFileNameWithoutExtension(s));
				writer.WriteElementString("ClassCode", content);
				writer.WriteEndElement();
				
			}
		}
		
		
		fileEntries = Directory.GetFiles(Application.dataPath + "/StreamingAssets/CompileFirst");
		// add the compilefirst classes
		foreach (string s in fileEntries) 
		{
			if(!s.Contains("meta"))
			{
				string content = File.ReadAllText(s);
				writer.WriteStartElement("FirstClass");
				writer.WriteElementString("ClassName", Path.GetFileNameWithoutExtension(s));
				writer.WriteElementString("ClassCode", content);
				writer.WriteEndElement();
				
			}
		}
		
		fileEntries = Directory.GetFiles(Application.dataPath + "/StreamingAssets/MovementClasses");
		// add the compilefirst classes
		foreach (string s in fileEntries) 
		{
			if(!s.Contains("meta"))
			{
				string content = File.ReadAllText(s);
				writer.WriteStartElement("Class");
				writer.WriteElementString("ClassName", Path.GetFileNameWithoutExtension(s));
				writer.WriteElementString("ClassCode", content);
				writer.WriteEndElement();
				
			}
		}
		writer.WriteEndElement();
		writer.WriteEndDocument();
		writer.Close();
		
		//get all xml bytes
		xmlFile = File.ReadAllText(Application.persistentDataPath + "/" + "MyInfo.xml");
		xmlBytes = File.ReadAllBytes(Application.persistentDataPath + "/" + "MyInfo.xml");
		//p.setText (xmlFile);
	}
	
	bool checkIfFilesExist()		// check if character data files exist
	{
		return File.Exists (Application.persistentDataPath + "/" + "Info.xml")  && File.Exists(Application.persistentDataPath + "/" + Modes.PlayerImage + ".png");
	}
	
	void setButtonInteractable(string tag , bool interactable) // set button enable/disable
	{
		go = GameObject.FindGameObjectWithTag (tag);		
		button = go.GetComponentInChildren<Button> ();
		button.interactable = interactable;
	}
	
}
