using UnityEngine;
using System.Collections;

public class AndroidMusic : MonoBehaviour {

	private static AndroidMusic instance = null;
	public static AndroidMusic Instance {
		get { return instance; }
	}
	
	void Awake() {
		
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
