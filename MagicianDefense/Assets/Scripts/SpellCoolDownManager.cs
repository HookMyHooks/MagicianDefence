using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Utils;

public class SpellCoolDownManager : MonoBehaviour
{
    [SerializeField] private List<Spell> availableSpells; // List of spells
    public SpellManager spellManager;

    void Start()
    {
        /*availableSpells = new List<Spell>();

        // Ensure to get the available spells from SpellManager
        if (spellManager == null)
        {
            spellManager = new SpellManager();
        }

        availableSpells = spellManager.GetSpells();*/
    }

    void Update()
    {
        // Update spell cooldowns internally, but not the UI
       /* for (int i = 0; i < availableSpells.Count; i++)
        {
            Spell spell = availableSpells[i];

            if (spell.RemainingCooldown > 0)
            {
                spell.SetCooldown(spell.RemainingCooldown - Time.deltaTime);
            }
        }*/
    }


    public List<Spell> GetAvailableSpells()
    {
        return availableSpells ?? new List<Spell>();
    }

}
