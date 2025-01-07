using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public static TurretManager Instance;

    private List<GameObject> turrets = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void RegisterTurret(GameObject turret)
    {
        turrets.Add(turret);
    }

    public void UnregisterTurret(GameObject turret)
    {
        Debug.Log($"{turret.name} Unregistered");
        turrets.Remove(turret);
    }

    public List<GameObject> GetActiveTurrets()
    {
        return turrets;
    }
}
