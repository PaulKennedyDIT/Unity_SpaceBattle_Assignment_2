using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BGE.States;

namespace BGE.Scenarios
{
	public class Fourth_Scenario : Scenario
	{
		static Vector3 initialPos = Vector3.zero;
		List<GameObject> prefabList = new List<GameObject>();
		GameObject enemyLeader = null;
		AudioSource audio;
		AudioClip clip;
		float timeShot = 0.25f;
		
		public override string Description()
		{
			return "Opening Scene";
		}
		
		public override void Start()
		{
			Params.Load("default.txt");

			float range = Params.GetFloat("world_range");
			
			leader = CreateBoid(new Vector3(0, 0, 0), leaderPrefab);
			leader.GetComponent<SteeringBehaviours>().SeekEnabled = true;
			leader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().seekTargetPos = new Vector3(-1000, 0, 1000);
			leader.AddComponent<Rigidbody> ();
			leader.rigidbody.useGravity = false;
			leader.tag = "leader";
			leader.rigidbody.velocity = new Vector3 (-10 ,0,10);
		
			GameObject camFollower;
			camFollower = CreateCamFollower(leader, new Vector3(0,0,0));
			
			GroundEnabled(true);
		}
		
		public override void Update()
		{
			
		}
	}
}