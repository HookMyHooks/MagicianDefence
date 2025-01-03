using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class FireRingSpell : Spell
    {
        private MonoBehaviour monoBehaviour; // Reference to MonoBehaviour
        private Transform wandTip;
        private GameObject fireRingPrefab;
        private float fireRingDistance;
        public float Radius { get; set; }

        public FireRingSpell() 
        {
            //this.monoBehaviour = monoBehaviour;
            //this.wandTip = wandTip;
            //this.fireRingPrefab = fireRingPrefab;
            //this.fireRingDistance = fireRingDistance;
            this.Name = "FireZone";
            this.Type = SpellType.Fire;
            this.Cost = 200;
            this.CoolDown = 15;
            this.Damage = 20; 
            this.Radius = 1f;
        }

        public override bool Cast()
        {
            base.Cast();
            Debug.Log($"{Name} casted.");
            return true;
        }

        //private void SpawnFireRing()
        //{
        //    Vector3 spawnPosition = transform.position + transform.forward * fireRingDistance;
        //    spawnPosition.y = transform.position.y; // Menține aceeași înălțime

        //    Quaternion fireRingRotation = Quaternion.Euler(-90, transform.eulerAngles.y, 0);

        //    GameObject fireRing = GameObject.Instantiate(fireRingPrefab, spawnPosition, fireRingRotation);

        //    GameObject.Destroy(fireRing, 5f);
        //}
    }
}
