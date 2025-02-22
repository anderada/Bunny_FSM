using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class Clean : ActionTask {

        Transform standingMesh;
        Transform sittingMesh;
        public BBParameter<float> dirtyness;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            foreach (Transform child in agent.transform)
            {
                if (child.tag == "Rabbit")
                {
                    standingMesh = child;
                }
                if (child.tag == "Sleeping")
                {
                    sittingMesh = child;
                }
            }
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            //EndAction(true);
            standingMesh.gameObject.SetActive(false);
            sittingMesh.gameObject.SetActive(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            dirtyness.SetValue(dirtyness.value - Time.deltaTime * 4f);
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {
            standingMesh.gameObject.SetActive(true);
            sittingMesh.gameObject.SetActive(false);
        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }
    }
}