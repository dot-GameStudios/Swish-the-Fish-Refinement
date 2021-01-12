using System.Collections.Generic;
using UnityEngine;

public enum Emotions
{
    Happy,
    Sad,
    Love,
    Hate,
    Confident,
    Fear
}

[CreateAssetMenu(fileName = "New Emotion", menuName = "Emotions")]
public class Emotion : ScriptableObject
{
    public Emotions mainEmotion;

    public List<Emotions> oppositeEmotions;

    public Sprite sprite;
}
