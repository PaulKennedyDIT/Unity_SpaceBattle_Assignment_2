using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE.Scenarios
{
	public class Opening_Scenario_Part_2 : Scenario
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
			camFollower = CreateCamFollower(leader, new Vector3(-108.85415f ,40f,-10f));
			// Create the boids
			GameObject fleet = null;

			prefabList.Add (wraithPrefab);
			prefabList.Add (gliderPrefab);
			prefabList.Add (asauranPrefab);
			prefabList.Add (tauriPrefab);
			
			int prefabIndex;
			// Now make a fleet
			int fleetSize = 8;
			float xOff = 90;
			float zOff = -90;
			for (int i = 2; i < fleetSize; i++)
			{
				for (int j = 0; j < i; j++)
				{
					prefabIndex = UnityEngine.Random.Range(0,4);
					
					float z = (i - 1) * zOff;
					Vector3 offset = new Vector3((xOff * (-i / 2.0f)) + (j * xOff), UnityEngine.Random.Range(-50,50), z);
					fleet = CreateBoid(new Vector3(leader.transform.position.x,300,leader.transform.position.z) + offset, prefabList[prefabIndex]);
					fleet.GetComponent<SteeringBehaviours>().leader = leader;
					fleet.GetComponent<SteeringBehaviours>().offset = offset;
					fleet.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().seekTargetPos = new Vector3(-1000, 0, 1000);
					fleet.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
					fleet.AddComponent<Rigidbody> ();
					fleet.rigidbody.useGravity = false;
					fleet.rigidbody.velocity = new Vector3 (-10 ,0,10);
				}
			}
			GroundEnabled(true);
		}
		
		public override void Update()
		{
			leader = GameObject.FindGameObjectWithTag ("leader");
			if(leader.transform.position.x < -200.0f)
			{
				leader.rigidbody.velocity = new Vector3 (-200 ,0,200);
			}
			
			GameObject[] ships = GameObject.FindGameObjectsWithTag ("boid");
			
			foreach(GameObject ship in ships)
			{
				if(ship.transform.position.x < -210.0f)
				{
					ship.rigidbody.velocity = new Vector3 (-200 ,0,200);
				}
			}
		}
	}
}