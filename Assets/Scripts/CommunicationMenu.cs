using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CommunicationMenu : MonoBehaviour {


	public GameObject go;
	public Panel p;

	public string connectionIp = "127.0.0.1";
	public int portNumber = 8632;
	private bool connected = false;
	public InputField portIn, ipIn;

	// Use this for initialization
	void Start () {
		go = GameObject.FindGameObjectWithTag ("CommunicationMenuCanvas");
		p = go.GetComponentInChildren<Panel>();

		go = GameObject.FindGameObjectWithTag ("PortInputField");
		portIn = go.GetComponentInChildren<InputField>();

		go = GameObject.FindGameObjectWithTag ("IpInputField");
		ipIn = go.GetComponentInChildren<InputField>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	private void OnconnectedToServer()
	{
		connected = true; 
	}
	
	private void OnServerInitialized()
	{
		connected = true; 
	}
	
	private void OnDisconnectedFromServer()
	{
		connected = false;
	}





	public void showMyIp()
	{
		string ip = Network.player.ipAddress;
		p.setText(ip);
	}

	public void connect()
	{
		if (connected)
		{
			return;		
		}

		Int32.TryParse(portIn.text, out portNumber);
		connectionIp = ipIn.text;

		p.setText (connectionIp + " " + portNumber);
		Network.Connect (connectionIp, portNumber);
		p.setText ("Connctions: " + Network.connections.Length.ToString ());

	}

	public void host()
	{
		if (connected)
		{
			return;		
		}

		Int32.TryParse(portIn.text, out portNumber);

		Network.InitializeServer (1, portNumber, true);
	}

	public void printNumOfConnections()
	{
		p.setText ("Connctions: " + Network.connections.Length.ToString ());
	}

	
}
