using System.Collections;
using System.Collections.Generic;
using Unity.IntegerTime;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    public float sightRange = 8f;
    public float moveSpeed = 4f;
    float attackRange = 2f;

    GameObject player;
    Rigidbody2D rb;
    SpriteRenderer sr;

    float playerSeenDirection = 0f;
    float playerSeenCounter = 0f;
    float playerSeenCooldownTotal = 5f;

    bool attacking = false;
    float attackCounter = 0f;
    float attackCooldownTotal = 0.8f;

    Animator animator;
    const string ANIM_FLOAT_SPEED = "Speed";
    const string ANIM_BOOL_ATTACKING = "Attacking";

    string animationState;
    const string ENEMYIDLE = "EnemyIdle";
    const string ENEMYWALK = "EnemyWalk";
    const string ENEMYATTACK = "EnemyAttack";

    float animationSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Debug.Log(CheckPlayerSameLevel());
        BehaviourOptions();

        AnimationSettings();
    }

    void BehaviourOptions()
    {
        SeenCountdown();

        if (CheckPlayerSameLevel() && TargetInChaseRange() && !TargetInAttackRange())
        {
            //Debug.Log("CHASE");
            Chase();
        }
        
        if (CheckPlayerSameLevel() && TargetInAttackRange() && attackCounter == 0f)
        {
            //Debug.Log("ATTACK");
            Attack();
        }
        else if (playerSeenCounter > 0 && playerSeenDirection != 0)
        {
            //Debug.Log("SEARCH");
            Search();
        }
        else
        {
            //Debug.Log("IDLE");
            Idle();
        }

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


    void Idle()
    {

    }

    void Move()
    {
        rb.velocity = new Vector2(moveSpeed * TargetDirectionNormalised(), rb.velocity.y);
        if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
    }
    void Attack()
    {
        attacking = true;
        Invoke("CheckHit", 0.5f);
    }

    void CheckHit()
    {
        Debug.Log("CHECK");
        if (CheckPlayerSameLevel() && TargetInAttackRange())
        {
            Debug.Log("HIT");
            player.SendMessageUpwards("Hit", 10f);
        }
    }

    void Chase()
    {
        playerSeenDirection = TargetDirectionAndDistance();
        playerSeenCounter = playerSeenCooldownTotal;
        Move();
    }

    void Search()
    {
        if(playerSeenCounter <= 0)
        {
            return;
        }

        Move();
    }

    void SeenCountdown()
    {
        if (playerSeenCounter > 0)
        {
            playerSeenCounter -= Time.deltaTime;
        }
        
        if(playerSeenCounter < 0)
        {
            playerSeenCounter = 0;
        }
    }

    bool CheckPlayerSameLevel()
    {
        float verticalDifference = player.transform.position.y - transform.position.y;
        //Debug.Log("vd: " + verticalDifference);

        if (verticalDifference > 4)
        {
            return false;
        }

        if (verticalDifference < -4)
        {
            return false;
        }

        return true;
    }

    bool TargetInAttackRange()
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

    bool TargetInChaseRange()
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

    float TargetDirectionAndDistance()
    {
        float horizontalDistance = player.transform.position.x - transform.position.x;
        return horizontalDistance;
    }

    float TargetDirectionNormalised()
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

    void AnimationSettings()
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
