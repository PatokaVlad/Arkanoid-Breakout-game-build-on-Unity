using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionLineHandler : MonoBehaviour
{
    [SerializeField]
    private Camera _camera = null;

    private LineRenderer _lineRenderer;

    private Vector2 pressingPosition;

    private bool isPressed = false;
    private bool isActive = false;

    public bool IsPressed { get => isPressed; }

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.positionCount = 2;
    }

    public void StartLinePositionSet(Vector2 startPosition)
    {
        if(!isActive)
        {
            isActive = true;
            gameObject.SetActive(isActive);
        }

        pressingPosition = Vector2.up * -startPosition.y * 2;
        _lineRenderer.SetPosition(0, startPosition);
    }

    public void FollowMouse()
    {
        pressingPosition.x = _camera.ScreenToWorldPoint(Input.mousePosition).x;
        _lineRenderer.SetPosition(1, pressingPosition);

        if (Input.GetKeyDown(KeyCode.W))
        {
            isPressed = true;
        }
    }

    public float GetDirectionVector()
    {
        isPressed = false;
        isActive = false;
        gameObject.SetActive(isActive);

        return Vector2.SignedAngle(Vector2.up, pressingPosition);
    }
}
