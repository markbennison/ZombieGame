
using UnityEngine;

public class EnemyBehaviour : EnemyController
{

    protected override void BehaviourOptions()
    {
















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