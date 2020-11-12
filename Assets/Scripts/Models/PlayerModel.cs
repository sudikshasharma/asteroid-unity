public class PlayerModel
{
    public int currentHealth;
    public PlayerModel(int maxHealth)
    {
        currentHealth = maxHealth;
    }

    public void DamageTaken(int healthReduceFactor)
    {
        currentHealth -= healthReduceFactor;
    }
}
