using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Shark : MonoBehaviour
{
    [SerializeField] Emotion m_Emotion;

    [SerializeField] float m_CurrentSize;               //Current size of the Shark
    [SerializeField] private float m_MinSize = 0.5f;
    [SerializeField] private float m_MaxSize = 3;

    [SerializeField] private float m_SizeIncrement;     //The 

    [SerializeField] private float m_DisappearTime;     //The time for the Emotion Sprite and Text to disappear
     
    [Header("To display emotions")]
    [SerializeField] TextMeshPro m_EmotionText;

    [SerializeField] SpriteRenderer m_EmotionSprite;

    private Quaternion m_Rotation;                             //Gets the rotation at start and keeps the text and sprite facing the screen

    #region Unity Functions
    private void Awake()
    {
        m_CurrentSize = 1;

        GetAndSetEmotion();

        GameManager.Instance.OnScorePoint += AteTheRightFish;
        GameManager.Instance.OnErrorPoint += AteTheWrongFish;
        GameManager.Instance.OnGameReset += Instance_OnGameReset;

        m_Rotation = transform.rotation;
    }
    private void LateUpdate()
    {
        //Because the sprite is currently a child of the text, rotate the text gameobject
        m_EmotionText.transform.rotation = m_Rotation;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Fish"))
        {
            EatFish(collision.GetComponent<Fish>());
        }
    }
    #endregion

    public Emotion GetEmotion()
    {
        return m_Emotion;
    }

    private void GetAndSetEmotion()
    {
        m_Emotion = EmotionManager.Instance.GetRandomEmotion();

        if (m_Emotion)
        {
            m_EmotionText.text = m_Emotion.name;
            m_EmotionSprite.sprite = m_Emotion.sprite;
            StartCoroutine(DelayFadeOut());
            FishManager.Instance.m_TargetEmotion = m_Emotion;
        }
    }

    private void Instance_OnGameReset()
    {
        m_CurrentSize = 1;
        transform.DOScale(m_CurrentSize, 0);
        FadeEmotion(1, 0);
        GetAndSetEmotion();
        transform.position = Vector3.zero;
    }

    private void EatFish(Fish fish)
    {
        if (EmotionManager.Instance.CompareEmotion(m_Emotion, fish.GetEmotion()))
            GameManager.Instance.ScorePoint();
        else
            GameManager.Instance.ErrorPoint();

        FishManager.Instance.RemoveFish(fish);
    }

    IEnumerator DelayFadeOut()
    {
        yield return new WaitForSeconds(m_DisappearTime);
        FadeEmotion(0, 1);
    }

    void FadeEmotion(float value, float time)
    {
        m_EmotionText.DOFade(value, time);
        m_EmotionSprite.DOFade(value, time);
    }

    void AteTheRightFish()
    {
        if (m_CurrentSize < m_MaxSize)
            m_CurrentSize += m_SizeIncrement;

        transform.DOScale(m_CurrentSize, 0.5f);
    }

    void AteTheWrongFish()
    {
        if (m_CurrentSize > m_MinSize)
            m_CurrentSize -= m_SizeIncrement;

        transform.DOScale(m_CurrentSize, 0.5f);
    }
}
