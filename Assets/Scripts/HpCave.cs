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
        int layerLightEnemy = LayerMask.NameToLayer("light enemy");
        int layerMediumEnemy = LayerMask.NameToLayer("medium enemy");
        int layerHeavyEnemy = LayerMask.NameToLayer("heavy enemy");
        int layerMask = (1 << layerLightEnemy) | (1 << layerMediumEnemy) | (1 << layerHeavyEnemy);

        RaycastHit hit;

        float maxDistance = Mathf.Infinity;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, maxDistance, layerMask))
        {
            if (hit.collider != null)
            {
                // Check if the hit object is on the "light enemy" layer
                if (hit.collider.gameObject.layer == layerLightEnemy)
                {
                    // Reduce health by 1
                    health -= 1;
                    Debug.Log("Hit light enemy, health reduced to: " + health);
                }

                // Destroy the gameobject that was hit by the raycast
                Destroy(hit.collider.gameObject);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
        }
    }
}
