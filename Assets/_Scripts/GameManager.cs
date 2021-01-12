using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int m_Errors = 0;
    [SerializeField] private int m_Score;
    [SerializeField] private Rect AreaBoundaries;

    public static GameManager Instance;

    public delegate void Scoring();

    public event Scoring OnScorePoint;
    public event Scoring OnErrorPoint;

    public delegate void GameState();

    public event GameState OnGameOver;
    public event GameState OnGameStart;
    public event GameState OnGameReset;

    internal int GetErrorCount()
    {
        return m_Errors;
    }
    internal int GetScore()
    {
        return m_Score;
    }

    public void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    //Used for the onClick event to start game
    public void GameStart()
    {
        OnGameStart();
    }

    public void Reset()
    {
        m_Errors = 0;
        m_Score = 0;

        OnGameReset();
    }

    internal void ScorePoint()
    {
        m_Score++;
        OnScorePoint();
    }

    internal void ErrorPoint()
    {
        m_Errors++;
        OnErrorPoint();

        if(m_Errors == 5)
        {
            OnGameOver();
        }
    }

    //Draws the area boundary set 
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(AreaBoundary().xMin, AreaBoundary().yMin), new Vector3(AreaBoundary().xMin, AreaBoundary().yMax));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(AreaBoundary().xMin, AreaBoundary().yMax), new Vector3(AreaBoundary().xMax, AreaBoundary().yMax));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(AreaBoundary().xMin, AreaBoundary().yMin), new Vector3(AreaBoundary().xMax, AreaBoundary().yMin));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(AreaBoundary().xMax, AreaBoundary().yMax), new Vector3(AreaBoundary().xMax, AreaBoundary().yMin));
    }
    
    internal Rect AreaBoundary()
    {
        Rect rect = new Rect
        {
            position = new Vector2(AreaBoundaries.x - AreaBoundaries.width, AreaBoundaries.y - AreaBoundaries.height),
            size = new Vector2(AreaBoundaries.width * 2, AreaBoundaries.height * 2)
        };

        return rect;
    }
}

