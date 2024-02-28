using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float walkSpeed = 3f;
	public float runSpeed = 6f;
	public float jumpForce = 9f;

	// private variables
	Rigidbody2D rb;
	SpriteRenderer sr;

	float moveSpeed;
	bool running;

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
	}

	void Update()
	{
		GroundedCheck();
		RunningCheck();
		Moving();
		Jumping();

		AnimationSettings();
	}

	void GroundedCheck()
	{
		if (rb.velocity.y == 0)
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
		rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
		if (rb.velocity.x > 0)
		{
			sr.flipX = false;
		}
		if (rb.velocity.x < 0)
		{
			sr.flipX = true;
		}
	}

	void Crouching()
	{
		if (Input.GetKeyDown(KeyCode.C) && rb.velocity.x < 0.1f && isGrounded)
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
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			animator.SetTrigger(ANIM_TRIG_ONJUMP);
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
		float relativeSpeed = rb.velocity.x;
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

