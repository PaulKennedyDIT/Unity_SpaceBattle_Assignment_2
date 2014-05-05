using UnityEngine;
using System.Collections;

public class DestroyRandomShip : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{

	}

	void OnLevelWasLoaded(int level)
	{
		if(Application.loadedLevel == 4)
		{
			InvokeRepeating ("DestroyShip", 20f, 10f);
		}

		if(Application.loadedLevel == 5)
		{
			Invoke ("DestroyFinal", 5f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void DestroyShip()
	{
		GameObject[] enemy = GameObject.FindGameObjectsWithTag ("enemyFleet");

		if(enemy.Length > 0)
		{
			GameObject shroom = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Detonator-MushroomCloud"));
			int index = UnityEngine.Random.Range (0, enemy.Length);
			shroom.transform.position = enemy[index].transform.position;
			DestroyObject (enemy [index]);
		}
		else
		{
			Debug.Log("No More Fleet");
			CancelInvoke("DestroyShip");
			Invoke ("DestroyLeader",17f);
		}
	}

	public void DestroyLeader()
	{
		GameObject leader = GameObject.FindGameObjectWithTag ("enemyLeader");
		GameObject shroom = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Detonator-MushroomCloud"));
		shroom.GetComponent<Detonator> ().size = shroom.GetComponent<Detonator> ().size * 2.0f;
		shroom.transform.position = leader.transform.position;
		//DestroyObject (leader);
	}


	public void DestroyFinal()
	{
		GameObject leader = GameObject.FindGameObjectWithTag ("leader");
		GameObject explosion = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Detonator-MultiExample"));
		explosion.GetComponent<Detonator> ().size = explosion.GetComponent<Detonator> ().size * 100.0f;
		explosion.transform.position = leader.transform.position;
	}
}
