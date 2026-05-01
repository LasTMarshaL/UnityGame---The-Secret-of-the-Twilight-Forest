using UnityEngine;

public class CameraController : MonoBehaviour 
{
    [SerializeField] private Vector3 _cameraSet;

    [SerializeField] private Transform _cameraAim;
    [SerializeField] private Vector3 _cameraAddPosition;

    [SerializeField] private float _cameraFollowSpeed;

    private void Start()
    {
        CameraToPlayer();
    }
    private void Update()
    {
        CameraToPlayer();
    }

    public void CameraToPlayer()
    {
        if (_cameraAim)
        {
            Vector3 currentPosition = _cameraAim.position + _cameraAddPosition + _cameraSet;
            transform.position = Vector3.Lerp(transform.position, currentPosition, _cameraFollowSpeed * Time.unscaledDeltaTime);
        }
    }
}
