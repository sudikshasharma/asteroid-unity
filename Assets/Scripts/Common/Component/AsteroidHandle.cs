using UnityEngine;
using Asteroids.Views;
using Asteroids.Models;
using Asteroids.Managers;

public class AsteroidHandle : MonoBehaviour
{
    public AsteroidTypeModel asteroidType;
    public float asteroidSpeed;
    public float currentHealth;
    public float rotationSpeed;
    public Vector3 asteroidDirection;
    private bool isEnable;
    private float activeTime;
    private int rotDir;
    void OnEnable()
    {
        isEnable = true;
        activeTime = 0;
        rotDir = Random.Range(-1, 1);
    }

    void Update()
    {
        if (isEnable && GameManager.Instance.currentGameState == GameStateModel.GAME)
        {
            this.transform.position += asteroidDirection * asteroidSpeed * Time.deltaTime;
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,
            this.transform.eulerAngles.y, this.transform.eulerAngles.z + (GameData.asteroidRotationSpeed[(int)asteroidType]) * rotDir);
            activeTime += Time.deltaTime;
            if (activeTime >= GameData.asteroidLifeTime)
            {
                DisableAsteroid();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shield")
        {
            DisableAsteroid();
        }
    }

    private void CheckAsteroidDestruction()
    {
        if (currentHealth <= 0)
        {
            DisableAsteroid();
        }
    }

    public void DisableAsteroid()
    {
        isEnable = false;
        this.gameObject.SetActive(false);
    }

    public void SetAsteroidData(AsteroidTypeModel asteroidType, Vector3 currentDir)
    {
        this.asteroidType = asteroidType;
        asteroidSpeed = GameData.asteroidSpeed[(int)asteroidType];
        currentHealth = GameData.asteroidHealth[(int)asteroidType];
        rotationSpeed = GameData.asteroidRotationSpeed[(int)asteroidType];
        asteroidDirection = Vector3.Normalize(currentDir);
    }

    public void SetDirection(Vector2 direction)
    {
        asteroidDirection = direction;
    }

    public void OnCollision(float healthReduceRate)
    {
        currentHealth -= healthReduceRate;
        GameObject.Find("GameView").GetComponent<GameView>().IncrementScore();
        CheckAsteroidDestruction();
    }
}
