using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public Transform target; // The target to which the instantiated object will transform
    public float moveSpeed = 1f; // Speed at which the instantiated object will move towards the target

    private List<GameObject> instantiatedObjects = new List<GameObject>();

    void Start()
    {
        if (prefabToInstantiate == null)
        {
            Debug.LogError("Prefab to Instantiate is not assigned.");
        }

        if (target == null)
        {
            Debug.LogError("Target is not assigned.");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (prefabToInstantiate != null)
            {
                GameObject obj = Instantiate(prefabToInstantiate);
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                instantiatedObjects.Add(obj);
                Debug.Log("Instantiated object at position: " + obj.transform.position);
            }
            else
            {
                Debug.LogError("Prefab to Instantiate is not assigned.");
            }
        }

        MoveObjectsToTarget();
    }

    void MoveObjectsToTarget()
    {
        foreach (var obj in instantiatedObjects)
        {
            if (obj != null && target != null)
            {
                Vector3 oldPosition = obj.transform.position;
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, target.position, moveSpeed * Time.deltaTime);
                obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, target.rotation, moveSpeed * 100 * Time.deltaTime);
                Debug.Log("Moved object from " + oldPosition + " to " + obj.transform.position);
            }
            else if (target == null)
            {
                Debug.LogError("Target is not assigned.");
            }
        }
    }
}