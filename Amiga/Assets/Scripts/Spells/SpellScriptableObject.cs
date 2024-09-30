using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
[CreateAssetMenu]: Allows you to create instances of the spell class as
Scriptable objects from the unity editor.
Right-click in the Project Window and navigate to "Spell System" to create a new spell.



*/
[CreateAssetMenu(fileName = "NewSpell", menuName = "Spell System/Spell")]
public class Spell : ScriptableObject
{
    public string spellName;       // Name of the spell
    public int manaCost;           // Mana cost to cast the spell
    public int damage;             // Damage dealt by the spell
    public GameObject spellEffect; // Visual effect of the spell
}
