using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameHUD : MonoBehaviour
{
    [SerializeField]
    BirdPhysics playerBird;
    
    [SerializeField]
    TMP_Text timeText;
    
    [SerializeField]
    TMP_Text livesText;
    
    [SerializeField]
    TMP_Text scoreText;
    
    [SerializeField]
    TMP_Text obstaclesText;

    private float gameTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = gameTime + Time.deltaTime;
        int totalSeconds = Mathf.FloorToInt(gameTime);
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds / 60) % 60;
        int seconds = totalSeconds % 60;

        obstaclesText.text = $"Obstacles superats: {playerBird.ObstaclesPassed}";
        scoreText.text = $"Puntuació: {playerBird.Score}";
        livesText.text = $"Vides: {playerBird.CurrentLives}";
        timeText.text = $"Temps de partida: {hours:00}:{minutes:00}:{seconds:00}";
    }
}
