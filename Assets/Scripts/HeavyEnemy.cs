using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeavyEnemy : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        monsterHealth = 5;
        maxHealth = 5; 

    }
   

    [SerializeField] FloaghtingHealthBar healthBar;
    private void Awake()
    {
        monsterHealth = maxHealth;
        healthBar.UpdateHealthBar(monsterHealth, maxHealth);
        healthBar = GetComponentInChildren<FloaghtingHealthBar>();
    }

    public void TakeDamage(float damageAmount)
    {
        monsterHealth -= damageAmount;
        healthBar.UpdateHealthBar(monsterHealth, maxHealth);
        if (monsterHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
