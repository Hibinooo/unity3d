using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCont : MonoBehaviour
{
    private Animator anim;
    public Camera cam;

    public float damage = 10f;
    public float fireRate = 1f;
    public float range = 14f;
    public float forse = 155f;
    private float nextFire = 0f;

    public Transform PosTarget;
    public float turnSpeed;
    public LayerMask mask;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        
        if (Input.GetButton("Fire2"))
        {
            anim.SetBool("Aim", true);
            Vector3 dir = PosTarget.position - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            PosTarget.position = ray.GetPoint(15);

        }
        else
        {
            anim.SetBool("Aim", false);
        }

        if (Input.GetButton("Fire1"))
        {
            Vector3 dir = PosTarget.position - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            PosTarget.position = ray.GetPoint(15);

        }
        

    }

    void Shoot()
    {

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.collider.tag == "Respawn")
            {
                Debug.Log("попал" + hit.collider);
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * forse);
                }

            }
            
            
        }
    }
}
