using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class FireBallSpell : Spell
    {
        private Transform wandTip;
        private GameObject fireballPrefab;
        private float fireballSpeed;

        public FireBallSpell(MonoBehaviour monoBehaviour, Transform wandTip, GameObject fireballPrefab, float fireballSpeed)
        {
            this.wandTip = wandTip;
            this.fireballPrefab = fireballPrefab;
            this.fireballSpeed = fireballSpeed;
            this.Type = SpellType.Fire;
            this.Name = "Fireball";
            this.Cost = 100;
            this.Damage = 20;
            this.CoolDown = 2;
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
                ShootFireball(magicianTransform, targetMinion);
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
            float maxAngle = 45f; // Define the FOV angle (e.g., 45 degrees in front of the wizard)

            foreach (var minion in minions)
            {
                Vector3 directionToMinion = (minion.transform.position - magicianTransform.position).normalized;

                // Check if the minion is within range
                float distance = Vector3.Distance(magicianTransform.position, minion.transform.position);
                if (distance < minDistance)
                {
                    // Check if the minion is within the FOV
                    float angle = Vector3.Angle(magicianTransform.forward, directionToMinion);
                    if (angle <= maxAngle) // Minion is within the FOV
                    {
                        closest = minion.transform;
                        minDistance = distance;
                    }
                }
            }
            return closest;
        }
        private void ShootFireball(Transform magicianTransform, Transform targetMinion)
        {
            GameObject fireball = GameObject.Instantiate(fireballPrefab, wandTip.position, wandTip.rotation);

            // Add Rigidbody and configure it
            Rigidbody rb = fireball.GetComponent<Rigidbody>() ?? fireball.AddComponent<Rigidbody>();
            rb.useGravity = false; // Prevent the "vaulting" behavior
            rb.mass = 1f;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            // Calculate the direction to the target
            Vector3 direction = (targetMinion.position - wandTip.position).normalized;

            // Apply force toward the target
            rb.AddForce(direction * fireballSpeed * 5f, ForceMode.Impulse);

            // Add collision handling
            FireballCollision collisionHandler = fireball.AddComponent<FireballCollision>();
            collisionHandler.damage = (int)this.Damage; // Pass the damage value from the spell

            // Destroy the stone ball after a certain time to clean up
            GameObject.Destroy(fireball, 5f);
        }
    }
}
