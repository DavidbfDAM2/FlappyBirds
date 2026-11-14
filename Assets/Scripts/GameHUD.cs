using UnityEngine;
using TMPro;

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
    
    [SerializeField]
    GameSceneController sceneController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int totalSeconds = Mathf.FloorToInt(sceneController.GameTime);           // acumulem el temps de joc i actualitzem els textos de vides, punts, obstacles i cronometre
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds / 60) % 60;
        int seconds = totalSeconds % 60;

        obstaclesText.text  = $"Obstacles superats: {playerBird.ObstaclesPassed}";
        scoreText.text      = $"Puntuació: {playerBird.Score}";
        livesText.text      = $"Vides: {playerBird.CurrentLives}";
        timeText.text       = $"Temps de partida: {hours:00}:{minutes:00}:{seconds:00}";
    }
}