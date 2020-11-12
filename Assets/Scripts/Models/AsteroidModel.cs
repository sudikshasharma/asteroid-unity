namespace Asteroids.Models
{
    public class AsteroidModel
    {
        // Havn't used it as of now (will use this model when Json will be used instead of GameData for Asteroid Specs)
        public AsteroidTypeModel asteroidType;
        public float asteroidSpeed;
        public float health;
        public float rotationSpeed;

        public AsteroidModel()
        {
            asteroidType = AsteroidTypeModel.BROWNBIG;
        }

        public void SetAsteroidModel(AsteroidTypeModel type, float speed, float maxhealth, float rotationSpeed)
        {
            asteroidType = type;
            asteroidSpeed = speed;
            health = maxhealth;
            this.rotationSpeed = rotationSpeed;
        }
    }
}
