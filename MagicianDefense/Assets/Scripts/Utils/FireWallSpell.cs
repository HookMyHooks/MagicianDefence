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

        public override bool Cast(Transform magicianTransform)
        {
            if (CanCast())
            {
                Transform targetMinion = FindClosestMinion(magicianTransform);
                if (targetMinion == null)
                {
                    Debug.Log("No target minion found for FireWall.");
                    return false;
                }

                base.Cast(magicianTransform);
                Debug.Log($"{Name} casted.");
                SpawnFirewall(magicianTransform, targetMinion);
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
        private void SpawnFirewall(Transform magicianTransform, Transform targetMinion)
        {
            if (targetMinion == null)
            {
                Debug.LogWarning("No target minion found. FireWall not spawned.");
                return;
            }

            // Calculate position in front of the minion
            Vector3 directionToWizard = (magicianTransform.position - targetMinion.position).normalized;
            Vector3 spawnPosition = targetMinion.position + directionToWizard  * 10f; // 5 units in front of the minion

            // Adjust height
            spawnPosition.y = targetMinion.position.y + 1f;

            // Instantiate the firewall
            GameObject fireWall = GameObject.Instantiate(fireWallPrefab, spawnPosition, Quaternion.identity);

            // Align rotation
            fireWall.transform.rotation = Quaternion.LookRotation(-directionToWizard);

            // Destroy firewall after 5 seconds
            GameObject.Destroy(fireWall, 10f);
        }

    }
}
