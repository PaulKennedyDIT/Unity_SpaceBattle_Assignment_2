using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace BGE.States
{
    class AttackingState:State
    {
        float timeShot = 0.25f;
		GameObject target = null;
		Color col;
        public override string Description()
        {
            return "Attacking State";
        }

        public AttackingState(GameObject entity,GameObject Etarget,Color shipCol):base(entity)
        {
			this.target = Etarget;
			this.col = shipCol;
			entity.GetComponent<SteeringBehaviours>().TurnOffAll();
			entity.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
			entity.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			entity.GetComponent<SteeringBehaviours>().offset = new Vector3(0, 0, 5);
			entity.GetComponent<SteeringBehaviours> ().leader = target;
        }

        public override void Enter()
        {
            entity.GetComponent<SteeringBehaviours>().TurnOffAll();
            entity.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
            entity.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
            entity.GetComponent<SteeringBehaviours>().offset = new Vector3(0, 0, 5);
			entity.GetComponent<SteeringBehaviours> ().leader = target;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            float range = 500.0f;
            timeShot += Time.deltaTime;
            float fov = Mathf.PI / 6.0f;
            // Can I see the leader?
			GameObject leader = target;
           
			if ((leader.transform.position - entity.transform.position).magnitude < range)
            {
                float angle;
                Vector3 toEnemy = (leader.transform.position - entity.transform.position);
                toEnemy.Normalize();
                angle = (float) Math.Acos(Vector3.Dot(toEnemy, entity.transform.forward));
                if (angle < fov)
                {
                    if (timeShot > 0.5f)
                    {
						if(leader.tag == "enemyLeader")
						{
			                GameObject lazer = new GameObject();
							lazer.AddComponent<goodLazer>();
							lazer.GetComponent<goodLazer>().SetColor(this.col);
							lazer.transform.position = entity.transform.position;
			                
							lazer.transform.LookAt(lazer.transform.position + entity.transform.rotation * Vector3.forward,entity.transform.rotation * Vector3.up);
			                timeShot = 0.0f;
			                //entity.GetComponent<AudioSource>().Play();
						}

						if(leader.tag == "leader")
						{
							GameObject lazer = new GameObject();
							lazer.AddComponent<goodLazer>();
							lazer.GetComponent<goodLazer>().SetColor(this.col);
							lazer.transform.position = entity.transform.position + new Vector3(-25,15,0);
							
							lazer.transform.LookAt(lazer.transform.position + entity.transform.rotation * Vector3.forward,entity.transform.rotation * Vector3.up);
							timeShot = 0.0f;
							//entity.GetComponent<AudioSource>().Play();
						}
                    }
                }
            }
        }

    }
}
