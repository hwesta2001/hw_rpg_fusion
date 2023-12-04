using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedActivator : NetworkBehaviour
{
    [SerializeField] List<GameObject> ObjsHasAutorithy = new();

    public override void Spawned()
    {
        if (!HasStateAuthority) return;
        foreach (var item in ObjsHasAutorithy)
        {
            item.SetActive(true);
        }
    }
}
