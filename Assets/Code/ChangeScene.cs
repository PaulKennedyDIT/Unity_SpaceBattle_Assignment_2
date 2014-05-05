using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{
		if(Application.loadedLevel == 0)
		{
			StartCoroutine ("Change");
		}
	}
	
	void OnLevelWasLoaded(int level)
	{
		StartCoroutine ("Change");
	}
	
	IEnumerator Change()
	{
		// Scene 0
		int current = Application.loadedLevel;
		Debug.Log (current);
		if(current == 0)
		{
			yield return new WaitForSeconds(2.0f);
		}
		
		if(current == 1)
		{
			yield return new WaitForSeconds(11f);
		}
		
		if(current == 2)
		{
			// Scene 2
			yield return new WaitForSeconds(22.0f);
		}
		
		if(current == 3)
		{
			// Scene 2
			yield return new WaitForSeconds(2.0f);
		}

		if(current == 4)
		{
			// Scene 2
			yield return new WaitForSeconds(62.8f);
		}

		if(current == 5)
		{
			// Scene 2
			yield return new WaitForSeconds(7f);
		}
		
		if(current < 6)
		{
			Application.LoadLevel (Application.loadedLevel + 1);
		}
		
		
	}
}
