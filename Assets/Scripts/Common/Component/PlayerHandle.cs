using UnityEngine;
using Asteroids.Views;
using System.Collections;

public class PlayerHandle : MonoBehaviour
{
    private bool isShielded = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle" && !isShielded)
        {
            this.gameObject.SetActive(false);
            GameObject.Find("GameView").GetComponent<GameView>().OnGameOver();
        }
        if (other.gameObject.name == "Shield")
        {
            StartCoroutine(Shielded());
            other.gameObject.SetActive(false);
        }
    }

    private IEnumerator Shielded()
    {
        var timer = 0;
        isShielded = true;
        this.transform.GetChild(2).gameObject.SetActive(true);
        while (timer < GameData.shieldTime)
        {
            timer += 1;
            yield return new WaitForSeconds(1);
        }
        isShielded = false;
        this.transform.GetChild(2).gameObject.SetActive(false);
    }
}
