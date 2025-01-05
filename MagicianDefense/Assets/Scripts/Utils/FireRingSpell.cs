using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class FireRingSpell : Spell
    {
        //private MonoBehaviour monoBehaviour; // Reference to MonoBehaviour
        private Transform wandTip;
        private GameObject fireRingPrefab;
        private float fireRingDistance;
        public float Radius { get; set; }

        public FireRingSpell(GameObject fireRingPrefab, float fireRingDistance) 
        {
            this.fireRingPrefab = fireRingPrefab;
            this.fireRingDistance = fireRingDistance;
            this.Name = "FireRing";
            this.Type = SpellType.Fire;
            this.Cost = 200;
            this.CoolDown = 15;
            this.Damage = 20; 
            this.Radius = 1f;
        }

        public override bool Cast(Transform position)
        {
            // Check if the spell is ready to be cast
            if (CanCast())
            {
                base.Cast(position); // Updates the lastCastTime in the base class
                Debug.Log($"{Name} casted.");
                SpawnFireRing(position); // Only call ShootFireball if the cooldown has expired
                return true;
            }
            else
            {
                float timeLeft = (lastCastTime + CoolDown) - Time.time;
                Debug.Log($"{Name} is on cooldown. Try again in {timeLeft:F2} seconds.");
                return false;
            }
        }

        private void SpawnFireRing(Transform transform)
        {
            Vector3 spawnPosition = transform.position + transform.forward * fireRingDistance;
            spawnPosition.y = transform.position.y; // Menține aceeași înălțime

            Quaternion fireRingRotation = Quaternion.Euler(-90, transform.eulerAngles.y, 0);

            GameObject fireRing = GameObject.Instantiate(fireRingPrefab, spawnPosition, fireRingRotation);

            GameObject.Destroy(fireRing, 5f);
        }
    }
}
