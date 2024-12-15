using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Spell
    {
        public string Name { get; set; }

        public int Cost { get; set; }

        public float CoolDown { get; set; }

        public float Damage { get; set; }

        public float Range { get; set; }

        public SpellType Type { get; set; }

        private float lastCastTime = -Mathf.Infinity; // Tracks the last time the spell was cast

        public virtual void Cast(Transform caster)
        {
            if (CanCast())
            {
                lastCastTime = Time.time; // Update the last cast time
                Debug.Log($"{Name} casted by {caster.name}");
            }
            else
            {
                float timeLeft = (lastCastTime + CoolDown) - Time.time;
                Debug.Log($"{Name} is on cooldown. Try again in {timeLeft:F2} seconds.");
            }
        }

        bool CanCast()
        {
            // Check if enough time has passed since the last cast
            return Time.time >= lastCastTime + CoolDown;
        }

    }
}
