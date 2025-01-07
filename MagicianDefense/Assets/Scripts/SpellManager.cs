using Assets.Scripts.Utils;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public void Initialize(SpellType initialType, Transform wandTip, GameObject fireballPrefab, float fireballSpeed, GameObject fireRingPrefab, float fireRingDistance, GameObject fireWallPrefab, GameObject stoneBallPrefab, float stoneBallSpeed, GameObject stoneWallPrefab)
    {
        fireSpells = new List<Spell>
        {
            new FireBallSpell(this, wandTip, fireballPrefab, fireballSpeed),
            new FireRingSpell(fireRingPrefab, fireRingDistance),
            new FireWallSpell(fireWallPrefab),
            new StoneBallSpell(this, wandTip, stoneBallPrefab, stoneBallSpeed),
            new StoneWallSpell(this, stoneWallPrefab)
        };

        earthSpells = new List<Spell>();

        SelectedCategory = initialType;

        isInitialized = true;

        Debug.Log("SpellManager initialized");
    }

    public SpellType SelectedCategory
    {
        get { return SelectedCategory; }
        set
        {
            switch (value)
            {
                case SpellType.None:
                    availableSpells.Clear();
                    break;
                case SpellType.Fire:
                    availableSpells = fireSpells;
                    break;
                case SpellType.Earth:
                    availableSpells = earthSpells;
                    break;
            }
        }
    }

    private List<Spell> availableSpells;

    private List<Spell> fireSpells;

    private List<Spell> earthSpells;

    public bool isInitialized = false;

    public Spell GetSpell(int key)
    {
        if ( (int)KeyCode.Q == key ) 
            return availableSpells[0];

        if ((int)KeyCode.W == key)
            return availableSpells[1];

        if ((int)KeyCode.E == key)
            return availableSpells[2];

        if ((int)KeyCode.R == key) 
            return availableSpells[3];

        if ((int)KeyCode.T == key)
            return availableSpells[4];
        return null;
    }

    public List<Spell> GetSpells() 
    { 
        return availableSpells; 
    }
}
