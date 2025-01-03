using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Wizard : MonoBehaviour
    {
        [Header("References")]
        public Transform wandTip;           // Child la toiag
        public GameObject fireballPrefab;   // Prefab pentru mingea de foc
        public GameObject fireRingPrefab;
        public int currentMana;
        public int health;

        [Header("Fireball Settings")]
        public float fireballSpeed = 1f;

        [Header("Fire Ring Settings")]
        public float fireRingDistance = 5f; // Distanța medie față de personaj
        void Start()
        {
            var spellManagerObject = new GameObject("SpellManager");
            spellManager = spellManagerObject.AddComponent<SpellManager>();

            spellManager.Initialize(SpellType.Fire, wandTip, fireballPrefab, fireballSpeed, fireRingPrefab, fireRingDistance);
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
    }
}
