using UnityEngine;

public class EnemyBehaviourComplete : EnemyController
{

    protected override void BehaviourOptions()
    {
        if (IsPlayerSameLevel() && IsTargetInAttackRange() && !IsMidAttack())
        {
            //Debug.Log("ATTACK");
            Attack();
        }
        else if (IsPlayerSameLevel() && IsTargetInChaseRange())
        {
            //Debug.Log("CHASE");
            Chase();
        }
        else if (IsPlayerSeenRecently())
        {
            //Debug.Log("SEARCH");
            Search();
        }
        else
        {
            //Debug.Log("IDLE");
            Idle();
        }
    }

	void Idle()
    {

    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed * TargetDirectionNormalised(), rb.linearVelocity.y);
        if (rb.linearVelocity.x > 0)
        {
            sr.flipX = false;
        }
        if (rb.linearVelocity.x < 0)
        {
            sr.flipX = true;
        }
    }

    void Attack()
    {
        attacking = true;
        Invoke("CheckHit", 0.3f);
    }

    void CheckHit()
    {
        if (IsPlayerSameLevel() && IsTargetInAttackRange())
        {
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
        if (playerSeenCounter <= 0)
        {
            return;
        }

        Move();
    }
}