using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class TeleportToTurret : MonoBehaviour
    {
        [Header("Turret Positions")]
        public Transform[] turretPositions; // Array cu pozițiile turetelor

        [Header("Teleport Settings")]
        public float teleportOffsetY = 1.0f; // Înălțimea la care apare wizardul după teleportare

        void Update()
        {
            CheckTeleportInput();
        }

        private void CheckTeleportInput()
        {
            if (turretPositions == null || turretPositions.Length < 5)
            {
                Debug.LogWarning("Please assign all 5 turret positions in the Inspector!");
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TeleportTo(turretPositions[0]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                TeleportTo(turretPositions[1]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                TeleportTo(turretPositions[2]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                TeleportTo(turretPositions[3]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                TeleportTo(turretPositions[4]);
            }
        }

        private void TeleportTo(Transform turretPosition)
        {
            if (turretPosition != null)
            {
                Vector3 newPosition = turretPosition.position + Vector3.up * teleportOffsetY;
                transform.position = newPosition;
                Debug.Log($"Wizard teleported to {turretPosition.name} at {newPosition}");
            }
            else
            {
                Debug.LogWarning("Target turret position is not assigned.");
            }
        }
    }
}
