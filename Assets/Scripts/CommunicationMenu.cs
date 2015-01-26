using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CommunicationMenu : MonoBehaviour {


	public GameObject go;
	public Panel p;

	public string connectionIp = "127.0.0.1";		//initial ip
	public int portNumber = 8632;					//initial port
	private bool connected = false;					// connected flag
	public InputField portIn, ipIn;					// port and ip from the to set from the input fields

	private WWW loadFile;	
	public string rpcString;

	// Use this for initialization
	void Start () {
		go = GameObject.FindGameObjectWithTag ("CommunicationMenuCanvas");		//find the objct "CommunicationMenuCanvas"
		p = go.GetComponentInChildren<Panel>();									// get the Panel component of the "CommunicationMenuCanvas" object
			
		go = GameObject.FindGameObjectWithTag ("PortInputField");				//find the objct "PortInputField"
		portIn = go.GetComponentInChildren<InputField>();						// get the InputField component of the "PortInputField" object
		
		go = GameObject.FindGameObjectWithTag ("IpInputField");					//find the objct "IpInputField"
		ipIn = go.GetComponentInChildren<InputField>();							// get the InputFields component of the "CommunicationMenuCanvas" object


		rpcString = "Empty";
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	private void OnconnectedToServer()		// check if connected to server
	{
		connected = true; 
	}
	
	private void OnServerInitialized()		// when starting server set the connected flag
	{
		connected = true; 
	}
	
	private void OnDisconnectedFromServer() // check if disonnected 
	{
		connected = false;
	}





	public void showMyIp()					// get the ip of the device and print to screen
	{
		string ip = Network.player.ipAddress;
		p.setText(ip);
	}

	public void connect()		// connect to server
	{
		if (connected)
		{
			return;		
		}

		Int32.TryParse(portIn.text, out portNumber);			// get the port from input field
		connectionIp = ipIn.text;								// get the ip from input field

		p.setText (connectionIp + " " + portNumber);
		Network.Connect (connectionIp, portNumber);
		//p.setText ("Connctions: " + Network.connections.Length.ToString ());
	}

	public void host()		// be the server
	{
		if (connected)
		{
			return;		
		}

		Int32.TryParse(portIn.text, out portNumber);			//get the port from the input field

		Network.InitializeServer (1, portNumber, true);			// start the server
	}

	public void printNumOfConnections()			// print the number of connections
	{
		p.setText ("Connections: " + Network.connections.Length.ToString ());
	}


	public void rpcSendButton()
	{
		if (connected) 
		{
			networkView.RPC ("sendString", RPCMode.Others, new object[]{rpcString});
		}
	}
	
	
	
	[RPC]
	public void sendString(string str)		// sed the string to client
	{
		rpcString = str;
	}
	
	public void printRpcString()
	{
		p.setText (rpcString);
	}
	
	
	public void wwwGetfileContent()			// --- testing --- getting test.cs file from android device and print the file content to screen
	{
		loadFile = new WWW("jar:file://" + Application.dataPath + "!/assets/test.cs");
		while (!loadFile.isDone) {
		}
		
		
		rpcString = loadFile.text;
		p.setText(loadFile.text);
		
	}

	
}
