using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class FireBallSpell : Spell
    {
        public FireBallSpell() 
        {
            this.Type = SpellType.Fire;
            this.Name = "Fireball";
            this.Cost = 100;
            this.Range = 10;
            this.Damage = 10;
            this.CoolDown = 2;
        }
        public override void Cast(Transform caster)
        {
            base.Cast(caster);

            Debug.Log($"{Name} casted.");
        }
    }
}
