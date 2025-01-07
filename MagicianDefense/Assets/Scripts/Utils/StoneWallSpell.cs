using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class StoneWallSpell : Spell
    {
        private MonoBehaviour monoBehaviour;
        private GameObject stoneWallPrefab;
        private float stoneWallDistance = 50f; // Distanța față de magician

        public StoneWallSpell(MonoBehaviour monoBehaviour, GameObject stoneWallPrefab)
        {
            this.monoBehaviour = monoBehaviour;
            this.stoneWallPrefab = stoneWallPrefab;
            this.Name = "StoneWall";
            this.Type = SpellType.Earth;
            this.Cost = 100;
            this.CoolDown = 10;
        }

        public override bool Cast(Transform magicianTransform)
        {
            if (CanCast())
            {
                base.Cast(magicianTransform);
                Debug.Log($"{Name} casted.");
                SpawnStoneWall(magicianTransform);
                return true;
            }
            else
            {
                float timeLeft = (lastCastTime + CoolDown) - Time.time;
                Debug.Log($"{Name} is on cooldown. Try again in {timeLeft:F2} seconds.");
                return false;
            }
        }

        private void SpawnStoneWall(Transform magicianTransform)
        {
            // Poziționează zidul mai în față
            Vector3 spawnPosition = magicianTransform.position + magicianTransform.forward * stoneWallDistance;

            // Ajustează înălțimea
            spawnPosition.y = magicianTransform.position.y + 15f;

            // Instanțiază zidul de piatră
            GameObject stoneWall = GameObject.Instantiate(stoneWallPrefab, spawnPosition, Quaternion.identity);

            // Aliniază rotația
            stoneWall.transform.rotation = Quaternion.Euler(0, magicianTransform.eulerAngles.y, 0);

            // Distruge după 10 secunde
            GameObject.Destroy(stoneWall, 10f);
        }

    }
}
