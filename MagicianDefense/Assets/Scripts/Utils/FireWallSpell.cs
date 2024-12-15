using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class FireWallSpell : Spell
    {
        public FireWallSpell() 
        {
            this.Name = "FireWall";
            this.Type = SpellType.Fire;
            this.Cost = 200;
            this.Range = 1;
            this.Damage = 0;
            this.CoolDown = 20;
        }

        public override void Cast(Transform caster)
        {
            base.Cast(caster);

            Debug.Log($"{Name} casted.");
        }
    }
}
