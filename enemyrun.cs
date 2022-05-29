using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyrun : MonoBehaviour
{

	public float speed = 2.5f;
	public float attackRange = 1f;
	public float MoveRange = 3f;

	[SerializeField] private Transform m_GroundCheck;
	[SerializeField] private LayerMask m_WhatIsGround;

	const float k_GroundedRadius = .4f;
	public bool m_Grounded;

	Transform player;
	public Animator anim;
	Rigidbody2D rb;
	follow follow;

	public Transform AttackPoint;
	public LayerMask PlayerLayers;
	bool Attack_Moment = true;



	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	public void Start()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		follow = GetComponent<follow>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

	}
	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			}
		}
	}

	public void Update()
	{   
		follow.LookAtPlayer();
		if (m_Grounded & Attack_Moment)
		{
			Vector2 target = new Vector2(player.position.x, rb.position.y);
			Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
			rb.MovePosition(newPos);
		}
		if(Vector2.Distance(player.position, rb.position) <= attackRange)
        {
			anim.SetTrigger("attack");
			Attack_Moment = false;
        }
		if (Vector2.Distance(player.position, rb.position) <= MoveRange)
		{
			Attack_Moment = true;
		}
		if (Vector2.Distance(player.position, rb.position) >= MoveRange)
		{
			Attack_Moment = false;
		}

	}

	public void AttackEnemy()
    {
		Collider2D colInfo = Physics2D.OverlapCircle(AttackPoint.position, attackRange, PlayerLayers);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHP>().TakeDamage(10);
		}
	}

	public void AttackMove()
    {
		Attack_Moment = true;
    }
}
