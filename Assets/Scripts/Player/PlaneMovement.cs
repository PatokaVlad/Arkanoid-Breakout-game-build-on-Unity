using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField]
    private Camera _camera = null;
    [SerializeField]
    private Player _player = null;
    private Transform _transform = null;

    [SerializeField]
    private float speed = 6;
    [SerializeField]
    private float planeLength = 3;
    private float edge = 6.5f;

    private Vector2 startPosition = Vector2.zero;

    private const string horizontal = "Horizontal";

    public Vector3 StartPosition { get => startPosition; }

    private void OnEnable()
    {
        _player.onLivesDecrease += ToStartPosition;
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();

        edge = _camera.aspect * _camera.orthographicSize - planeLength / 2;

        startPosition = _transform.position;
    }

    private void Update()
    {
        if(!_player.StopGame)
            Move();
    }

    private void OnDisable()
    {
        _player.onLivesDecrease -= ToStartPosition;
    }

    private void Move()
    {
        if (!Input.GetAxis(horizontal).Equals(0))
        {
            Vector3 position = _transform.position;

            float delta = Input.GetAxis(horizontal) * Time.deltaTime * speed;

            position.x += delta;

            if (position.x < -edge)
            {
                position.x = -edge;
            }
            else if (position.x > edge)
            {
                position.x = edge;
            }

            _transform.position = position;
        }
    }

    private void ToStartPosition()
    {
        _transform.position = StartPosition;
    }

    public Vector3 GetPosition()
    {
        if (_transform != null)
            return _transform.position;
        else
            return Vector3.zero;
    }
}
