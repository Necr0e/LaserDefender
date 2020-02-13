using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{ 
    private TextMeshProUGUI scoreText;
    private GameSession currentSession;
    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        currentSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        scoreText.text = currentSession.GetScore().ToString();
    }
}