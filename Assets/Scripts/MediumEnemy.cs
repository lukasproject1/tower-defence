using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemy : Monster
{
    public int moneyValue = 40;
    // Start is called before the first frame update
    void Start()
    {
        speed= 5;
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

            MoneySystem.instance.AddMoney(moneyValue);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
