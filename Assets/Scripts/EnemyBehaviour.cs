using System.Collections;
using System.Collections.Generic;
using Unity.IntegerTime;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    public float sightRange = 8f;
    public float moveSpeed = 4f;

    GameObject player;
    float playerSeenDirection = 0f;
    float playerSeenCounter = 0f;
    float playerSeenCooldownTotal = 5f;
    Vector3 translateValue;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        translateValue = new Vector3(moveSpeed, 0, 0);
    }

    void Update()
    {
        Debug.Log(CheckPlayerSameLevel());
        BehaviourOptions();
    }

    void BehaviourOptions()
    {
        SeenCountdown();

        if (CheckPlayerSameLevel() && TargetInChaseRange() && !TargetInAttackRange())
        {
            Debug.Log("CHASE");
            Chase();
        }
        else if (CheckPlayerSameLevel() && TargetInAttackRange())
        {
            Debug.Log("ATTACK");
        }
        else if (playerSeenCounter > 0 && playerSeenDirection != 0)
        {
            Debug.Log("SEARCH");
            Search();
        }
        else
        {
            Debug.Log("IDLE");
            
        }



    }

    void Move()
    {
        //Vector3 movementVector = translateValue * TargetDirectionNormalised();
        //transform.position = Vector3.Lerp(transform.position, movementVector, Time.deltaTime);


        transform.Translate(translateValue * Mathf.Lerp(0f, TargetDirectionNormalised(), Time.deltaTime));
    }

    void Attack()
    {
        
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
        float verticalDifference = transform.position.y - player.transform.position.y;
        //Debug.Log("vd: " + verticalDifference);

        if (verticalDifference > 2)
        {
            return false;
        }

        if (verticalDifference < -5)
        {
            return false;
        }

        return true;
    }

    bool TargetInAttackRange()
    {
        float temporaryRange = TargetDirectionAndDistance();
        float attackRange = 2f;
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
        float horizontalDistance = player.transform.position.x - transform.position.x;

        if (horizontalDistance < 0)
        {
            return -1f;
        }
        else if (horizontalDistance > 0)
        {
            return 1f;
        }

        return 0f;
    }

}
