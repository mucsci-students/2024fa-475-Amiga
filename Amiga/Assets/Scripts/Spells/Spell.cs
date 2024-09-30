using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Holds a reference to a Spell. Allowing it to cast the spell defined in the spell class.
*/
public class SpellCaster : MonoBehaviour
{
    public Spell spell; //Spell ScriptableObject
    public ManaSystem manaSystem; //Manasystem
    void Update()
    {
        // Cast the spell when the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CastSpell();
        }
    }

    /*1. Logs the name of the spell being cast to the console
      2. Instantiates the visual effect of the spell at the caster's position and orientation
      3. Logs the damage dealt by the spellm
        Which can be extended to apply damage to enemies in the game
        
        
        */


    void CastSpell()
{
    // Check if we have enough mana to cast the spell
    if (manaSystem.CastSpell(spell.manaCost))
    {
        Debug.Log("Casting " + spell.spellName); // Corrected to Debug.Log

        // Instantiate the spell's effect at the caster's position
        if (spell.spellEffect != null)
        {
            Instantiate(spell.spellEffect, transform.position, transform.rotation);
        }

        // Deal damage (can be extended to deal damage to enemies)
        Debug.Log("Dealt " + spell.damage + " damage!"); // Corrected to Debug.Log
    }else{
        Debug.Log("Not enough mana to cast the spell!");
    }
}

}
