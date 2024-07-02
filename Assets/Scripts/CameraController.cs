using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not assigned!");
        }
    }

    void Update()
    {
        // Voeg hier je camerafuncties toe
    }
}
