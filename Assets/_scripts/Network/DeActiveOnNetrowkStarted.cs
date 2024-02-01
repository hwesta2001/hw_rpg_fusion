using UnityEngine;

public class DeActiveOnNetrowkStarted : MonoBehaviour
{
    [SerializeField] bool destroyObject;

    void OnEnable() => GameState.Ins.OnGameStateChanged += DeactiveOrDestroyObject;
    void OnDisable() => GameState.Ins.OnGameStateChanged -= DeactiveOrDestroyObject;

    void DeactiveOrDestroyObject(GameStates gameState)
    {
        if (gameState != GameStates.Connected) return;
        if (destroyObject)
        {
            Destroy(gameObject, .5f);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
