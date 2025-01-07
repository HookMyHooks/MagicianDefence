using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class StoneBallSpell : Spell
    {
        private MonoBehaviour monoBehaviour; // Referin?? la MonoBehaviour pentru a folosi corutine (dac? e necesar)
        private Transform wandTip;           // Pozi?ia de lansare
        private GameObject stoneBallPrefab;  // Prefab-ul pentru StoneBall
        private float stoneBallSpeed;        // Viteza de aruncare

        public StoneBallSpell(MonoBehaviour monoBehaviour, Transform wandTip, GameObject stoneBallPrefab, float stoneBallSpeed)
        {
            this.monoBehaviour = monoBehaviour;
            this.wandTip = wandTip;
            this.stoneBallPrefab = stoneBallPrefab;
            this.stoneBallSpeed = stoneBallSpeed;
            this.Type = SpellType.Earth;
            this.Name = "StoneBall";
            this.Cost = 50;
            this.Range = 15;
            this.Damage = 20;
            this.CoolDown = 3;
        }

        public override bool Cast(Transform position)
        {
            // Verific? dac? vraja poate fi lansat?
            if (CanCast())
            {
                base.Cast(position); // Actualizeaz? timpul ultimei lans?ri
                Debug.Log($"{Name} casted.");
                ShootStoneBall();
                return true;
            }
            else
            {
                float timeLeft = (lastCastTime + CoolDown) - Time.time;
                Debug.Log($"{Name} is on cooldown. Try again in {timeLeft:F2} seconds.");
                return false;
            }
        }

        private void ShootStoneBall()
        {
            // Instan?iaz? mingea de piatr? la pozi?ia toiagului
            GameObject stoneBall = GameObject.Instantiate(stoneBallPrefab, wandTip.position, wandTip.rotation);

            // Adaug? Rigidbody dac? nu exist? deja
            Rigidbody rb = stoneBall.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = stoneBall.AddComponent<Rigidbody>();
            }

            // Seteaz? propriet??ile Rigidbody
            rb.useGravity = true; // Mingea de piatr? cade dup? ce este aruncat?
            rb.mass = 2f;         // Ajusteaz? masa pentru a simula greutatea
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            // Calculeaz? direc?ia de aruncare
          Vector3 shootDirection = wandTip.forward;
          shootDirection.y = 0;
          shootDirection.x = 0;

            // Aplic? for?? pentru a arunca mingea
           rb.linearVelocity = shootDirection * stoneBallSpeed;

            // Distruge mingea dup? 5 secunde pentru optimizare
          //  GameObject.Destroy(stoneBall, 5f);
        }
    }
}
