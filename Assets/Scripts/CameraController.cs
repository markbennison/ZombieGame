using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    Vector2 targetPosition;

    public Transform lowerBounds;
    public Transform upperBounds;

    void Update()
    {
        targetPosition.x = Mathf.Clamp(target.position.x, lowerBounds.position.x, upperBounds.position.x);
        targetPosition.y = Mathf.Clamp(target.position.y, lowerBounds.position.y, upperBounds.position.y);

        transform.position = new Vector3(targetPosition.x, targetPosition.y, -10f);
    }
}
