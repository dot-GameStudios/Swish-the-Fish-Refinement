using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    public static EmotionManager Instance;

    [SerializeField] private List<Emotion> m_EmotionList;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    internal Emotion GetRandomEmotion()
    {
        if (m_EmotionList == null)
            return null;

        return m_EmotionList[Random.Range(0, m_EmotionList.Count)];
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
