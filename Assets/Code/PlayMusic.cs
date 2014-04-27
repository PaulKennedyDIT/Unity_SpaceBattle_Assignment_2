using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour 
{
	public GameObject music;
	public GameObject g;
	// Use this for initialization
	void Start () {
	
		g = GameObject.FindGameObjectWithTag ("GameController");

		if(!g)
		{
			Instantiate(music,Vector3.zero,Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		g = GameObject.FindGameObjectWithTag ("GameController");
		GameObject l = GameObject.FindGameObjectWithTag ("MainCamera");
		g.transform.position = l.transform.position;
	}
}
