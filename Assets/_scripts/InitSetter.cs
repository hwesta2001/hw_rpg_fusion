using UnityEngine;

public class InitSetter : MonoBehaviour
{
    [SerializeField] bool EnableWhenInit;
    void Start()
    {      
        gameObject.SetActive(EnableWhenInit);
        enabled = false;    
    }
}
