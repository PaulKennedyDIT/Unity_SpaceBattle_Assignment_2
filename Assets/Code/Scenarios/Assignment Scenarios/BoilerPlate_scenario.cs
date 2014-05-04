using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BGE.States;

namespace BGE.Scenarios
{
	public class BoilerPlate_Scenario : Scenario
	{
		static Vector3 initialPos = Vector3.zero;
		List<GameObject> prefabList = new List<GameObject>();
		GameObject enemyLeader = null;
		AudioSource audio;
		AudioClip clip;
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
			leader.GetComponent<SteeringBehaviours>().ArriveEnabled = true;
			leader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().seekTargetPos = new Vector3(100, 90, 600);
			
			leader.tag = "leader";
			leader.AddComponent<Rigidbody> ();
			leader.rigidbody.useGravity = false;
			
			audio = leader.AddComponent<AudioSource>();
			AudioClip clip = Resources.Load<AudioClip>("Audio/lazer");
			audio.loop = false;
			audio.clip = clip;       
			
			enemyLeader = CreateBoid(new Vector3(100, 90, 1000), auroraPrefab);
			enemyLeader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			enemyLeader.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
			enemyLeader.tag = "enemyLeader";
			enemyLeader.AddComponent<MeshCollider> ();
			
			
			leader.AddComponent<StateMachine>();
			leader.GetComponent<StateMachine>().SwicthState(new AttackingState(leader,enemyLeader,Color.yellow));
			
			
			GameObject camFollower;
			camFollower = CreateCamFollower(leader, new Vector3(100,10, 300));
			
			GameObject fleet = null;
			prefabList.Add (wraithPrefab);
			prefabList.Add (gliderPrefab);
			prefabList.Add (tauriPrefab);
			prefabList.Add (asauranPrefab);
			
			int prefabIndex;
			// Now make a fleet
			int fleetSize = 5;
			float xOff = 150;
			float zOff = -150;
			for (int i = 2; i < fleetSize; i++)
			{
				for (int j = 0; j < i; j++)
				{
					prefabIndex = UnityEngine.Random.Range(0,4);
					
					float z = (i - 1) * zOff;
					Vector3 offset = new Vector3((xOff * (-i / 2.0f)) + (j * xOff), UnityEngine.Random.Range(-60,60), z);
					fleet = CreateBoid(new Vector3(leader.transform.position.x,leader.transform.position.y,leader.transform.position.z) + offset, prefabList[prefabIndex]);
					fleet.GetComponent<SteeringBehaviours>().leader = leader;
					fleet.GetComponent<SteeringBehaviours>().offset = offset;
					fleet.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().ArriveEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().seekTargetPos = new Vector3(100, 90, 600);
					fleet.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
					fleet.GetComponent<SteeringBehaviours>().target = leader;
					fleet.AddComponent<Rigidbody> ();
					fleet.rigidbody.useGravity = false;
				}
			}
			
			GroundEnabled(true);
		}
		
		public override void Update()
		{
			if (Vector3.Distance(leader.transform.position, enemyLeader.transform.position) < 400)
			{
				Debug.Log("Point Reached");
				if (initialPos == Vector3.zero)
				{
					initialPos = leader.transform.position;
				}
				
				Path path = leader.GetComponent<SteeringBehaviours>().path;
				leader.GetComponent<SteeringBehaviours>().path.Waypoints.Add(initialPos);
				path.Waypoints.Add(initialPos + new Vector3(100, 0, 0));
				path.Waypoints.Add(initialPos + new Vector3(100, 0, 700));
				path.Waypoints.Add(initialPos + new Vector3(-100, 0, 700));
				path.Waypoints.Add(initialPos + new Vector3(-100, 0, 0));
				path.Looped = true;
				path.draw = true;
				
				leader.GetComponent<SteeringBehaviours>().TurnOffAll();
				leader.GetComponent<SteeringBehaviours>().FollowPathEnabled = true;
				leader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			}
		}
	}
}