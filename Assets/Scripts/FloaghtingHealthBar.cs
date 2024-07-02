using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloaghtingHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    private GameObject player;

    private void Start()
    {
        camera = Camera.main;
        player = GameObject.Find("Player");
    }
    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue/maxValue;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!camera)
        {
            camera = Camera.main;
        }
        else
        {
            transform.rotation = camera.transform.rotation;

        }
    }
}
