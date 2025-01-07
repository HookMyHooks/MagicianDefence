using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class StoneBallSpell : Spell
    {
        private MonoBehaviour monoBehaviour; // Referință la MonoBehaviour pentru a folosi corutine (dacă e necesar)
        private Transform wandTip;           // Poziția de lansare
        private GameObject stoneBallPrefab;  // Prefab-ul pentru StoneBall
        private float stoneBallSpeed;        // Viteza de aruncare

        public StoneBallSpell(MonoBehaviour monoBehaviour, Transform wandTip, GameObject stoneBallPrefab, float stoneBallSpeed)
        {
            this.monoBehaviour = monoBehaviour;
            this.wandTip = wandTip;
            this.stoneBallPrefab = stoneBallPrefab;
            this.stoneBallSpeed = stoneBallSpeed;
            this.Type = SpellType.Earth;
            this.Name = "StoneBall";
            this.Cost = 50;
            this.Range = 15;
            this.Damage = 20;
            this.CoolDown = 3;
        }

        public override bool Cast(Transform magicianTransform)
        {
            // Verifică dacă vraja poate fi lansată
            if (CanCast())
            {
                base.Cast(magicianTransform); // Actualizează timpul ultimei lansări
                Debug.Log($"{Name} casted.");
                ShootStoneBall(magicianTransform);
                return true;
            }
            else
            {
                float timeLeft = (lastCastTime + CoolDown) - Time.time;
                Debug.Log($"{Name} is on cooldown. Try again in {timeLeft:F2} seconds.");
                return false;
            }
        }

        private void ShootStoneBall(Transform magicianTransform)
        {
            // Instanțiază mingea de piatră la poziția toiagului
            GameObject stoneBall = GameObject.Instantiate(stoneBallPrefab, wandTip.position, wandTip.rotation);

            // Adaugă Rigidbody dacă nu există deja
            Rigidbody rb = stoneBall.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = stoneBall.AddComponent<Rigidbody>();
            }

            // Setează proprietățile Rigidbody
            rb.useGravity = true; // Mingea de piatră cade după ce este aruncată
            rb.mass = 1f;         // Ajustează masa pentru a simula greutatea
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            // Calculează direcția bazată pe rotația magicianului
            Vector3 shootDirection = magicianTransform.forward;
            rb.AddForce(shootDirection * stoneBallSpeed, ForceMode.Impulse);

            // Aplică mișcare mingii în direcția privirii magicianului
            rb.linearVelocity = shootDirection * stoneBallSpeed;

            // Distruge mingea după 5 secunde pentru optimizare
            GameObject.Destroy(stoneBall, 5f);
        }
    }
}
