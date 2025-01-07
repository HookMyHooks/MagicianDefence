using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class FireWallSpell : Spell
    {
        private MonoBehaviour monoBehaviour; // Reference to MonoBehaviour
        private GameObject fireWallPrefab;


        public FireWallSpell(GameObject fireWallPrefab) 
        {
            this.fireWallPrefab = fireWallPrefab;
            this.Name = "FireWall";
            this.Type = SpellType.Fire;
            this.Cost = 200;
            this.Range = 1;
            this.Damage = 0;
            this.CoolDown = 20;
        }

        public override bool Cast(Transform position)
        {
            // Check if the spell is ready to be cast
            if (CanCast())
            {
                base.Cast(position); // Updates the lastCastTime in the base class
                Debug.Log($"{Name} casted.");
                SpawnFirewall(position); // Only call ShootFireball if the cooldown has expired
                return true;
            }
            else
            {
                float timeLeft = (lastCastTime + CoolDown) - Time.time;
                Debug.Log($"{Name} is on cooldown. Try again in {timeLeft:F2} seconds.");
                return false;
            }
        }

        private void SpawnFirewall(Transform transform)
        {
            // Calculează poziția FireWall-ului
            Vector3 spawnPosition = transform.position + transform.forward * 100f; // Poziționează FireWall-ul în fața personajului

            spawnPosition.y = transform.position.y+20f; // Adaugă 2 unități pe axa Y (ajustează valoarea după nevoie)

            Quaternion fireWallRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            // Instanțiază FireWall-ul
         //   GameObject fireWall = GameObject.Instantiate(fireWallPrefab, spawnPosition, Quaternion.identity);


            GameObject fireWall = GameObject.Instantiate(fireWallPrefab, spawnPosition, fireWallRotation);
            // Aliniază rotația
            fireWall.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            // Distruge FireWall-ul după 5 secunde
          //  GameObject.Destroy(fireWall, 5f);
        }
    }
}
