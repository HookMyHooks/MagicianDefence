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
                Instantiate(fireballPrefab, wandTip.position, wandTip.rotation);
            
                //spellManager.SelectedCategory = SpellType.Fire;
                //_animator.SetInteger("AnimState", 1);
                //isMoving = true;
                //Debug.Log(_animator.GetInteger("AnimState"));
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                spellManager.SelectedCategory = SpellType.Earth;
            }

            int key = GetCurrentButton();

            if(key != 0)
            {
                
                var spell = spellManager.GetSpell(key);
                //spell.Cast();
                Mana -= spell.Cost;
            }
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
