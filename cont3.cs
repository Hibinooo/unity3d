using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cont3 : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    public Transform cam;

    float turnSmooth;
    public float speed = 6f;
    public float turn = 0.1f;


    private Vector3 direction;
    private Vector3 velocity;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jump;
    private float cursorLastPosX;
    private float _cinemachineTargetYaw;
    public GameObject CinemachineCameraTarget;
    public float sens;
    private void Start()
    {

        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {

       
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
       

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmooth, turn);
            if (Input.GetButton("Fire2"))
            {
                
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
            speed = 5;
        }
        else if (direction != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
            speed = 10;
        }

        else if (direction == Vector3.zero)
        {
            Idle();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("speed", 0f, 0.1f, Time.deltaTime);
    }
    private void Walk()
        {
                    
            anim.SetFloat("speed", 0.3f, 0.1f, Time.deltaTime);
        }
   private void Run()
        {   
            anim.SetFloat("speed", 1f, 0.1f, Time.deltaTime);
        }
 
}
