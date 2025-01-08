using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Wizard : MonoBehaviour
    {
        [Header("References")]
        public Transform wandTip;           // Child la toiag
        public GameObject fireballPrefab;   // Prefab pentru mingea de foc
        public GameObject fireRingPrefab;

        //added fireWall prefab here
        public GameObject fireWallPrefab;
        public int health;

        [Header("Mana Regeneration")]
        public int currentMana = 500;          // Current mana capacity
        public int manaRegenRate = 15;     // Mana regenerated per second

        [Header("Fireball Settings")]
        public float fireballSpeed = 1f;

        [Header("Fire Ring Settings")]
        public float fireRingDistance = 5f; // Distanța medie față de personaj


        [Header("Stone Ball Settings")]
        public GameObject stoneBallPrefab;  // Prefab pentru mingea de piatră
        public float stoneBallSpeed = 100f;

        [Header("Stone Wall Settings")]
        public GameObject stoneWallPrefab;

        void Start()
        {
            // Asigură-te că layer-urile nu ignoră coliziunea
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Wizard"), LayerMask.NameToLayer("Obstacle"), false);

            // Alte inițializări existente
            Debug.Log("Coliziunea între Wizard și Obstacle este acum activată.");

            var spellManagerObject = new GameObject("SpellManager");
            spellManager = spellManagerObject.AddComponent<SpellManager>();

            if (!spellManager.isInitialized)
            {
                spellManager.Initialize(SpellType.Fire, wandTip, fireballPrefab, fireballSpeed, fireRingPrefab, fireRingDistance, fireWallPrefab, stoneBallPrefab, stoneBallSpeed, stoneWallPrefab);
            }

            InvokeRepeating(nameof(RegenerateMana), 1f, 1f);
        }

        private SpellManager spellManager;

        private void Update()
        {
            Transform targetMinion = GetClosestMinion();
            int key = GetCurrentButton();

            if (key != 0 && targetMinion != null)
            {
                var spell = spellManager.GetSpell(key);
                if (currentMana < spell.Cost) return;

                bool hasCasted = spell.Cast(targetMinion);
                if (hasCasted) currentMana -= spell.Cost;
            }
        }

        private Transform GetClosestMinion()
        {
            GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
            Transform closest = null;
            float minDistance = Mathf.Infinity;
            float maxViewAngle = 45f; // Field of view angle (e.g., 45 degrees on either side of forward)

            foreach (var minion in minions)
            {
                Vector3 directionToMinion = (minion.transform.position - transform.position).normalized;

                // Check if the minion is within the wizard's field of view
                float angle = Vector3.Angle(transform.forward, directionToMinion);
                if (angle <= maxViewAngle)
                {
                    // Check if the minion is closer than the currently selected one
                    float distance = Vector3.Distance(transform.position, minion.transform.position);
                    if (distance < minDistance)
                    {
                        closest = minion.transform;
                        minDistance = distance;
                    }
                }
            }

            return closest;
        }




        int GetCurrentButton()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                return (int)KeyCode.Q;

            if (Input.GetKeyDown(KeyCode.W))
                return (int)KeyCode.W;

            if (Input.GetKeyDown(KeyCode.E))
                return (int)KeyCode.E;

            if (Input.GetKeyDown(KeyCode.R))
                return (int)KeyCode.R;

            if (Input.GetKeyDown(KeyCode.T))
                return (int)KeyCode.T;
            return 0;
        }

        public void TakeDamage(int value)
        {
            health -= value;

            if (health == 0)
            {
                Destroy(gameObject);
            }
        }

        private void RegenerateMana()
        {
            currentMana = Mathf.Min(currentMana + manaRegenRate, 500); // Ensure mana doesn't exceed maxMana
            //Debug.Log($"Updated mana {currentMana}\n");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Wizzard collision triggered with: {other.gameObject.ToString()}");
        }
    }
}
