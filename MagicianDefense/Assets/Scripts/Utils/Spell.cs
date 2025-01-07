using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Spell
    {
        public string Name { get; set; }

        public int Cost { get; set; }

        public float CoolDown { get; set; }

        public float RemainingCooldown
        {
            get
            {
                float timeSinceCast = Time.time - lastCastTime;
                return Mathf.Clamp(CoolDown - timeSinceCast, 0, CoolDown);
            }
        }


        public float Damage { get; set; }

        public float Range { get; set; }

        public SpellType Type { get; set; }

        protected float lastCastTime = -Mathf.Infinity; // Tracks the last time the spell was cast
        private float remainingCooldown; // The actual remaining cooldown value


        public virtual bool Cast(Transform position)
        {
            lastCastTime = Time.time; // Update the last cast time
            remainingCooldown = CoolDown;
            Debug.Log($"{Name} casted");
            return true;
        }

        protected bool CanCast()
        {
            // Check if enough time has passed since the last cast
            return Time.time >= lastCastTime + CoolDown;
        }

        public void SetCooldown(float cooldown)
        {
            remainingCooldown = cooldown;
            lastCastTime = Time.time; // Update last cast time when cooldown is set
        }

        public float GetLastTimeCalled()
        {
            return lastCastTime;
        }
    }
}
