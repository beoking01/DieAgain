using UnityEngine;

public class FallingFloorTrap : TrapBase
{
    public float speed = 4f;
    public float moveDistance = 3f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingDown = false;

    private void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.down * moveDistance;
    }

    private void Update()
    {
        if (!movingDown) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );
    }

    protected override void Activate()
    {
        movingDown = true;
    }
}