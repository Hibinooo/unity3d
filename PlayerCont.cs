using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerCont : MonoBehaviour
{
    private Camera cam;
    public NavMeshAgent agent;
   // public ThirdPersonCharacter character;
    void Start()
    {
        cam = Camera.main;
        agent.updateRotation = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
