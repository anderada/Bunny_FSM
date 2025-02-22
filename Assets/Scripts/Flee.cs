using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions {

	public class Flee : ActionTask {

        public BBParameter<Transform> playerPos;
         

        UnityEngine.Vector3 target;
        float clock = 0f;
        float hopFrequency = 0.2f;
        float hopHeight = 5f;
        float minZ = -7.5f;
        float maxZ = 21.5f;
        float minX = -20.5f;
        float maxX = 5.5f;
        public float fleeDistance = 5f;

        Transform agentMesh;
        NavMeshAgent navAgent;
        Transform startledSign;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            navAgent = agent.GetComponent<NavMeshAgent>();
            target = agent.transform.position;
            foreach (Transform child in agent.transform)
            {
                if (child.tag == "Rabbit")
                {
                    agentMesh = child;
                }
                if (child.tag == "Startled")
                {
                    startledSign = child;
                }
            }
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//EndAction(true);
            startledSign.gameObject.SetActive(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            clock += Time.deltaTime;
            agentMesh.position = agent.transform.position + new Vector3(0, hopHeight * (Mathf.Abs((clock % hopFrequency) - hopFrequency / 2f)), 0);

            target = agent.transform.position + (agent.transform.position - playerPos.value.position).normalized * (fleeDistance + 2f);
            target.y = 0;
            target.z = Mathf.Clamp(target.z, minZ, maxZ);
            target.x = Mathf.Clamp(target.x, minX, maxX);
            navAgent.SetDestination(target);

            if((Vector3.Distance(playerPos.value.position, agent.transform.position) >= fleeDistance))
            {
                EndAction(true);
            }
        }

		//Called when the task is disabled.
		protected override void OnStop() {
            navAgent.SetDestination(agent.transform.position);
            startledSign.gameObject.SetActive(false);
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}