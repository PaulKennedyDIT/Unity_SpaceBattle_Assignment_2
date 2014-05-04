using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BGE.States;

namespace BGE.Scenarios
{
	public class Third_Scenario : Scenario
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
			
			leader = CreateBoid(new Vector3(120, 90, 900), leaderPrefab);
			leader.GetComponent<SteeringBehaviours>().offset = new Vector3(-5,0,-200);
			leader.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
			leader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			leader.AddComponent<MeshCollider> ();
			leader.tag = "leader";

			audio = leader.AddComponent<AudioSource>();
			AudioClip clip = Resources.Load<AudioClip>("Audio/lazer");
			audio.loop = false;
			audio.clip = clip;       

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
			//leader.AddComponent<StateMachine>();
			//leader.GetComponent<StateMachine>().SwicthState(new AttackingState(leader,enemyLeader,Color.yellow));

			GameObject camFollower;
			camFollower = CreateCamFollower(leader, new Vector3(0,0,0));
		
			GroundEnabled(true);
		}

		public override void Update()
		{
			leader = GameObject.FindGameObjectWithTag ("leader");
			enemyLeader = GameObject.FindGameObjectWithTag ("enemyLeader");
			float speed = 20.0f;
			float range = 500.0f;
			timeShot += Time.deltaTime;
			float fov = Mathf.PI / 8.0f;
			// Can I see the leader?

			if ((enemyLeader.transform.position - leader.transform.position).magnitude < range)
			{
				float angle;
				Vector3 toEnemy = (enemyLeader.transform.position - leader.transform.position);
				toEnemy.Normalize();
				angle = (float) Math.Acos(Vector3.Dot(toEnemy, leader.transform.forward));
				if (angle < fov)
				{
					if (timeShot > 0.5f)
					{
						if(leader.tag == "leader")
						{
							GameObject lazer = new GameObject();
							lazer.AddComponent<goodLazer>();
							lazer.GetComponent<goodLazer>().SetColor(Color.yellow);
							lazer.transform.position = leader.transform.position + new Vector3(40,15,0);
							lazer.transform.forward = leader.transform.forward;
							//lazer.transform.rotation = Quaternion.LookRotation(leader.transform.forward);
							//lazer.transform.position = Vector3.Lerp(leader.transform.position + new Vector3(40,15,0), enemyLeader.transform.position, speed*Time.deltaTime);
							//lazer.transform.LookAt(lazer.transform.position + leader.transform.rotation * Vector3.forward,leader.transform.rotation * Vector3.up);
							timeShot = 0.0f;
							//entity.GetComponent<AudioSource>().Play();
						}
					}
				}
			}

		}
	}
}