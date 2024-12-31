using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [Header("Fireball Settings")]
        public float fireballSpeed = 1f;

        [Header("Fire Ring Settings")]
        public float fireRingDistance = 5f; // Distanța medie față de personaj
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Health = 100;
            Mana = 500;
            spellManager = new SpellManager(SpellType.Fire);
        }

        public int Health { get; set; }

        public int Mana {  get; set; }
        
        private SpellManager spellManager;


        private void Update()
        {
            bool isCasting = false;


            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //aici daca apas pe 1 apare fireball, dar inca nu are implementari de mana si damage
                ShootFireball();

                //spellManager.SelectedCategory = SpellType.Fire;
                //_animator.SetInteger("AnimState", 1);
                //isMoving = true;
                //Debug.Log(_animator.GetInteger("AnimState"));
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //aici daca apas pe tasta 2 apare FireRing
                SpawnFireRing();
                //spellManager.SelectedCategory = SpellType.Earth;
            }

            int key = GetCurrentButton();

            if(key != 0)
            {
                
                var spell = spellManager.GetSpell(key);
                //spell.Cast();
                Mana -= spell.Cost;
            }
        }

        private void ShootFireball()
        {
            // Instanțiază mingea de foc la poziția și rotația toiagului
            GameObject fireball = Instantiate(fireballPrefab, wandTip.position, wandTip.rotation);

            // Adaugă mișcare folosind Rigidbody
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculează direcția bazată pe orientarea toiagului (wandTip.forward)
                Vector3 shootDirection = transform.forward.normalized;

                // Aplică mișcare mingii în direcția privirii Wizard-ului
                rb.linearVelocity = shootDirection * fireballSpeed;

                // Aplică viteza mingii
                rb.linearVelocity = shootDirection * fireballSpeed;
            }

            // Distruge mingea după 5 secunde
            Destroy(fireball, 5f);
        }

        private void SpawnFireRing()
        {
            Vector3 spawnPosition = transform.position + transform.forward * fireRingDistance;
            spawnPosition.y = transform.position.y; // Menține aceeași înălțime

            Quaternion fireRingRotation = Quaternion.Euler(-90, transform.eulerAngles.y, 0);

            GameObject fireRing = Instantiate(fireRingPrefab, spawnPosition, fireRingRotation);

            Destroy(fireRing, 5f);
        }

     

        int GetCurrentButton()
        {
            if (Input.GetKey(KeyCode.Q))
                return (int)KeyCode.Q;

            if (Input.GetKey(KeyCode.W))
                return (int)KeyCode.W;

            if (Input.GetKey(KeyCode.E))
                return (int)KeyCode.E;

            return 0;
        }
    }
}
