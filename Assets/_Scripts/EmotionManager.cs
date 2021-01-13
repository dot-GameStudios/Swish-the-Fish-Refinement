using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    public static EmotionManager Instance;

    [SerializeField] private List<Emotion> m_EmotionList;
    [SerializeField] private List<Emotion> m_ActiveEmotions;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    internal Emotion GetRandomEmotion()
    {
        if (m_ActiveEmotions == null)
            return null;

        return m_ActiveEmotions[Random.Range(0, m_ActiveEmotions.Count)];
    }

    internal Emotion GetOppositeEmotion(Emotion mainEmotion) //the emotion you are looking to find the opposite of
    {
        for(int i =0; i < m_ActiveEmotions.Count; i++) //loop through all activeEmotions in game
        {
            for(int j = 0; j < mainEmotion.oppositeEmotions.Count; j++) 
            {
                if(mainEmotion.oppositeEmotions[j] == m_ActiveEmotions[i].mainEmotion)
                {
                    return m_ActiveEmotions[i];
                }
            }
        }
        return null;
    }

    internal bool CompareEmotion(Emotion mainEmotion, Emotion oppositeEmotion)
    {
        foreach(Emotions emo in mainEmotion.oppositeEmotions)
        {
            if (emo == oppositeEmotion.mainEmotion)
                return true;
        }

        return false;
    }

}
