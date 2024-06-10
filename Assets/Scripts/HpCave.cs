using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class HpCave : MonoBehaviour
{
    static void main(string[] args)
    {

        HpClass a = HpClass.GetInstance();
        HpClass b = HpClass.GetInstance();
        HpClass c = HpClass.GetInstance();

    }

    public int health = 100;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "Health: " + health;

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
                    healthText.text = "Health: " + health; // Update the health text
                    Debug.Log("Hit light enemy, health reduced to: " + health);
                }


                // Destroy the gameobject that was hit by the raycast
                Destroy(hit.collider.gameObject);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
        }
    }


    internal class HpClass
    {
        private int Gamehealth = 100;
        private static HpClass _instance;
        private HpClass()
        {

        }
        public static HpClass GetInstance()
        {
            if (_instance == null)
            {
                _instance = new HpClass();

            }
            return _instance;
        }
        public void Hp(int Ghealth)
        {
            Gamehealth -= Ghealth;
        }
    }

    internal class ScoreM
    {

        private static ScoreM _instance;
        private int score = 0;
        private ScoreM() { }
        public static ScoreM GetInstance()
        {

            if (_instance == null)
            {
                _instance = new ScoreM();
            }
            return _instance;
        }
        public void AddScore(int someScore)
        {
            score += someScore;
        }
    }
}
    


