using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public Spell spell; // Reference to a Spell ScriptableObject

    void Update()
    {
        // Cast the spell when the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CastSpell();
        }
    }

    void CastSpell()
    {
        // Check if we have enough mana (if a mana system is implemented)
        Debug.Log("Casting " + spell.spellName);

        // Instantiate the spell's effect at the caster's position
        if (spell.spellEffect != null)
        {
            Instantiate(spell.spellEffect, transform.position, transform.rotation);
        }

        // Deal damage (can be extended to deal damage to enemies)
        Debug.Log("Dealt " + spell.damage + " damage!");
    }
}
