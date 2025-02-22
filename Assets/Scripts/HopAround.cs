using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions {

	public class HopAround : ActionTask {

		public BBParameter<int> decision;
        public BBParameter<float> dirtyness;
        public BBParameter<float> lonelyness;


        UnityEngine.Vector3 target;
		float stopDistance = 1.5f;
		float wanderRadius = 10f;
		float wanderClock = 0;
		float wanderTime = 1.5f;
		float hopFrequency = 0.2f;
		float hopHeight = 5f;
		float minZ = -7.5f;
		float maxZ = 21.5f;
		float minX = -20.5f;
		float maxX = 5.5f;
		int targets = 0;
		float lonelyTollerance = 0;

		Transform agentMesh;
		NavMeshAgent navAgent;
		Transform tiredSign;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			navAgent = agent.GetComponent<NavMeshAgent>();
			foreach(Transform child in agent.transform){
				if (child.tag == "Rabbit")
				{
					agentMesh = child; 
				}
                if (child.tag == "Tired")
                {
                    tiredSign = child;
                }
            }
			lonelyTollerance = Random.Range(15f,40f);
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//EndAction(true);
			targets = 0;
			decision.SetValue(0);
            target = agent.transform.position;
			wanderClock = wanderTime + Random.Range(0f, wanderTime);
			tiredSign.gameObject.SetActive(false);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			wanderClock -= Time.deltaTime;
            dirtyness.SetValue(dirtyness.value + Time.deltaTime);
			lonelyness.SetValue(lonelyness.value + Time.deltaTime);
            target.y = agent.transform.position.y;
            if (wanderClock <= 0)
			{
				Vector3 circleCentre = agent.transform.position;
				Vector3 randomPoint = Random.insideUnitSphere * wanderRadius;
				target = randomPoint + circleCentre;
                target.y = 0;
				target.z = Mathf.Clamp(target.z, minZ, maxZ);
				target.x = Mathf.Clamp(target.x, minX, maxX);
				targets++;
                navAgent.SetDestination(target);
                wanderClock = wanderTime + Random.Range(0f,wanderTime);
			}
			else if(Vector3.Distance(target, agent.transform.position) > stopDistance)
			{
				agentMesh.position = agent.transform.position + new Vector3(0, hopHeight * (Mathf.Abs((wanderClock % hopFrequency) - hopFrequency/2f)), 0);
			}
			else
			{
                agentMesh.position = agent.transform.position;
            }

			if(targets >= 5)
			{
				decision.SetValue((int)(Random.Range(1f, 2.9999f)));
				navAgent.SetDestination(agent.transform.position);
			}

			if(lonelyness.value >= lonelyTollerance)
			{
				agent.GetComponent<Rabbit>().socializing = true;
			}
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}