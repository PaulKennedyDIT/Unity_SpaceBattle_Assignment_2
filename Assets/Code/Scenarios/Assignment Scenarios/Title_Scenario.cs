using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE.Scenarios
{
	public class Title_Scenario : Scenario
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
			
			leader = CreateBoid(new Vector3(-0, 0, 0), leaderPrefab);
			leader.GetComponent<SteeringBehaviours>().SeekEnabled = true;
			leader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().seekTargetPos = new Vector3(-1000, 0, 1000);
			leader.tag = "leader";
			
			GameObject camFollower;
			camFollower = CreateCamFollower(leader, new Vector3(-78.85415f ,40f, 99.08089f));
			camFollower.transform.Rotate (new Vector3 (0, 90, 0));
			// Create the boids
			GameObject fleet = null;
			
			
			for (int i = 0; i < Params.GetInt("num_tauri"); i++)
			{
				int j = 2;
				if(i % 2 == 0)
				{
					j = -j;
				}
				else
				{
					j = Math.Abs(j);
				}
				
				Vector3 offset = new Vector3(20 * j,0,20 * j);
				Vector3 pos = leader.transform.position + offset;
				fleet = CreateBoid(pos, tauriPrefab);
				fleet.GetComponent<SteeringBehaviours>().leader = leader;
				fleet.GetComponent<SteeringBehaviours>().offset = new Vector3(j * 10,j * 2, 0);
				fleet.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
				fleet.GetComponent<SteeringBehaviours>().seekTargetPos = new Vector3(-1000, 0, 1000);
				fleet.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
				fleet.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
				fleet.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
			}
			
			prefabList.Add (wraithPrefab);
			prefabList.Add (auroraPrefab);
			
			int prefabIndex;
			// Now make a fleet
			int fleetSize = 5;
			float xOff = 90;
			float zOff = -90;
			for (int i = 2; i < fleetSize; i++)
			{
				for (int j = 0; j < i; j++)
				{
					prefabIndex = UnityEngine.Random.Range(0,2);
					
					float z = (i - 1) * zOff;
					Vector3 offset = new Vector3((xOff * (-i / 2.0f)) + (j * xOff), UnityEngine.Random.Range(-50,50), z);
					fleet = CreateBoid(new Vector3(leader.transform.position.x,200,leader.transform.position.z) + offset, prefabList[prefabIndex]);
					fleet.GetComponent<SteeringBehaviours>().leader = leader;
					fleet.GetComponent<SteeringBehaviours>().offset = offset;
					fleet.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().seekTargetPos = new Vector3(-1000, 0, 1000);
					fleet.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
				}
			}
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