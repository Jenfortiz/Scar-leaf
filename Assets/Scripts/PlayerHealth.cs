using UnityEngine;
using System.Collections;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
  
    [Header("Health Settings")]
    public int maxHealth = 100;
    public float decayRate = 2f;
    public int decayAmount = 5; 
    public int healAmount = 10;  
    [Header("UI Reference")]
    public TextMeshProUGUI healthText;
    [Header("Current Status")]
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthDisplay();
        StartCoroutine(HealthDecayLoop());
    }
    void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }

    IEnumerator HealthDecayLoop()
    {
        while (currentHealth > 0)
        {
            yield return new WaitForSeconds(decayRate);
            
            DecreaseHealth(decayAmount);
        }
    }


    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthDisplay();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthDisplay();
        
    }

    void Die()
    {
        StopCoroutine(HealthDecayLoop());
        
        Debug.Log("Player has died!");
        
    }
}