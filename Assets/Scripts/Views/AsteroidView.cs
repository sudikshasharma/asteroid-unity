using UnityEngine;
using Asteroids.Models;
using Asteroids.Managers;
using System.Collections;
using Asteroids.Controllers;
using System.Collections.Generic;

namespace Asteroids.Views
{
    public class AsteroidView : MonoBehaviour
    {
        [SerializeField] private GameObject asteroidPrefab;
        [SerializeField] private Transform asteroidParent;
        [SerializeField] private Sprite[] asteroidSprite;
        private GameObject[] asteroidPool;
        private AsteroidTypeModel currentAsteroidModel;
        private Vector2 asteroidDirection;

        public void CreateAsteroidPool()
        {
            asteroidPool = new GameObject[GameData.asteroidPool];
            for (int i = 0; i < asteroidPool.Length; i++)
            {
                asteroidPool[i] = Instantiate(asteroidPrefab);
                asteroidPool[i].SetActive(false);
                asteroidPool[i].transform.SetParent(asteroidParent);
            }
        }

        public IEnumerator SpawnAsteroid()
        {
            var xPos = 0f;
            while (GameManager.Instance.currentGameState == GameStateModel.GAME)
            {
                for (int i = 0; i < asteroidPool.Length; i++)
                {
                    if (!asteroidPool[i].activeSelf)
                    {
                        currentAsteroidModel = (AsteroidTypeModel)Random.Range(0, System.Enum.GetValues(typeof(AsteroidTypeModel)).Length);
                        asteroidPool[i].transform.position = new Vector2(Random.Range(-GameData.asteroidXSpawnGap, GameData.asteroidXSpawnGap), asteroidParent.transform.position.y);
                        xPos = Random.Range(0, GameData.asteroidDirectionX);
                        if (asteroidPool[i].transform.position.x > 0)
                        {
                            xPos = -xPos;
                        }
                        asteroidDirection = new Vector3(xPos, -1, 0);
                        asteroidPool[i].GetComponent<AsteroidHandle>().SetAsteroidData(currentAsteroidModel, asteroidDirection);
                        asteroidPool[i].GetComponent<SpriteRenderer>().sprite = asteroidSprite[(int)currentAsteroidModel];
                        asteroidPool[i].SetActive(true);
                        break;
                    }
                }
                yield return new WaitForSeconds(Random.Range(GameData.asteroidSpawnTimeMin, GameData.asteroidSpawnTimeMax));
            }
        }
    }
}