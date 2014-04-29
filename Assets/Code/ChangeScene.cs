using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if(Application.loadedLevel == 0)
		{
			Invoke ("LLoad", 2);
		}

		if(Application.loadedLevel == 1)
		{
			Invoke ("LLoad", 22);
		}

		if(Application.loadedLevel == 2)
		{
			Invoke ("LLoad", 4);
		}

		if(Application.loadedLevel == 3)
		{
			Invoke ("LLoad", 22);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LLoad()
	{
		Application.LoadLevel (Application.loadedLevel + 1);
	}
}
