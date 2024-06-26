using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<HeavyEnemy>(out HeavyEnemy heavyEnemyComponent))
        {
            heavyEnemyComponent.TakeDamage(1);
            Debug.Log("did hit monster");
        }


        Destroy(gameObject);
    }
}
