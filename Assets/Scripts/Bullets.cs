using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullets : MonoBehaviour
{

    private Transform target;
    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    public void Seek(Transform _target)
    {
        target = _target;
    }
    
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
    public void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }
    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

      

        Destroy(gameObject);
    }


}
