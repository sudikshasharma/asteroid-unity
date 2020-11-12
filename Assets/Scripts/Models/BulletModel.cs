namespace Asteroids.Models
{
    public class BulletModel
    {
        public BulletTypeModel bulletType;
        public float bulletSpeed;
        public float healthReduceRate;
        public float bulletLifetime;
        public float bulletSpawnRate;

        public BulletModel()
        {
            bulletType = BulletTypeModel.SINGLE;
        }

        public void SetBulletModel(BulletTypeModel type, float speed, float healthReduce, float lifeTime, float spawnRate)
        {
            bulletType = type;
            bulletSpeed = speed;
            healthReduceRate = healthReduce;
            bulletLifetime = lifeTime;
            bulletSpawnRate = spawnRate;
        }
    }
}