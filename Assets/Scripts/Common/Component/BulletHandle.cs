using UnityEngine;
using Asteroids.Models;
using Asteroids.Managers;

public class BulletHandle : MonoBehaviour
{
    public BulletTypeModel bulletType;
    public float bulletSpeed;
    public float healthReduceRate;
    public float bulletLifetime;
    public Vector3 bulletDirection;
    private bool isEnable;
    private float activeTime;

    void OnEnable()
    {
        isEnable = true;
        activeTime = 0;
    }

    void Update()
    {
        if (isEnable && GameManager.Instance.currentGameState == GameStateModel.GAME)
        {
            this.transform.position += bulletDirection * bulletSpeed * Time.deltaTime;
            activeTime += Time.deltaTime;
            if (activeTime >= bulletLifetime)
            {
                DisableBullet();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            other.gameObject.GetComponent<AsteroidHandle>().OnCollision(healthReduceRate);
            DisableBullet();
        }
    }

    public void DisableBullet()
    {
        isEnable = false;
        this.gameObject.SetActive(false);
    }

    public void SetBulletData(BulletModel bulletObject)
    {
        bulletType = bulletObject.bulletType;
        bulletSpeed = bulletObject.bulletSpeed;
        healthReduceRate = bulletObject.healthReduceRate;
        bulletLifetime = bulletObject.bulletLifetime;
    }

    public void SetDirection(Vector2 direction)
    {
        bulletDirection = direction;
    }
}
