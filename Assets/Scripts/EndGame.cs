using UnityEngine;

public class EndGame : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            print("End Game");
            UI_Manager.instance.endScreen.SetActive(true);
            Time.timeScale = 0;

            int initialECount = GameManager.instance.initialEnemyCount;
            int finalECount = GameManager.instance.enemyContainer.transform.childCount;

            int enemiesLeft = initialECount - finalECount;

            UI_Manager.instance.killCountText.SetText("Enemies defeated: " + enemiesLeft.ToString() + "/" + initialECount.ToString());
        }
    }
}
