using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using Unity.VisualScripting;
using Wizard = Assets.Scripts.Utils.Wizard;

public class HUDManager : MonoBehaviour
{
    public Text[] spellTexts; // Text fields for the HUD
    public SpellCoolDownManager spellCoolDownManager; // Reference to the manager
    public Slider healthSlider; // Reference to the health slider
    public Slider manaSlider;   // Reference to the mana slider

    void Start()
    {
    }

    void Update()
    {
        var spellManager = FindFirstObjectByType<SpellManager>();
        var wizard = FindFirstObjectByType<Wizard>();

        var availableSpells = spellManager.GetSpells();
        Debug.Log(availableSpells.Count);

        float timeSinceCast = 0;
        float remainingCooldown = 0;

        for (int i = 0; i < spellTexts.Length; i++)
        {
            if (i < availableSpells.Count)
            {
                Spell spell = availableSpells[i];
                Debug.Log(spell.Name);
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
                spellTexts[i].text = "";
            }
        }

        if (wizard != null)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, (float)wizard.health / 100f, Time.deltaTime * 10f);
            manaSlider.value = Mathf.Lerp(manaSlider.value, (float)wizard.currentMana / 500f, Time.deltaTime * 10f);
        }
    }

}
