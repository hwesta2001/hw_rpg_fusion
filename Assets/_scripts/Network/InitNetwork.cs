using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class InitNetwork : MonoBehaviour
{
    public List<GameObject> activatedObjects = new();
    public List<GameObject> deactivatedObjects = new();
    public List<GameObject> deletedObjects = new();

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GameState.CurrentState == GameStates.Connected);
        InitAll();
    }

    private void InitAll()
    {
        foreach (GameObject obj in activatedObjects)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in deactivatedObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in deletedObjects)
        {
            Destroy(obj, 1f);
        }
        DebugText.ins.AddText("Init Network");
    }
}
