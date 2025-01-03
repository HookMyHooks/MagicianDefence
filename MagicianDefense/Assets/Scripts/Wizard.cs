using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Wizard : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        [Header("References")]
        public Transform wandTip;           // Child la toiag
        public GameObject fireballPrefab;   // Prefab pentru mingea de foc
        public GameObject fireRingPrefab;
        public int mana;
        public int health;

        [Header("Fireball Settings")]
        public float fireballSpeed = 1f;

        [Header("Fire Ring Settings")]
        public float fireRingDistance = 5f; // Distanța medie față de personaj
        void Start()
        {
            spellManager = new SpellManager(SpellType.Fire, wandTip, fireballPrefab, fireballSpeed, fireRingPrefab, fireRingDistance);
        }

        private SpellManager spellManager;


        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    //aici daca apas pe tasta 2 apare FireRing
            //    SpawnFireRing();
            //    //spellManager.SelectedCategory = SpellType.Earth;
            //}

            int key = GetCurrentButton();

            if(key != 0)
            {
                var spell = spellManager.GetSpell(key);
                if (mana < spell.Cost) return;
                bool hasCasted = spell.Cast(transform);
                if(hasCasted) mana -= spell.Cost;
            }
        }

        //private void SpawnFireRing()
        //{
        //    Vector3 spawnPosition = transform.position + transform.forward * fireRingDistance;
        //    spawnPosition.y = transform.position.y; // Menține aceeași înălțime

        //    Quaternion fireRingRotation = Quaternion.Euler(-90, transform.eulerAngles.y, 0);

        //    GameObject fireRing = Instantiate(fireRingPrefab, spawnPosition, fireRingRotation);

        //    Destroy(fireRing, 5f);
        //}

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
