using UnityEngine;

public class InitSetter : MonoBehaviour
{
    [SerializeField] bool EnableWhenInit;
    void Awake()
    {       
        if (EnableWhenInit)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
