using System.Collections;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    public int maxMana = 100;     // Maximum amount of mana
    public int currentMana;        // Current amount of mana
    public float manaRegenRate = 5f; // Rate of mana regeneration per second

    void Start()
    {
        currentMana = maxMana; // Initialize current mana to max at the start
        StartCoroutine(RegenerateMana());
    }

    void Update()
    {
        // You can add any UI updates here to show current mana
    }

    // Coroutine to regenerate mana over time
    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            if (currentMana < maxMana)
            {
                currentMana += Mathf.FloorToInt(manaRegenRate * Time.deltaTime);
                currentMana = Mathf.Min(currentMana, maxMana); // Cap at maxMana
            }
            yield return null; // Wait until the next frame
        }
    }

    // Method to cast a spell
    public bool CastSpell(int manaCost)
    {
        if (currentMana >= manaCost)
        {
            currentMana -= manaCost; // Deduct mana cost
            return true; // Spell cast successfully
        }
        Debug.Log("Not enough mana to cast the spell!");
        return false; // Not enough mana
    }
}
