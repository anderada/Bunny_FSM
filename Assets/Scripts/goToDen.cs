using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class goToDen : ActionTask {

        public BBParameter<Transform> denLocation;


        UnityEngine.Vector3 target;
        float stopDistance = 3f;
        float hopFrequency = 0.2f;
        float hopHeight = 5f;
        float clock = 0f;

        Transform agentMesh;
        NavMeshAgent navAgent;
        Transform tiredSign;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();
            foreach (Transform child in agent.transform)
            {
                if (child.tag == "Rabbit")
                {
                    agentMesh = child;
                }
                if (child.tag == "Tired")
                {
                    tiredSign = child;
                }
            }
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            target = denLocation.value.position;
            navAgent.SetDestination(target);
            tiredSign.gameObject.SetActive(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            clock += Time.deltaTime;
            if (Vector3.Distance(target, agent.transform.position) > stopDistance)
            {
                agentMesh.position = agent.transform.position + new Vector3(0, hopHeight * (Mathf.Abs((clock % hopFrequency) - hopFrequency / 2f)), 0);
            }
            else
            {
                agentMesh.position = agent.transform.position;
                EndAction(true);
            }
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {
            navAgent.SetDestination(agent.transform.position);
        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }
    }
}