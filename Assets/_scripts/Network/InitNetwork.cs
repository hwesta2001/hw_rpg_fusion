using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class InitNetwork : NetworkBehaviour
{

    public List<GameObject> activatedObjects = new();
    public List<GameObject> deActivatedObjects = new();
    public List<GameObject> deletedObjects = new();


    public override void Spawned()
    {
        base.Spawned();
        Debug.Log("Init Network");
        if (Object.HasStateAuthority)
        {
            foreach (GameObject obj in activatedObjects)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
            foreach (GameObject obj in deActivatedObjects)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
            foreach (GameObject obj in deletedObjects)
            {
                if (obj != null)
                {
                    Destroy(obj, 1f);
                }
            }

        }

        Runner.Despawn(Object);
    }
}
