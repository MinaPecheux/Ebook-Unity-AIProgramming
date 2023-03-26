using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Spell")]
public class Spell : ScriptableObject
{
    public static Dictionary<string, Spell> LIB;

    public int amount;
    public int manaCost;
    public string animation;
    public bool onSelf;
    public bool freezes;
    public string callbackName;
    public GameObject castVfxPrefab;
    public GameObject hitVfxPrefab;

    public static void LoadAll()
    {
        Spell[] spells = Resources.LoadAll<Spell>("Spells");
        LIB = new Dictionary<string, Spell>();
        foreach (Spell spell in spells)
            LIB[spell.name] = spell;
    }
}
