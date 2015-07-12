using UnityEngine;
using System.Collections;

public class MoveLeftRight : MonoBehaviour {
	
	public float speed = 2f; 
	public float total = 200f;
	float now = 0;
	
	public bool left = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (now >= total)
		{
			if(left)
			{
				left = false;
				now = 0;
			}
			else
			{
				left = true;
				now = 0;
			}
		}
		
		if (left)
		{

			transform.position = new Vector3 (transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);
			now += speed;
		}
		else
		{
			transform.position = new Vector3 (transform.position.x - speed*Time.deltaTime, transform.position.y, transform.position.z);
			now += speed;
		}
		
		
	}
}
