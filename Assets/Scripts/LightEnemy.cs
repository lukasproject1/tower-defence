using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy :Monster
{
    public int moneyValue = 20;
    // Start is called before the first frame update
    void Start()
    {
        speed = 6;
        monsterHealth = 2;
        maxHealth = 2;
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
            MoneySystem.instance.AddMoney(moneyValue);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
