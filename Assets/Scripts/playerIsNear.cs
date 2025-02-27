using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class playerIsNear : ConditionTask {

        public BBParameter<Transform> playerPos;
		public BBParameter<bool> playerMoving;
		public float fleeDistance = 5f;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			return ((Vector3.Distance(playerPos.value.position, agent.transform.position) < fleeDistance) && playerMoving.value);
		}
	}
}