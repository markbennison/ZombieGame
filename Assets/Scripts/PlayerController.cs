using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float walkSpeed = 3f;
	public float runSpeed = 6f;
	public float jumpForce = 9f;

	// private variables
	Rigidbody2D rb;
	SpriteRenderer sr;
	GameObject attackZone;

	float moveSpeed;
	bool running;

	float attackCooldownTimer = 0f;
	float attackCooldownTarget = 0.3f;

	Animator animator;
	const string ANIM_FLOAT_SPEED = "Speed";
	const string ANIM_BOOL_ISGROUNDED = "IsGrounded";
	const string ANIM_BOOL_ISCROUCHED = "IsCrouched";
	const string ANIM_TRIG_ONDEATH = "OnDeath";
	const string ANIM_TRIG_ONRESET = "OnReset";
	const string ANIM_TRIG_ONJUMP = "OnJump";
	const string ANIM_TRIG_ONATTACK = "OnAttack";
	const string ANIM_TRIG_ONHIT = "OnHit";
	const string ANIM_TRIG_ONCHEER = "OnCheer";

	string animationState;
	const string PLAYERIDLE = "PlayerIdle";
	const string PLAYERWALK = "PlayerWalk";
	const string PLAYERRUN = "PlayerRun";
	const string PLAYERCROUCH = "PlayerCrouch";
	const string PLAYERJUMP = "PlayerJump";
	const string PLAYERFALL = "PlayerFall";
	const string PLAYERHIT = "PlayerHit";
	const string PLAYERATTACK = "PlayerAttack";
	const string PLAYERCHEER = "PlayerCheer";
	const string PLAYERDEATH = "PlayerDeath";

	float animationSpeed;
	bool isGrounded;
	bool isCrouched;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();

		SetMoveSpeed(runSpeed);
		attackZone = gameObject.transform.GetChild(0).gameObject;
		attackZone.SetActive(false);
		attackCooldownTimer = attackCooldownTarget;
	}

	void Update()
	{
		GroundedCheck();
		if (!GetComponent<HealthManager>().IsDead())
		{
			RunningCheck();
			Moving();
			Jumping();
			Attacking();
		}
		AnimationSettings();
	}

	void GroundedCheck()
	{
		if (rb.linearVelocity.y == 0)
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}
	}

	void RunningCheck()
	{
		if (Input.GetButtonDown("Fire3") && isGrounded)
		{
			SetMoveSpeed(walkSpeed);
		}
		if (Input.GetButtonUp("Fire3"))
		{
			SetMoveSpeed(runSpeed);
		}
		if (!isGrounded)
		{
			SetMoveSpeed(runSpeed);
		}
	}

	void Moving()
	{
		rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.linearVelocity.y);
		if (rb.linearVelocity.x > 0)
		{
			sr.flipX = false;
			attackZone.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
		}
		if (rb.linearVelocity.x < 0)
		{
			sr.flipX = true;
			attackZone.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0f, 180f, 0f));
		}
	}

	void Crouching()
	{
		if (Input.GetKeyDown(KeyCode.C) && rb.linearVelocity.x < 0.1f && isGrounded)
		{
			isCrouched = true;
		}
		else
		{
			isCrouched = false;
		}
	}

	void Jumping()
	{
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetMouseButton(1)) && isGrounded)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
			animator.SetTrigger(ANIM_TRIG_ONJUMP);
		}
	}

	void Attacking()
	{
		attackCooldownTimer += Time.deltaTime;

		if (attackCooldownTimer >= attackCooldownTarget)
		{
			attackCooldownTimer = attackCooldownTarget;

			if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				attackCooldownTimer = 0f;
				attackZone.SetActive(true);
				animator.SetTrigger(ANIM_TRIG_ONATTACK);
			}
		}
		
	}

	void SetMoveSpeed(float speed)
	{
		if (speed >= runSpeed)
		{
			moveSpeed = runSpeed;
		}
		else
		{
			moveSpeed = walkSpeed;
		}
	}

	void AnimationSettings()
	{
		float relativeSpeed = rb.linearVelocity.x;
		if (relativeSpeed < 0)
		{
			relativeSpeed *= -1;
		}


		if (relativeSpeed >= (runSpeed - 1f)) //run
		{
			animationSpeed = 1f;
		}
		else if (relativeSpeed <= 0.1f) //idle
		{
			animationSpeed = 0f;
		}
		else //assume walk
		{
			animationSpeed = 0.4f;
		}

		animator.SetFloat(ANIM_FLOAT_SPEED, animationSpeed);
		animator.SetBool(ANIM_BOOL_ISGROUNDED, isGrounded);

	}



	void ChangeAnimationState(string newState)
	{
		if (newState == animationState)
		{
			return;
		}
		animator.Play(newState);
		animationState = newState;
	}

	bool IsAnimationPlaying(string stateName)
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
			animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}

