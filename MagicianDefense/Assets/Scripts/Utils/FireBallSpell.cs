using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class FireBallSpell : Spell
    {
        private MonoBehaviour monoBehaviour; // Reference to MonoBehaviour
        private Transform wandTip;
        private GameObject fireballPrefab;
        private float fireballSpeed;


        public FireBallSpell(MonoBehaviour monoBehaviour, Transform wandTip, GameObject fireballPrefab, float fireballSpeed) 
        {
            this.monoBehaviour = monoBehaviour;
            this.wandTip = wandTip;
            this.fireballPrefab = fireballPrefab;
            this.fireballSpeed = fireballSpeed;
            this.Type = SpellType.Fire;
            this.Name = "Fireball";
            this.Cost = 100;
            this.Range = 10;
            this.Damage = 10;
            this.CoolDown = 2;
        }
        public override bool Cast(Transform position)
        {
            // Check if the spell is ready to be cast
            if (CanCast())
            {
                base.Cast(position); // Updates the lastCastTime in the base class
                Debug.Log($"{Name} casted.");
                ShootFireball(); // Only call ShootFireball if the cooldown has expired
                return true;
            }
            else
            {
                float timeLeft = (lastCastTime + CoolDown) - Time.time;
                Debug.Log($"{Name} is on cooldown. Try again in {timeLeft:F2} seconds.");
                return false;
            }
        }

        private void ShootFireball()
        {
            // Instanțiază mingea de foc la poziția și rotația toiagului
            GameObject fireball = GameObject.Instantiate(fireballPrefab, wandTip.position, wandTip.rotation);

            // Adaugă mișcare folosind Rigidbody
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculează direcția bazată pe orientarea toiagului (wandTip.forward)
                Vector3 shootDirection = wandTip.transform.forward.normalized;

                // Aplică mișcare mingii în direcția privirii Wizard-ului
                rb.linearVelocity = shootDirection * fireballSpeed;

                // Aplică viteza mingii
                rb.linearVelocity = shootDirection * fireballSpeed;
            }

            // Distruge mingea după 5 secunde
            GameObject.Destroy(fireball, 5f);
        }
    }
}
