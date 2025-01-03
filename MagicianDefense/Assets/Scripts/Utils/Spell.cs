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

        protected float lastCastTime = -Mathf.Infinity; // Tracks the last time the spell was cast

        public virtual bool Cast(Transform position)
        {
            lastCastTime = Time.time; // Update the last cast time
            Debug.Log($"{Name} casted");
            return true;
        }

        protected bool CanCast()
        {
            // Check if enough time has passed since the last cast
            return Time.time >= lastCastTime + CoolDown;
        }

    }
}
