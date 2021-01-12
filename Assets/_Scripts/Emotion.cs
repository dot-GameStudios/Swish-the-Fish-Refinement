using System.Collections.Generic;
using UnityEngine;

public enum Emotions
{
    Happy,
    Sad,
    Love,
    Hate,
    Confident,
    Fear,
    Silly,
    Serious
}

[CreateAssetMenu(fileName = "New Emotion", menuName = "Emotions")]
public class Emotion : ScriptableObject
{
    public Emotions mainEmotion;
    public Sprite sprite;

    public List<Emotions> oppositeEmotions;

}
