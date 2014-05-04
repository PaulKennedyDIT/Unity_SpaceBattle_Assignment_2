#pragma strict

function Start () {
// Scene 0
		var current = Application.loadedLevel;
		
		if(current == 0 || current == 1 || current == 2 || current == 3)
		{
			gameObject.GetComponent(SmoothLookAt).enabled = true;
			gameObject.GetComponent(SmoothFollow).enabled = false;
		}
		
		if(current == 4)
		{
			gameObject.GetComponent(SmoothLookAt).enabled = false;
			gameObject.GetComponent(SmoothFollow).enabled = true;
		}


}

function Update () {

}

function OnLevelWasLoaded(level :int)
{

		// Scene 0
		var current = Application.loadedLevel;
		
		if(current == 0 || current == 1 || current == 2 || current == 3 || current == 5)
		{
			gameObject.GetComponent(SmoothLookAt).enabled = true;
			gameObject.GetComponent(SmoothFollow).enabled = false;
		}
		
		if(current == 4)
		{
			gameObject.GetComponent(SmoothLookAt).enabled = false;
			gameObject.GetComponent(SmoothFollow).enabled = true;
		}

}