using UnityEngine;
using Asteroids.Models;
using Asteroids.Managers;

public class ShieldHandle : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.currentGameState == GameStateModel.GAME)
        {
            transform.Translate(Vector2.down * 1f * Time.deltaTime);
        }
    }
}
