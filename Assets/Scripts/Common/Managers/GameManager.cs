using UnityEngine;
using Asteroids.Models;


namespace Asteroids.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public GameStateModel currentGameState;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}