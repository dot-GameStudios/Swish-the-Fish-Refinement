using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager Instance;

    [SerializeField] List<Fish> m_FishType;         //For all types of fish that is going to be used in the game

    [SerializeField] int m_StartingFish;            //The max amount of fishes that are able to be spawned
    [SerializeField] int m_FishCount;               //Keeps track of the total amount of fish that have spawned

    [SerializeField] List<Fish> m_FishList;         //Tracks all spawned fish during gameplay

    [SerializeField] Stack<int> m_FishIndexes;      //Ties with m_FishList, keeps track of destroyed fish index

    [SerializeField] float m_MinFishSpeed = 2;      //Min and Max speed for the fishes
    [SerializeField] float m_MaxFishSpeed = 8;

    [SerializeField] GameObject m_PlayerShark;
    public Emotion m_TargetEmotion;

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        m_FishIndexes = new Stack<int>(20);

        GameManager.Instance.OnGameStart += Instance_OnGameStart;
        GameManager.Instance.OnGameReset += Instance_OnGameReset;
        GameManager.Instance.OnGameOver += Instance_OnGameOver;
    }

    private void Instance_OnGameOver()
    {
        CancelInvoke(nameof(SpawnFish));
    }

    private void Instance_OnGameReset()
    {
        for (int i = 0; i < m_FishList.Count; i++)
        {
            if (m_FishList[i])
            {
                Fish tempFish = m_FishList[i];
                Destroy(tempFish.gameObject);
            }
        }
        m_FishList.Clear();
        m_FishCount = 0;
        Instance_OnGameStart();
    }

    private void Instance_OnGameStart()
    {
        m_FishList = new List<Fish>();
        InvokeRepeating(nameof(SpawnFish), 0, 1);
        InvokeRepeating(nameof(CheckFishList), 15, 10);   
    }

    public void SpawnFish()
    {
        //Selects a random fish from the list
        Fish temp = m_FishType[Random.Range(0, m_FishType.Count)];

        float x = Random.Range(0, 2) == 0 ? GameManager.Instance.AreaBoundary().xMin : GameManager.Instance.AreaBoundary().xMax;
        float y = Random.Range(GameManager.Instance.AreaBoundary().yMin, GameManager.Instance.AreaBoundary().yMax);

        temp.transform.position = new Vector2(x, y);
        temp.InitializeFish(EmotionManager.Instance.GetRandomEmotion(), RandomWaypoint(), Random.Range(m_MinFishSpeed, m_MaxFishSpeed));

        if (m_FishList.Count == m_StartingFish)
        {
            if (m_FishIndexes.Count == 0)
                return;

            if (m_FishList.Contains(m_FishList[m_FishIndexes.Peek()]))
            {
                temp.name = "Fish " + ++m_FishCount;
                m_FishList[m_FishIndexes.Pop()] = Instantiate(temp, transform);
            }
        }
        else
        {
            temp.name = "Fish " + ++m_FishCount;
            m_FishList.Add(Instantiate(temp, transform));
        }
    }

    public void CheckFishList()
    {
        Fish m_FurthestFish = null;

        float m_CurrentHighestDistance = 0;

        int m_NumberOfCorrectFish = 0;

        for (int i = 0; i < m_FishList.Count; i++)
        {
            if (m_FishList[i])
            {
                if (Vector3.Distance(m_FishList[i].transform.position, m_PlayerShark.transform.position) > m_CurrentHighestDistance) //find a fish that is far away from the player
                {
                    m_FurthestFish = m_FishList[i];
                }

                if (EmotionManager.Instance.CompareEmotion(m_TargetEmotion, m_FishList[i].GetEmotion())) //compare emotions
                {
                    m_NumberOfCorrectFish++;
                }

            }
        }
        
        if (m_NumberOfCorrectFish == 0) //if there are no fish with the emotion the shark needs, change the emotion of furthestFish to the emotion the shark needs
        {
            ChangeFishEmotion(m_FurthestFish, EmotionManager.Instance.GetOppositeEmotion(m_TargetEmotion));
        }
    }

    public void ChangeFishEmotion(Fish selectedFish, Emotion targetEmotion) //the fish you want to change the emotion of, and the emotion you are changing it to
    {
        selectedFish.SetEmotion(targetEmotion);
    }

    public Vector3 RandomWaypoint()
    {
        Vector3 newPoint = new Vector3
        {
            x = Random.Range(GameManager.Instance.AreaBoundary().xMin, GameManager.Instance.AreaBoundary().xMax),
            y = Random.Range(GameManager.Instance.AreaBoundary().yMin, GameManager.Instance.AreaBoundary().yMax)
        };

        return newPoint;
    }

    public void RemoveFish(Fish fish)
    {
        int index = m_FishList.IndexOf(fish);

        fish.BeginDeath();

        m_FishIndexes.Push(index);
    }
}
