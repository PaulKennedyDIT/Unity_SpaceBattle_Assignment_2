using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE.Scenarios
{
	public class Second_Scenario : Scenario
	{
		List<GameObject> prefabList = new List<GameObject>();
		
		public override string Description()
		{
			return "Opening Scene";
		}
		
		public override void Start()
		{

			Params.Load("default.txt");
			float range = Params.GetFloat("world_range");
			
			leader = CreateBoid(new Vector3(100, 90, 100), leaderPrefab);
			leader.GetComponent<SteeringBehaviours>().SeekEnabled = true;
			leader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().seekTargetPos = new Vector3(-1000, 120, 1000);
			leader.tag = "leader";
			leader.AddComponent<Rigidbody> ();
			leader.rigidbody.useGravity = false;
			GameObject camFollower;
			camFollower = CreateCamFollower(leader, new Vector3(-100,10, 90));
			camFollower.transform.Rotate (new Vector3 (0, 180, 0));
			GroundEnabled(true);
		}

		public override void Update()
		{
			leader.rigidbody.velocity = new Vector3 (-200 ,0,200);
		}
	}
}