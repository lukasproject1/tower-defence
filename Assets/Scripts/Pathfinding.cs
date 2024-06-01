using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Pathfinding : MonoBehaviour
{
    public Vector3[] targetPO;
    private int currentPoint = 0; // Index of the current waypoint
    public float speed = 5f; // Speed of movement
    // Start is called before the first frame update
    void Start()
    {
        if (targetPO.Length>0)
        {
            transform.position = targetPO[0];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPO.Length > 1 && currentPoint < targetPO.Length)
        {
            MoveTowardsNextPoint();
        }

    }
    void MoveTowardsNextPoint()
    {
        if (currentPoint >= targetPO.Length)
            return;

        Vector3 targetPosition = targetPO[currentPoint];
        float step = speed * Time.deltaTime; // Calculate step size based on speed and frame time

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step); // Move towards the next waypoint

        // Check if the object has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPoint++; // Move to the next waypoint
        }
    }
}

