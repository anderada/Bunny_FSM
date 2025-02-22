using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class canSocialize : ConditionTask {

		bool seeking = false;
		Rabbit rabbit;
		Rabbit[] rabbits;

		public BBParameter<Transform> partner;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			rabbit = agent.GetComponent<Rabbit>();
			rabbits = GameObject.FindObjectsOfType<Rabbit>();
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
			seeking = rabbit.socializing;
			if (seeking)
			{
				foreach (Rabbit bun in rabbits){
					if (bun != rabbit && bun.socializing)
					{
						partner.SetValue(bun.transform);
                        return true;
                    }
				}
			}
			return false;
		}
	}
}