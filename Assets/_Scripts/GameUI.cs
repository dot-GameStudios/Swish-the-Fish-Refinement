using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

//This class handles all the UI elements in the game

public class GameUI : MonoBehaviour
{

    [Header("Game UI Screens")]
    [SerializeField] GameObject m_StartScreen;
    [SerializeField] GameObject m_GameScreen;
    [SerializeField] GameObject m_GameOverScreen;

    [SerializeField] TextMeshProUGUI m_ScoreText;

    [Header("The five X's for error images")]
    [SerializeField] Image[] m_RedErrors;



    // Start is called before the first frame update
    void Start()
    {
        foreach(Image img in m_RedErrors)
        {
            img.DOColor(new Color(1, 1, 1, 0.3f), 0);
        }

        GameManager.Instance.OnErrorPoint += IncreaseError;
        GameManager.Instance.OnScorePoint += IncreaseScore;
        GameManager.Instance.OnGameStart += Instance_OnGameStart;
        GameManager.Instance.OnGameOver += Instance_OnGameOver;
        GameManager.Instance.OnGameReset += Instance_OnGameReset;

        m_ScoreText.text = GameManager.Instance.GetScore().ToString();
    }

    private void Instance_OnGameReset()
    {
        m_GameOverScreen.SetActive(false);

        foreach (Image img in m_RedErrors)
        {
            img.DOColor(new Color(1, 1, 1, 0.3f), 0);
        }
        m_ScoreText.text = GameManager.Instance.GetScore().ToString();
    }

    private void Instance_OnGameOver()
    {
        m_GameOverScreen.SetActive(true);
    }

    private void Instance_OnGameStart()
    {
        m_StartScreen.SetActive(false);
        m_GameScreen.SetActive(true);
    }

    void IncreaseError()
    {
        m_RedErrors[GameManager.Instance.GetErrorCount() - 1].DOColor(Color.white, 0.4f);
    }

    void IncreaseScore()
    {
        m_ScoreText.text = GameManager.Instance.GetScore().ToString();
    }
}
