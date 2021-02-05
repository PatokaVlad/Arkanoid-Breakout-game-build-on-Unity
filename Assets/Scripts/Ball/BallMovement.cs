using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    private Camera _camera = null;
    [SerializeField]
    private Player _player = null;
    [SerializeField]
    private PlaneMovement _plane = null;
    [SerializeField]
    private DirectionLineHandler _line = null;

    private Transform _transform;
    private AudioHandler _audioHandler;

    [SerializeField]
    private BallState _bounceState = null;
    [SerializeField]
    private BallState _planeState = null;

    private BallState _currentState = null;

    [SerializeField]
    private float speed = 4;
    [SerializeField]
    private float bounceMultiplier = 15f;
    [SerializeField]
    private float angleNormalizeMultiplier = 25;

    private float xEdge = 7.5f;
    private float yEdge = 4f;

    private Vector2 direction = Vector2.zero;
    private Vector3 startPosition = Vector3.zero;

    public bool IsBounce { get; set; }
    public Vector3 StartPosition { get => startPosition; }

    private void OnEnable()
    {
        _player.onLivesDecrease += ToStartPosition;
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _audioHandler = FindObjectOfType<AudioHandler>();

        yEdge = _camera.orthographicSize;
        xEdge = _camera.aspect * yEdge;

        startPosition = _transform.position;

        IsBounce = false;

        SetState(_planeState);
    }

    private void Update()
    {
        if(!_player.StopGame)
        {
            if (!_currentState.IsFinished)
            {
                _currentState.Move();
            }
            else
            {
                if (IsBounce)
                {
                    SetState(_bounceState);
                }
                else
                {
                    SetState(_planeState);
                }
            }
        }
    }

    private void OnDisable()
    {
        _player.onLivesDecrease -= ToStartPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateDirection(collision.contacts[0].normal);
    }

    private void SetState(BallState ballState)
    {
        if(_currentState!=null)
            Destroy(_currentState);
        _currentState = Instantiate(ballState);
        _currentState.Ball = this;
    }

    #region Bounce
    public void Bounce()
    {
        Vector3 delta = direction * Time.deltaTime * speed;
        Vector3 newPosition = _transform.position + delta;

        if (newPosition.x < -xEdge)
        {
            newPosition.x = -xEdge;
            UpdateDirection(Vector2.right);
        }
        else if (newPosition.x > xEdge)
        {
            newPosition.x = xEdge;
            UpdateDirection(Vector2.left);
        }
        else if (newPosition.y > yEdge)
        {
            newPosition.y = yEdge;
            UpdateDirection(Vector2.down);
        }
        else if (newPosition.y < -yEdge)
        {
            newPosition.y = -yEdge;

            _audioHandler.PlayDieClip();
            _player.LivesDecrease();
            _currentState.IsFinished = true;
            IsBounce = false;
        }

        _transform.position = newPosition;
    }

    private void UpdateDirection(Vector2 normal)
    {
        _audioHandler.PlayBounceClip();

        direction = Vector2.Reflect(direction, normal);
        direction = AdjustDirection(direction, normal, bounceMultiplier);
    }

    private Vector2 AdjustDirection(Vector2 direction, Vector2 normal, float angleRange)
    {
        float angle = Vector2.SignedAngle(normal, direction);
        float normalAngle = 90 - angleNormalizeMultiplier;

        if (Mathf.Abs(angle) < normalAngle)
        {
            if(angle < 0)
            {
                direction = RotateVector(direction, -Random.Range(angleRange / 2, angleRange));
            }
            else
            {
                direction = RotateVector(direction, Random.Range(angleRange / 2, angleRange));
            }
        }

        return direction;
    }

    private Vector2 RotateVector(Vector2 vector, float degrees)
    {
        float radians = Mathf.Deg2Rad * degrees;

        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        return new Vector2(vector.x * cos - vector.y * sin, vector.x * sin + vector.y * cos);
    }
    #endregion

    private void ToStartPosition()
    {
        _transform.position = startPosition;
    }

    public void StickToPlane()
    {
        Vector3 offSet = startPosition - _plane.StartPosition;

        _transform.position = _plane.GetPosition() + offSet;

        _line.StartLinePositionSet(_transform.position);
        _line.FollowMouse();

        if(_line.IsPressed)
        {
            direction = RotateVector(Vector2.up, _line.GetDirectionVector());

            _currentState.IsFinished = true;
            IsBounce = true;
        }
    }
}
