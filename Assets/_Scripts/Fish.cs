using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Fish : MonoBehaviour
{
    [SerializeField] Emotion m_Emotion;

    [SerializeField] SpriteRenderer m_SpriteRenderer;

    [SerializeField] TextMeshPro m_EmotionText;

    [SerializeField] Vector3 m_Direction;

    [SerializeField] Vector3 m_CurrentWaypoint;

    [SerializeField] float m_Speed = 2;

    [SerializeField] float m_ThreatDistance = 6;

    [SerializeField] Transform m_Threat;

    [SerializeField] Collider m_Collider;

    [SerializeField] MeshRenderer m_Mesh;

    [SerializeField] private bool m_Dead;

    private Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        m_Threat = GameObject.FindGameObjectWithTag("Player").transform;
        rot = transform.rotation;
        m_Collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (m_Dead)
            return;

        if (Vector3.Distance(transform.position, m_CurrentWaypoint) <= 2)
            m_CurrentWaypoint = FishManager.Instance.RandomWaypoint();

        if (Vector3.Distance(transform.position, m_Threat.position) <= m_ThreatDistance)
        {
            m_Direction = (transform.position - m_Threat.position).normalized;
            transform.DOMove(transform.position + m_Direction * 2, 1);
        }
        else
            m_Direction = (m_CurrentWaypoint - transform.position).normalized;

        transform.position += m_Direction * Time.fixedDeltaTime * m_Speed;

        if(m_Direction.x > 0.1f)
            transform.DORotate(new Vector3(0, 90, 0), 0.3f);
        else
            transform.DORotate(new Vector3(0, -90, 0), 0.3f);
    }

    private void LateUpdate()
    {
        m_SpriteRenderer.transform.rotation = rot;
    }

    public void InitializeFish(Emotion emotion, Vector3 wayPoint, float speed)
    {
        m_Emotion = emotion;
        m_SpriteRenderer.sprite = emotion.sprite;
        m_EmotionText.text = emotion.name;
        m_EmotionText.DOFade(0, 0);
        m_CurrentWaypoint = wayPoint;
        m_Speed = speed;
        m_Collider.enabled = true;
        m_Mesh.enabled = true;
        m_SpriteRenderer.enabled = true;
    }    

    public Emotion GetEmotion()
    {
        return m_Emotion;
    }   

    IEnumerator DelayDeath()
    {
        m_EmotionText.DOFade(1, 0.8f);
        yield return new WaitForSeconds(1.5f);
        m_EmotionText.DOFade(0, 0).OnComplete(() => { Destroy(this.gameObject); });
    }

    internal void BeginDeath()
    {
        m_Collider.enabled = false;
        m_Mesh.enabled = false;
        m_SpriteRenderer.enabled = false;
        m_Dead = true;
        StartCoroutine(DelayDeath());
    }
}
