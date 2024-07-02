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
            Debug.Log("did hit heavy monster");
        }

        if (collision.gameObject.TryGetComponent<LightEnemy>(out LightEnemy LightEnemyComponent))
        {
            LightEnemyComponent.TakeDamage(1);
            Debug.Log("did hit light monster");
        }

        Destroy(gameObject);
    }
    
}
