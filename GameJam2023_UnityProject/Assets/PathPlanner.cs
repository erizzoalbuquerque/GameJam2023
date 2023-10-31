using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlanner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

    }

    public Vector2 GetDirectionToGoal(Vector2 targetPosition, Vector2 currentPosition)
    {
        Vector2 delta = targetPosition - currentPosition;

        return delta.normalized;
    }
}
