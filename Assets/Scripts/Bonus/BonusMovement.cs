using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusMovement : MonoBehaviour
{
    protected Transform _transform = null;

    [SerializeField]
    protected float minSpeed = 1f;
    [SerializeField]
    protected float maxSpeed = 5f;

    private float speed;
    private bool isDestroyed = false;

    private const string playerTag = "Player";

    private void Start()
    {
        _transform = GetComponent<Transform>();

        speed = Random.Range(minSpeed, maxSpeed);

        Initialize();
    }

    private void Update()
    {
        MoveDown();
    }

    protected virtual void Initialize() { }

    protected void MoveDown()
    {
        Vector3 delta = Vector3.down * speed * Time.deltaTime;
        _transform.position += delta;
        speed += speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            ActivateBonus();
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if(!isDestroyed)
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    protected abstract void ActivateBonus();
}
