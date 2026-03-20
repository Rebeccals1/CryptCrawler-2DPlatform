using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RestartGame()
    {
        GameSession session = FindFirstObjectByType<GameSession>();
        if (session != null)
            Destroy(session.gameObject);
        SceneManager.LoadScene("Level1");
    }
}