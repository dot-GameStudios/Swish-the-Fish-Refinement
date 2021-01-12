using UnityEngine;
using DG.Tweening;
using System.Diagnostics;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private const float m_RotateDuration = 0.3f;
    [SerializeField] private Vector3 m_MouseInput;
    [SerializeField] private Vector3 m_MouseDelta;
    [SerializeField] private bool m_CanMove;
    [SerializeField] private bool m_Moveable;    

    private void Awake()
    {
        GameManager.Instance.OnGameOver += Instance_OnGameOver;
        GameManager.Instance.OnGameStart += Instance_OnGameStart;
        GameManager.Instance.OnGameReset += Instance_OnGameStart;
        Input.simulateMouseWithTouches = true;
    }

    private void Instance_OnGameStart()
    {
        m_CanMove = true;
    }

    private void Instance_OnGameOver()
    {
        m_CanMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_CanMove)
            return;

        UpdateMouseInputAndDelta();

        RaycastToWorldSpace();

        if (Input.GetMouseButton(0) && m_Moveable) //When mouse down and is allowed to move object
        {
            MoveAndRotate();
        }
    }

    private void MoveAndRotate()
    {
        //Move object to new position
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(m_MouseInput); 
        newPosition.z = 0;
        transform.position = newPosition;
        
        //Based off the mouse delta, rotate the object to the correct direction
        if (m_MouseDelta.x > 0.1f)
            transform.DORotate(new Vector3(0, 90, 0), m_RotateDuration);
        else if (m_MouseDelta.x < -0.1f)
            transform.DORotate(new Vector3(0, -90, 0), m_RotateDuration);
    }

    private void UpdateMouseInputAndDelta()
    {
        m_MouseDelta = m_MouseInput;
        m_MouseInput = Input.mousePosition;
        m_MouseDelta = m_MouseInput - m_MouseDelta;
    }

    void RaycastToWorldSpace()
    {
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(m_MouseInput);

        if (Physics.Raycast(screenToWorld, Vector3.forward, out RaycastHit hit, 50))
        {
            if (hit.transform.CompareTag("Player"))
                m_Moveable = true;
        }
        else
            m_Moveable = false;
    }
}
