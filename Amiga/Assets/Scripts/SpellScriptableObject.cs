using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Spell System/Spell")]
public class Spell : ScriptableObject
{
    public string spellName;       // Name of the spell
    public int manaCost;           // Mana cost to cast the spell
    public int damage;             // Damage dealt by the spell
    public GameObject spellEffect; // Visual effect of the spell
}
