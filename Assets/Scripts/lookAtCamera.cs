using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtCamera : MonoBehaviour
{
    Transform cameraPos;


    // Start is called before the first frame update
    void Start()
    {
        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
        cameraPos = camObj.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraPos);
    }
}
