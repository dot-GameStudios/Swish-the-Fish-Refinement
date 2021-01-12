using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform m_Target;                //The target the camera has to follow

    [SerializeField] float m_CameraSpeed = 1;          
    [SerializeField] float m_CameraZPosition = -10;     //The camera z position offset
    [SerializeField] float m_PlayerClampFactor;         //Use this to determine the distance from the areaBoundaries you want to clamp the player in
    // Start is called before the first frame update
    void Start()
    {
        m_CameraZPosition = transform.position.z;
    }

    private void LateUpdate()
    {
        if (!m_Target)
            return;

        FollowTarget();
        ClampTargetPosition(m_Target);
    }

    void FollowTarget()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, m_Target.position, m_CameraSpeed * Time.deltaTime);

        newPosition.x = Mathf.Clamp(newPosition.x, GameManager.Instance.AreaBoundary().xMin / 2, GameManager.Instance.AreaBoundary().xMax / 2);
        newPosition.y = Mathf.Clamp(newPosition.y, GameManager.Instance.AreaBoundary().yMin / 2, GameManager.Instance.AreaBoundary().yMax / 2);
        newPosition.z = m_CameraZPosition;

        transform.position = newPosition;
    }

    //Clamps the targets position in the *Area Boundaries;
    void ClampTargetPosition(Transform target)
    {
        Vector3 clampPosition = Camera.main.WorldToViewportPoint(transform.position);

        clampPosition.x = Mathf.Clamp(target.position.x, GameManager.Instance.AreaBoundary().xMin * m_PlayerClampFactor, GameManager.Instance.AreaBoundary().xMax * m_PlayerClampFactor);
        clampPosition.y = Mathf.Clamp(target.position.y, GameManager.Instance.AreaBoundary().yMin * m_PlayerClampFactor, GameManager.Instance.AreaBoundary().yMax * m_PlayerClampFactor);

        target.position = clampPosition;
    }
}
