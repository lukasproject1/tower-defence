using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HpCave : MonoBehaviour
{
    public int health= 100;
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = (1 << 11) | (1 << 12) | (1 << 13);

        RaycastHit hit;

        float maxDistance = Mathf.Infinity; 

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, maxDistance,layerMask))
        {
            if (hit.collider != null)
            {
                // Destroy the gameobject that was hit by the raycast
                Destroy(hit.collider.gameObject);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
              
            }
        }
    }
}
