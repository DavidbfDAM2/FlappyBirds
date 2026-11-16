using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;     // guardem una instancia unica i accessible per tothom del GameManager    
    
    public bool skipStartPanel;             // indiquem si la nova escena ha de començar sense mostrar les instruccions
    int bestScore;
    float bestSurvivalTime;

    public int BestScore
    {
        get { return this.bestScore; }
    }

    public float BestSurvivalTime
    {
        get { return this.bestSurvivalTime; }
    }

    void Awake()
    {    // abans de lstart; conservem una unica instancia entre escenes, carreguem els records i eliminem els duplicats
        
        if (instance == null)       // si es el primer cop que es crea, guardem la instancia i li assignem 'DontDestroyOnLoad'
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            bestScore = PlayerPrefs.GetInt("BestScore", 0);
            bestSurvivalTime = PlayerPrefs.GetFloat("BestSurvivalTime", 0f);
        }
        
        else// si es una altre 'GameManager' leliminem perque nomes en volem un. (Patró Singleton)
        {
            Destroy(gameObject);
        }
    }
    public void UpdateRecords(int score, float survivalTime)    // actualitzem els records superats de puntuacio i supervivencia i guardem les dades amb 'PlayerPrefs'
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        if (survivalTime > bestSurvivalTime)
        {
            bestSurvivalTime = survivalTime;
            PlayerPrefs.SetFloat("BestSurvivalTime", bestSurvivalTime);
        }

        PlayerPrefs.Save();
    }
}