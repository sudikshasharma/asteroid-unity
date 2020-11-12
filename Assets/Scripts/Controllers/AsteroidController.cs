using UnityEngine;
using Asteroids.Views;
using Asteroids.Models;
using System.Collections;
using Asteroids.Managers;
using System.Collections.Generic;

namespace Asteroids.Controllers
{
    public class AsteroidController : MonoBehaviour
    {
        [SerializeField] private AsteroidView asteroidView;
        private AsteroidModel asteroidModel;

        public void StartAsteroidSpawn()
        {
            StartCoroutine(asteroidView.SpawnAsteroid());
        }

        public void Init()
        {
            asteroidView.CreateAsteroidPool();
        }
    }
}