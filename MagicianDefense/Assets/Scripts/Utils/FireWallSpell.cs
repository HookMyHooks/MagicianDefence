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

        public override bool Cast(Transform transform)
        {
            base.Cast(transform);

            Debug.Log($"{Name} casted.");

            return true;
        }
    }
}
