using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public static Actions Instance;

    private void Awake()
    {
        Instance = this;
    }
}
