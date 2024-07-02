using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOneWayPoints : MonoBehaviour
{

    public List <GameObject> waypoints;
    public float speed = 2;
    private GameObject Waypoints;



    // Start is called before the first frame update
    void Start()
    {
        Waypoints = GameObject.Find("waypoint 0");
        
    }

    // Update is called once per frame
    void Update()
    {

        int index = 0;
        Vector3 NewPOs= Vector3 .MoveTowards(transform.position, waypoints[index].transform.position, speed* Time.deltaTime);
        transform.position = NewPOs;
        
    }
}
