using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class FireZoneSpell : Spell
    {
        public float Radius { get; set; }

        public FireZoneSpell() 
        {
            this.Name = "FireZone";
            this.Type = SpellType.Fire;
            this.Cost = 200;
            this.CoolDown = 15;
            this.Damage = 20; 
            this.Radius = 1f;
        }

        public override void Cast(Transform caster)
        {
            base.Cast(caster);
            Debug.Log($"{Name} casted.");
        }
    }
}
