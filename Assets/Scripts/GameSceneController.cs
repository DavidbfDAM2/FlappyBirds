using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameSceneController : MonoBehaviour
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
        
    enum GameState    { Start, Playing, Paused, GameOver }
    GameState currentState;  
    float gameTime = 0f;
    bool isRestarting;
    
    public bool IsPlaying
    {
        get { return this.currentState == GameState.Playing; }
    }
    public float GameTime
    {
        get { return this.gameTime; }
    }
    void Awake() // sexecuta abans de Start()
    {
        ChangeState(GameState.Start);    // posem lescena en lestat inicial amb les instruccions visibles i el temps aturat

    }
    
    void Start() 
    {
        if (GameManager.instance.skipStartPanel)     // comsultem el gestor persistent i no mostrem les instruccions del joc si venim dun reinici
        {
            GameManager.instance.skipStartPanel = false;
            StartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying) // nomes comptem el temps si estem en partida
        {
            gameTime = gameTime + Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Return))    // gestionem 'Enter' per començar o reiniciar i 'Esc' per alternar entre joc i pausa
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
    void ChangeState(GameState newState)    // canviem lestat de la partida, mostrem el panell corresponent i ajustem el pas del temps
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
        if (currentState == GameState.Start)    // amaguem les instruccions i programem linici de la partida si encara som al menu inicial
        {
            startPanel.SetActive(false);
            StartCoroutine(BeginGame());
        }
    }
    
    public void ResumeGame()
    {
        ChangeState(GameState.Playing);    // tornem a lestat de joc i reprenem el temps de la partida

    }
    
    public void RestartGame()
    {
        if (!isRestarting)    // iniciem la recarrega de lescena una sola vegada per cada peticio de reinici
        {
            isRestarting = true;
            StartCoroutine(ReloadGame());
        }
    }

    // corutina; esperem un fotograma abans diniciar el joc per evitar que el clic del boto impulsi locell; indiquem que someti el menu inicial i recarreguem lescena amb el temps aturat

    IEnumerator ReloadGame()                                            // ------------->   EXEMPLES amb temps concret:   - esperar dos segons de temps de joc 'yield return new WaitForSeconds(2f);'
    {                                                                   //                                                - esperar dos segons reals, fins i tot amb el joc pausat 'yield return new WaitForSecondsRealtime(2f);'
        yield return null;      // esperem fins al seguent fotograma

        GameManager.instance.skipStartPanel = true;
        Time.timeScale = 0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator BeginGame()     // corituna; esperem un fotograma abans dactivar el joc per evitar que el clic inicial doni un impuls
    {
        yield return null;      // esperem fins al seguent fotograma

        ChangeState(GameState.Playing);
    }

    public void ExitGame() 
    {
        Application.Quit();    // tanca laplicacio; no atura el mode 'Play' de leditor ni tanca la pestanya web 
    }
        
    public void ShowGameOver()// aturem una partida activa, actualitzem els records i mostrem els resultats finals
    {
        if (currentState == GameState.Playing)
        {
            ChangeState(GameState.GameOver);            

            GameManager.instance.UpdateRecords(playerBird.Score, gameTime);

            resultsText.text =  "Puntuació: "                       + playerBird.Score +
                                "\nObstacles superats: "            + playerBird.ObstaclesPassed +
                                "\nTemps de partida: "              + FormatTime(gameTime) +
                                "\n\nRècord de puntuació: "         + GameManager.instance.BestScore +
                                "\nRècord de temps sobreviscut: "   + FormatTime(GameManager.instance.BestSurvivalTime) +
                                "\n\nPrem 'Enter' o el botó 'Reiniciar partida' per tornar a jugar.";
        }
    }

    string FormatTime(float time)      // convertim els segons a un text amb el format hores:minuts:segons sense decimals
    {
        int totalSeconds = Mathf.FloorToInt(time);

        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds / 60) % 60;
        int seconds = totalSeconds % 60;

        return $"{hours:00}:{minutes:00}:{seconds:00}";
    }
}