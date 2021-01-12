using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform m_Target;                //The target the camera has to follow

    [SerializeField] float m_CameraSpeed = 1;          
    [SerializeField] float m_CameraZPosition = -10;     //The camera z position offset

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

    //Clamps the targets position in the screen;
    void ClampTargetPosition(Transform target)
    {
        Vector3 clampPosition = Camera.main.WorldToViewportPoint(transform.position);

        clampPosition.x = Mathf.Clamp01(clampPosition.x);
        clampPosition.y = Mathf.Clamp01(clampPosition.y);

        target.position = Camera.main.ViewportToWorldPoint(clampPosition);
    }
}
