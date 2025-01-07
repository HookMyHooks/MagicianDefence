using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using Unity.VisualScripting;

public class HUDManager : MonoBehaviour
{
    public Text[] spellTexts; // Text fields for the HUD
    public SpellCoolDownManager spellCoolDownManager; // Reference to the manager

    void Update()
    {
        var spellManager = GetComponent<SpellManager>();

        var availableSpells = spellManager.GetSpells();

        float timeSinceCast = 0;
        float remainingCooldown = 0;

        for (int i = 0; i < spellTexts.Length; i++)
        {
            if (i < availableSpells.Count)
            {
                Spell spell = availableSpells[i];
                timeSinceCast = Time.time - spell.GetLastTimeCalled();
                remainingCooldown = Mathf.Clamp(spell.CoolDown - timeSinceCast, 0, spell.CoolDown);

                if (spell.RemainingCooldown > 0)
                {
                    spellTexts[i].text = $"{spell.Name}: {Mathf.Ceil(remainingCooldown)}s";
                }
                else
                {
                    spellTexts[i].text = $"{spell.Name}: Ready!";
                }
            }
            else
            {
                spellTexts[i].text = "Test";
            }
        }
    }

}
