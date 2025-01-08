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
            if (CanCast())
            {
                // Find the closest minion as the target
                Transform targetMinion = FindClosestMinion(magicianTransform);
                if (targetMinion == null)
                {
                    Debug.Log("No target minion found.");
                    return false;
                }

                base.Cast(magicianTransform);
                Debug.Log($"{Name} casted.");
                ShootStoneBall(magicianTransform, targetMinion);
                return true;
            }
            else
            {
                float timeLeft = (lastCastTime + CoolDown) - Time.time;
                Debug.Log($"{Name} is on cooldown. Try again in {timeLeft:F2} seconds.");
                return false;
            }
        }

        private Transform FindClosestMinion(Transform magicianTransform)
        {
            GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
            Transform closest = null;
            float minDistance = Mathf.Infinity;

            foreach (var minion in minions)
            {
                float distance = Vector3.Distance(magicianTransform.position, minion.transform.position);
                if (distance < minDistance)
                {
                    closest = minion.transform;
                    minDistance = distance;
                }
            }
            return closest;
        }


        private void ShootStoneBall(Transform magicianTransform, Transform targetMinion)
        {
            GameObject stoneBall = GameObject.Instantiate(stoneBallPrefab, wandTip.position, wandTip.rotation);

            // Add Rigidbody and configure it
            Rigidbody rb = stoneBall.GetComponent<Rigidbody>() ?? stoneBall.AddComponent<Rigidbody>();
            rb.useGravity = false; // Prevent the "vaulting" behavior
            rb.mass = 1f;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            // Calculate the direction to the target
            Vector3 direction = (targetMinion.position - wandTip.position).normalized;

            // Apply force toward the target
            rb.AddForce(direction * stoneBallSpeed * 5f, ForceMode.Impulse);

            // Add collision handling
            StoneBallCollision collisionHandler = stoneBall.AddComponent<StoneBallCollision>();
            collisionHandler.damage = (int)this.Damage; // Pass the damage value from the spell

            // Destroy the stone ball after a certain time to clean up
            GameObject.Destroy(stoneBall, 5f);
        }


    }
}
