using System.Collections;
using System.Collections.Generic;
using Unity.IntegerTime;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
	public float sightRange = 8f;
	public float moveSpeed = 4f;
	public float attackRange = 2f;

	protected GameObject player;
	protected Rigidbody2D rb;
	protected SpriteRenderer sr;

	protected float playerSeenDirection = 0f;
	protected float playerSeenCounter = 0f;
	protected float playerSeenCooldownTotal = 5f;

	protected bool attacking = false;
	protected bool canAttackPlayer;
	protected float attackCounter = 0f;
	protected float attackCooldownTotal = 0.8f;

	protected Animator animator;
	protected const string ANIM_FLOAT_SPEED = "Speed";
	protected const string ANIM_BOOL_ATTACKING = "Attacking";

	protected float animationSpeed;

	protected void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();

		player = GameObject.FindGameObjectWithTag("Player");
	}

	protected void Update()
	{
		//Debug.Log(CheckPlayerSameLevel());

		canAttackPlayer = player.GetComponent<HealthManager>().PlayerAlive;

        Behaviour();

		AnimationSettings();
	}

	protected void Behaviour()
	{
		SeenCountdown();

		BehaviourOptions();

		animator.SetBool(ANIM_BOOL_ATTACKING, attacking);
		if (attacking)
		{
			attackCounter += Time.deltaTime;
			if (attackCounter >= attackCooldownTotal)
			{
				attackCounter = 0;
				attacking = false;
			}
		}
	}

	protected virtual void BehaviourOptions()
	{


	}


	protected bool IsMidAttack()
	{
		//attackCounter == 0f
		return attacking;
	}

	protected bool IsPlayerSeenRecently()
	{
		if (playerSeenCounter > 0 && playerSeenDirection != 0)
		{
			return true;
		}
		return false;
	}

	protected void SeenCountdown()
	{
		if (playerSeenCounter > 0)
		{
			playerSeenCounter -= Time.deltaTime;
		}

		if (playerSeenCounter < 0)
		{
			playerSeenCounter = 0;
		}
	}

	protected bool IsPlayerSameLevel()
	{
		float verticalDifference = player.transform.position.y - transform.position.y;
		//Debug.Log("vd: " + verticalDifference);

		if (verticalDifference > 4)
		{
			return false;
		}

		if (verticalDifference < -3.8)
		{
			return false;
		}

		return true;
	}

	protected bool IsTargetInAttackRange()
	{
		float temporaryRange = TargetDirectionAndDistance();

		if (temporaryRange < 0)
		{
			temporaryRange *= -1;
		}

		if (temporaryRange < attackRange)
		{
			return true;
		}

		return false;
	}

	protected bool IsTargetInChaseRange()
	{
		float temporaryRange = TargetDirectionAndDistance();
		if (temporaryRange < 0)
		{
			temporaryRange *= -1;
		}

		if (temporaryRange < sightRange)
		{
			return true;
		}

		return false;
	}

	protected float TargetDirectionAndDistance()
	{
		float horizontalDistance = player.transform.position.x - transform.position.x;
		return horizontalDistance;
	}

	protected float TargetDirectionNormalised()
	{
		if (playerSeenDirection < 0)
		{
			return -1f;
		}
		else if (playerSeenDirection > 0)
		{
			return 1f;
		}

		return 0f;
	}

	protected void AnimationSettings()
	{
		float relativeSpeed = rb.velocity.x;
		if (relativeSpeed < 0)
		{
			relativeSpeed *= -1;
		}


		if (relativeSpeed >= 0.1f) // walk
		{
			animationSpeed = 1f;
		}
		else //idle
		{
			animationSpeed = 0f;
		}

		animator.SetFloat(ANIM_FLOAT_SPEED, animationSpeed);

	}


}
