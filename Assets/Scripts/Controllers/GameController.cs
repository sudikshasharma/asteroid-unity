using UnityEngine;
using Asteroids.Views;
using Asteroids.Models;
using Asteroids.Managers;

namespace Asteroids.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameView gameView;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private AsteroidController asteroidController;

        void Start()
        {
            Init();
        }

        private void Init()
        {
            SetGameState(GameStateModel.MENU);
            gameView.Init();
            playerController.Init();
            asteroidController.Init();
        }

        public void SetGameState(GameStateModel gameState)
        {
            GameManager.Instance.currentGameState = gameState;
        }

        public void SetCurrentBulletType(BulletTypeModel bulletType)
        {
            playerController.SetCurrentBulletType(bulletType);
        }

        public void ChangeShipSprite(Sprite shipSprite)
        {
            playerController.ChangeShipSprite(shipSprite);
        }

        public void PlayGame()
        {
            SetGameState(GameStateModel.GAME);
            gameView.Play();
            asteroidController.StartAsteroidSpawn();
            playerController.MoveUp();
            StartCoroutine(gameView.ScrollBg());
        }

        public void GameOver()
        {
            SetGameState(GameStateModel.GAMEOVER);
        }
    }
}
