using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using BGE;

public class LazerFiring : MonoBehaviour {

	GameObject leader;
	GameObject enemyLeader;
	float timeShot = 0.25f;
	// Use this for initialization
	void Start () 
	{
	
	}

	// Update is called once per frame
	void Update () 
	{
		if(Application.loadedLevel == 4)
		{
			leader = GameObject.FindGameObjectWithTag ("leader");
			enemyLeader = GameObject.FindGameObjectWithTag ("enemyLeader");
			
			Fire (leader, enemyLeader,Color.yellow);
			Fire (enemyLeader, leader,Color.red);
			
			GameObject[] ships = GameObject.FindGameObjectsWithTag ("enemyFleet");
			
			foreach(GameObject ship in ships)
			{
				Fire (leader, ship,Color.yellow);
				Fire (ship, leader,Color.red);
			}
			
			GameObject[] fighters = GameObject.FindGameObjectsWithTag ("enemyFighter");
			
			foreach(GameObject ship in fighters)
			{
				Fire (leader, ship,Color.yellow);
				Fire (ship, leader,Color.red);
			}
			
			GameObject[] ships2 = GameObject.FindGameObjectsWithTag ("fleet");
			
			foreach(GameObject ship in ships2)
			{
				foreach(GameObject eship in ships)
				{
					Fire (ship, eship,Color.green);
					Fire (eship, ship,Color.red);
				}
				
				foreach(GameObject fighter in fighters)
				{
					Fire (ship, fighter,Color.yellow);
					Fire (fighter, ship,Color.red);
				}
				
				Fire (ship,enemyLeader,Color.green);
				Fire (enemyLeader,ship,Color.red);
			}	
		}
	}
	
	public void Fire(GameObject entity,GameObject target,Color col)
	{
		float speed = 20.0f;
		float range = 500.0f;
		timeShot += Time.deltaTime;
		float fov = 5 * Mathf.PI / 180.0f;
		
		if ((entity.transform.position - target.transform.position).magnitude < range) 
		{
			float angle;
			Vector3 toEnemy = (target.transform.position - entity.transform.position);
			toEnemy.Normalize ();
			angle = (float)Math.Acos (Vector3.Dot (toEnemy, entity.transform.forward));
			if (angle < fov) 
			{
				if (timeShot > 0.5f) 
				{
					GameObject lazer = new GameObject ();
					lazer.AddComponent<goodLazer> ();
					lazer.GetComponent<goodLazer> ().SetColor (col);
					lazer.transform.position = entity.transform.position;
					lazer.transform.forward = toEnemy ;
					timeShot = 0.0f;
				}
			}
		}
	}
}
