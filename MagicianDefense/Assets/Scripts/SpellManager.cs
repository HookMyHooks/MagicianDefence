using Assets.Scripts.Utils;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager
{
    public SpellManager(SpellType initialType)
    {
        fireSpells = new List<Spell>
        {
            new FireBallSpell(),
            new FireWallSpell(),
            new FireZoneSpell()
        };

        earthSpells = new List<Spell>();

        SelectedCategory = initialType;
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

    public Spell GetSpell(int key)
    {
        if ( (int)KeyCode.Q == key ) 
            return availableSpells[0];

        if ((int)KeyCode.W == key)
            return availableSpells[1];

        if ((int)KeyCode.E == key)
            return availableSpells[2];

        return null;
    }
}
