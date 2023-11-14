using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public int hex_id;
    public Transform trans;
    public Vector3 pos;
    public bool moveable;

    public void SetHex(int hex_id, float scaleRate, bool moveable)
    {
        this.hex_id = hex_id;
        this.moveable = moveable;
        trans = transform;
        pos = trans.position * scaleRate;
    }
}
