using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BGE.States;
using System.Collections;

namespace BGE.Scenarios
{
	public class Third_Scenario : Scenario
	{
		static Vector3 initialPos = Vector3.zero;
		List<GameObject> prefabList = new List<GameObject>();
		List<int> validAxis = new List<int>();
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

			validAxis.Add(10);
			validAxis.Add(40);
			validAxis.Add(70);

			leader = GameObject.Find ("Tauri");
			leader.transform.position = new Vector3(-100, 140, 950);
			leader.AddComponent<SteeringBehaviours>();
			leader.GetComponent<SteeringBehaviours>().offset = new Vector3(-50,40,-400);
			leader.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
			leader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
			//leader.AddComponent<MeshCollider> ();
			//leader.AddComponent<MeshRenderer> ();
			leader.tag = "leader";  

			enemyLeader = CreateBoid(new Vector3(120, 90, 600), auroraPrefab);
			enemyLeader.tag = "enemyLeader";
			if (initialPos == Vector3.zero)
			{
				initialPos = enemyLeader.transform.position;
			}
			Path path = enemyLeader.GetComponent<SteeringBehaviours>().path;
			enemyLeader.GetComponent<SteeringBehaviours>().path.Waypoints.Add(initialPos);
			path.Waypoints.Add(initialPos + new Vector3(300, 0, -300));
			path.Waypoints.Add(initialPos + new Vector3(300, 0, 300));
			path.Waypoints.Add(initialPos + new Vector3(-300, 0, 300));
			path.Waypoints.Add(initialPos + new Vector3(-300, 0, -300));
			path.Looped = true;
			path.draw = false;
			
			enemyLeader.GetComponent<SteeringBehaviours>().TurnOffAll();
			enemyLeader.GetComponent<SteeringBehaviours>().FollowPathEnabled = true;
			enemyLeader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;

			leader.GetComponent<SteeringBehaviours>().leader = enemyLeader;

			// Static Ships
			GameObject fleet = null;
			prefabList.Add (auroraPrefab);
			
			int prefabIndex;
			// Now make a fleet
			int fleetSize = 4;
			float xOff = 160;
			float zOff = -100;
			for (int i = 2; i < fleetSize; i++)
			{
				for (int j = 0; j < i; j++)
				{
					prefabIndex = UnityEngine.Random.Range(0,1);

					int axisIndex = UnityEngine.Random.Range(0,3);
					float z = (i - 1) * zOff;
					Vector3 offset = new Vector3((xOff * (-i / 2.0f)) + (j * xOff), validAxis[axisIndex], z);
					fleet = CreateBoid(new Vector3(enemyLeader.transform.position.x,-80,enemyLeader.transform.position.z) + offset, prefabList[prefabIndex]);
					fleet.GetComponent<SteeringBehaviours>().leader = enemyLeader;
					fleet.GetComponent<SteeringBehaviours>().offset = offset;
					fleet.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
					fleet.tag = "enemyFleet";
				}
			}

			prefabList.RemoveAt (0);
			prefabList.Add (gliderPrefab);
			// Now make a fleet
			fleetSize = 4;
			xOff = 20;
			zOff = -100;
			for (int i = 2; i < fleetSize; i++)
			{
				for (int j = 0; j < i; j++)
				{
					prefabIndex = UnityEngine.Random.Range(0,1);
					
					float z = (i - 1) * zOff;
					
					
					int axisIndex = UnityEngine.Random.Range(0,3);
					Vector3 offset = new Vector3((xOff * (-i / 2.0f)) + (j * xOff),validAxis[axisIndex], z);
					fleet = CreateBoid(new Vector3(leader.transform.position.x,50,leader.transform.position.z) + offset, prefabList[prefabIndex]);
					fleet.GetComponent<SteeringBehaviours>().leader = enemyLeader;
					fleet.GetComponent<SteeringBehaviours>().offset = offset;
					fleet.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
					fleet.AddComponent<MeshRenderer> ();
					fleet.AddComponent<Rigidbody> ();
					fleet.rigidbody.useGravity = false;
					fleet.rigidbody.velocity = new Vector3 (-10 ,0,10);
					fleet.tag = "fleet";
				}
			}

			prefabList.RemoveAt (0);
			prefabList.Add (wraithDartPrefab);
			// Now make a fleet
			fleetSize = 4;
			xOff = 20;
			zOff = -20;
			for (int i = 2; i < fleetSize; i++)
			{
				for (int j = 0; j < i; j++)
				{
					prefabIndex = UnityEngine.Random.Range(0,1);
					
					float z = (i - 1) * zOff;
					
					
					int axisIndex = UnityEngine.Random.Range(0,3);
					Vector3 offset = new Vector3((xOff * (-i / 2.0f)) + (j * xOff),validAxis[axisIndex], z);
					fleet = CreateBoid(new Vector3(leader.transform.position.x,0,leader.transform.position.z) + offset, prefabList[prefabIndex]);
					fleet.GetComponent<SteeringBehaviours>().leader = leader;
					fleet.GetComponent<SteeringBehaviours>().offset = offset;
					fleet.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
					fleet.AddComponent<MeshRenderer> ();
					fleet.AddComponent<Rigidbody> ();
					fleet.rigidbody.useGravity = false;
					fleet.rigidbody.velocity = new Vector3 (-10 ,0,10);
					fleet.tag = "enemyFighter";
				}
				
			}
			prefabList.RemoveAt (0);
			prefabList.Add (wraithPrefab);
			prefabList.Add (asauranPrefab);
			prefabList.Add (tauriPrefab);
		
			// Now make a fleet
			 fleetSize = 5;
			 xOff = 120;
			 zOff = -350;
			for (int i = 2; i < fleetSize; i++)
			{
				for (int j = 0; j < i; j++)
				{
					prefabIndex = UnityEngine.Random.Range(0,3);
					
					float z = (i - 1) * zOff;


					int axisIndex = UnityEngine.Random.Range(0,3);
					Vector3 offset = new Vector3((xOff * (-i / 2.0f)) + (j * xOff),validAxis[axisIndex], z);
					fleet = CreateBoid(new Vector3(leader.transform.position.x,50,leader.transform.position.z) + offset, prefabList[prefabIndex]);
					fleet.GetComponent<SteeringBehaviours>().leader = enemyLeader;
					fleet.GetComponent<SteeringBehaviours>().offset = offset;
					fleet.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
					fleet.AddComponent<MeshRenderer> ();
					fleet.AddComponent<Rigidbody> ();
					fleet.rigidbody.useGravity = false;
					fleet.rigidbody.velocity = new Vector3 (-10 ,0,10);
					fleet.tag = "fleet";
				}
			}

			GameObject camFollower;
			camFollower = CreateCamFollower(leader, new Vector3(478.2176f ,127.5226f,165.9115f));
		
			GroundEnabled(true);
		}

	}
}