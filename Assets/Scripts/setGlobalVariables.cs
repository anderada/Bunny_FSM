using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class setGlobalVariables : MonoBehaviour
{
    GlobalBlackboard blackboard;

    // Start is called before the first frame update
    void Start()
    {
        blackboard = GetComponent<GlobalBlackboard>();    
    }

    // Update is called once per frame
    void Update()
    {
        blackboard.SetVariableValue("playerMoving", blackboard.GetVariableValue<Transform>("player").gameObject.GetComponent<Player>().isMoving);
    }
}
