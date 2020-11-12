public static class GameData
{
    public static int bulletPool = 50;
    public static int asteroidPool = 20;
    public static int maxPower = 100;
    public static float playerInputMaxDistance = 0.2f;
    public static float playerRotationRate = 0.15f;
    public static float bgScrollSpeed = 1f;
    public static float asteroidXSpawnGap = 5f;
    public static float asteroidSpawnTimeMin = 1f;
    public static float asteroidSpawnTimeMax = 2.5f;
    public static float asteroidDirectionX = 1f;
    public static float shieldTimer = 12f;
    public static float shieldTime = 7f;
    public static float asteroidLifeTime = 10f;
    public static float[] asteroidSpeed = { 1.7f, 2.3f, 2.0f, 2.6f };
    public static float[] asteroidRotationSpeed = { 0.2f, 0.4f, 0.35f, 0.47f };
    public static float[] asteroidHealth = { 170f, 100f, 2f, 130f };
    public static float spaceShipTouchFollowSpeed = 100f;
    public static float tripleBulletAngle = 20f; //in degrees
    public static int[] playerBulletPowerReduce = { 30, 50, 100 };
    public static int[] playerBulletLifeTime = { 2, 3, 4 };
    public static float[] playerBulletSwawnRate = { 0.2f, 0.3f, 0.3f };
    public static int[] playerBulletSpeed = { 10, 10, 7 };

}
