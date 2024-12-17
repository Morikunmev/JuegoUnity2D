using UnityEngine;
using UnityEngine.UI;

public class ControladorTexto : MonoBehaviour
{
    public Text scoreText;
    private JohnMovement player;
    private int maxItems = 20; // Añadimos el máximo de items

    void Start()
    {
        player = FindObjectOfType<JohnMovement>();
        if (player != null)
        {
            player.OnItemCollected += UpdateScore;
        }
        UpdateScore(0);
    }

    void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Items: {score}/{maxItems}";
        }
    }
}