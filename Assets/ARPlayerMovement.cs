using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPlayerMovement : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent nav;
    public GameObject moveTarget;

    float camRayLength = 100f;
    int floorMask;

    private void Awake() {
        floorMask = LayerMask.GetMask ("Floor");
        Debug.Log(floorMask);
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0)){
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
            
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                moveTarget.transform.position = floorHit.point;
                
            }
        }
        nav.SetDestination(moveTarget.transform.position);
    }
}
