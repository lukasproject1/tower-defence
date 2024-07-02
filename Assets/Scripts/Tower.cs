using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet
    public float range = 10f; // Range within which the tower can detect monsters
    public float fireRate = 1f; // Rate of fire (bullets per second)
    private float fireCountdown = 0f; // Countdown timer for firing
    private Transform target; // Current target monster

    // Reference to the part of the tower that rotates to aim at the target
    //public Transform partToRotate;
    public Transform baseToRotate;
    public Transform turretToRotate;
    public float turnSpeed = 10f;

    private GameObject towerObject;
    private void Start()
    {

        baseToRotate = transform.Find("BaseRotationPart");
        turretToRotate = transform.Find("TurretRotationPart");

        if (baseToRotate == null || turretToRotate == null)
        {
            Debug.LogError("Rotation parts not found. Please ensure the rotating parts are correctly named.");
        }
    }
    void Update()
    {
        UpdateTarget();

        if (target == null)
            return;

        AimAtTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestMonster = null;

        foreach (GameObject monster in monsters)
        {
            float distanceToMonster = Vector3.Distance(transform.position, monster.transform.position);
            if (distanceToMonster < shortestDistance)
            {
                shortestDistance = distanceToMonster;
                nearestMonster = monster;
            }
        }

        if (nearestMonster != null && shortestDistance <= range)
        {
            target = nearestMonster.transform;
        }
        else
        {
            target = null;
        }
    }

    void AimAtTarget()
    {
        Vector3 dir = target.position - transform.position;

        // Rotate the base horizontally
        Vector3 baseDir = new Vector3(dir.x, 0, dir.z);
        Quaternion baseLookRotation = Quaternion.LookRotation(baseDir);
        Vector3 baseRotation = Quaternion.Lerp(baseToRotate.rotation, baseLookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        baseToRotate.rotation = Quaternion.Euler(0f, baseRotation.y, 0f);

        // Rotate the turret vertically
        Quaternion turretLookRotation = Quaternion.LookRotation(dir);
        Vector3 turretRotation = Quaternion.Lerp(turretToRotate.rotation, turretLookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        turretToRotate.rotation = Quaternion.Euler(turretRotation.x, baseRotation.y, turretRotation.z);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullets bullet = bulletGO.GetComponent<Bullets>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

