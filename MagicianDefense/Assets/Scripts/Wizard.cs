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
        void Start()
        {
            var spellManagerObject = new GameObject("SpellManager");
            spellManager = spellManagerObject.AddComponent<SpellManager>();

            spellManager.Initialize(SpellType.Fire, wandTip, fireballPrefab, fireballSpeed, fireRingPrefab, fireRingDistance, fireWallPrefab);

            InvokeRepeating(nameof(RegenerateMana), 1f, 1f);
        }

        private SpellManager spellManager;

        private void Update()
        {
            int key = GetCurrentButton();

            if(key != 0)
            {
                var spell = spellManager.GetSpell(key);
                if (currentMana < spell.Cost) return;
                bool hasCasted = spell.Cast(transform);
                if(hasCasted) currentMana -= spell.Cost;
            }
        }

        int GetCurrentButton()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                return (int)KeyCode.Q;

            if (Input.GetKeyDown(KeyCode.W))
                return (int)KeyCode.W;

            if (Input.GetKeyDown(KeyCode.E))
                return (int)KeyCode.E;

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
