using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu; 
    [SerializeField]
    GameObject startPanel;
    
    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    TMP_Text resultsText;

    [SerializeField]
    BirdPhysics playerBird;
    
    [SerializeField]
    GameHUD gameHUD;

    static bool skipStartPanel;
    bool isRestarting;
    enum GameState    { Start, Playing, Paused, GameOver }
    
    GameState currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ChangeState(GameState.Start); 
        
        if (skipStartPanel)
        {
            skipStartPanel = false;
            StartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentState == GameState.Start)
            {
                StartGame();
            }
            else if (currentState == GameState.GameOver)
            {
                RestartGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                ChangeState(GameState.Paused);
            }
            else if (currentState == GameState.Paused)
            {
                ChangeState(GameState.Playing);
            }
        }
    }
    void ChangeState(GameState newState)
    {
        currentState = newState;

        if (currentState == GameState.Start)
        {
            startPanel.SetActive(true);
        }
        else
        {
            startPanel.SetActive(false);
        }

        if (currentState == GameState.Paused)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }

        if (currentState == GameState.Playing)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }

        if (currentState == GameState.GameOver)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(false);
        }
    }
    public void StartGame()
    {
        if (currentState == GameState.Start)
        {
            startPanel.SetActive(false);
            StartCoroutine(BeginGame());
        }
    }
    IEnumerator BeginGame()
    {
        yield return null;

        ChangeState(GameState.Playing);
    }
    public void ResumeGame()
    {
        ChangeState(GameState.Playing);
    }

    public void RestartGame()
    {
        if (!isRestarting)
        {
            isRestarting = true;
            StartCoroutine(ReloadGame());
        }
    }
    // Corutina: espera un fotograma abans d'iniciar el joc per evitar que el clic del botó impulsi l'ocell.
    IEnumerator ReloadGame()    // Exemple amb temps concret:   Esperar dos segons de temps de joc 'yield return new WaitForSeconds(2f);'
    {                           //                              Esperar dos segons reals, fins i tot amb el joc pausat 'yield return new WaitForSecondsRealtime(2f);'
        yield return null;      // Esperar fins al següent fotograma

        skipStartPanel = true;
        Time.timeScale = 0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame() 
    {
        Application.Quit();
    }
    public void ShowGameOver()
    {
        if (currentState == GameState.Playing)
        {
            ChangeState(GameState.GameOver);

            float finalTime = gameHUD.GameTime;

            int bestScore = PlayerPrefs.GetInt("BestScore", 0);
            float bestSurvivalTime = PlayerPrefs.GetFloat("BestSurvivalTime", 0f);

            if (playerBird.Score > bestScore)
            {
                bestScore = playerBird.Score;
                PlayerPrefs.SetInt("BestScore", bestScore);
            }

            if (finalTime > bestSurvivalTime)
            {
                bestSurvivalTime = finalTime;
                PlayerPrefs.SetFloat("BestSurvivalTime", bestSurvivalTime);
            }

            PlayerPrefs.Save();

            resultsText.text =  "Puntuació: "               + playerBird.Score +
                                "\nObstacles superats: "    + playerBird.ObstaclesPassed +
                                "\nTemps de partida: "      + FormatTime(finalTime) +
                                "\n\nRècord de puntuació: " + bestScore +
                                "\nRècord de temps sobreviscut: "  + FormatTime(bestSurvivalTime) +
                                "\n\nPrem 'Enter' o el botó 'Reiniciar partida' per tornar a jugar.";

        }
    }
    string FormatTime(float time)
    {
        int totalSeconds = Mathf.FloorToInt(time);

        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds / 60) % 60;
        int seconds = totalSeconds % 60;

        return $"{hours:00}:{minutes:00}:{seconds:00}";
    }
}
