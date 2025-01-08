using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class StoneWallSpell : Spell
    {
        private MonoBehaviour monoBehaviour;
        private GameObject stoneWallPrefab;
        private float stoneWallDistance = 10.0f; // Distanța față de magician

        public StoneWallSpell(MonoBehaviour monoBehaviour, GameObject stoneWallPrefab)
        {
            this.monoBehaviour = monoBehaviour;
            this.stoneWallPrefab = stoneWallPrefab;
            this.Name = "StoneWall";
            this.Type = SpellType.Earth;
            this.Cost = 100;
            this.CoolDown = 10;
        }

        public override bool Cast(Transform magicianTransform)
        {
            if (CanCast())
            {
                Transform targetMinion = FindClosestMinion(magicianTransform);
                if (targetMinion == null)
                {
                    Debug.Log("No target minion found for StoneWall.");
                    return false;
                }

                base.Cast(magicianTransform);
                Debug.Log($"{Name} casted.");
                SpawnStoneWall(magicianTransform, targetMinion);
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

        private void SpawnStoneWall(Transform magicianTransform, Transform targetMinion)
        {
            if (targetMinion == null)
            {
                Debug.LogWarning("No target minion found. StoneWall not spawned.");
                return;
            }

            // Calculate position in front of the minion
            Vector3 directionToWizard = (magicianTransform.position - targetMinion.position).normalized;
            Vector3 spawnPosition = targetMinion.position - directionToWizard * 50f; // 10 units in front of the minion

            // Adjust height
            spawnPosition.y = targetMinion.position.y + 1f;
            spawnPosition.z -= 70f;

            // Instantiate the stone wall
            GameObject stoneWall = GameObject.Instantiate(stoneWallPrefab, spawnPosition, Quaternion.identity);

            // Align rotation
            stoneWall.transform.rotation = Quaternion.LookRotation(-directionToWizard);


            // Destroy after 10 seconds
            GameObject.Destroy(stoneWall, 10f);
        }
    }
}
