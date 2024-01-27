using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    private Camera _camera;
    public Camera Camera => _camera;
    private Transform _target;
    private Vector2 _velocity = Vector2.zero;
    [SerializeField] private float smoothTime = 0.3f;
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public void SetTarget(Transform targetTransform)
    {
        _target = targetTransform;
    }

    private void LateUpdate()
    {
        if (_target == null) return;
        
        var newPos = Vector2.SmoothDamp(transform.position, _target.position, ref _velocity, smoothTime);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
