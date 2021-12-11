using UnityEngine;

public class Dragger : MonoBehaviour
{
    //Dragger
    private Vector3 _dragOffset;
    private Camera _camera;
    //Rotator
    [SerializeField] private float _rotateSpeed = 100f;
    private float _horInput;
    private float _rotation = 0f;

    void Awake()
    {
        _camera  = Camera.main;
    }

    void OnMouseDown()
    {
        _dragOffset = transform.position - GetMousePos();
    }
    void OnMouseDrag()
    {
        transform.position = GetMousePos() + _dragOffset;

        _horInput = Input.GetAxis("Horizontal");
        if(_horInput != 0f)
        {
            _rotation += _horInput * _rotateSpeed;
            _rotation *= Time.deltaTime;
            transform.Rotate(0,0,_rotation);
        }
    }

    Vector3 GetMousePos()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
