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
			leader.GetComponent<SteeringBehaviours>().seekTargetPos = new Vector3(-1000, 90, 1000);
			leader.tag = "leader";

			GameObject camFollower;
			camFollower = CreateCamFollower(leader, new Vector3(-95,90, 100));
			camFollower.transform.Rotate (new Vector3 (0, 90, 0));
			GroundEnabled(true);
		}
		
		void Update()
		{
			if(leader.transform.position.x < -250)
			{
				Params.timeModifier *= 1.2f;
			}
		}
	}
}