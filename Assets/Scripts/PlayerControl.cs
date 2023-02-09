using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float paddingHorizontal;
    [SerializeField] float paddingVertical;

    Shooter shooter;
    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        InitBounds();
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, 
            minBounds.x + paddingHorizontal, maxBounds.x - paddingHorizontal);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y,
            minBounds.y + paddingVertical, maxBounds.y - paddingVertical);
        transform.position = newPos;
    }
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        
    }
    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
