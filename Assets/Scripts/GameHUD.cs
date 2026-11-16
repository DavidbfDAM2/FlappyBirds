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

    // Inicialitzem els components abans de la primera actualització.    
    void Start()
    {        
    }

    // Executem aquestes comprovacions a cada fotograma.
    void Update()
    {
        int totalSeconds = Mathf.FloorToInt(sceneController.GameTime);           // consultem el temps del controlador i actualitzem els textos del marcador
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds / 60) % 60;
        int seconds = totalSeconds % 60;

        obstaclesText.text  = $"Obstacles superats: {playerBird.ObstaclesPassed}";
        scoreText.text      = $"Puntuació: {playerBird.Score}";
        livesText.text      = $"Vides: {playerBird.CurrentLives}";
        timeText.text       = $"Temps de partida: {hours:00}:{minutes:00}:{seconds:00}";
    }
}