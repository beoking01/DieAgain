using UnityEngine;

public class MovingWallTrap : TrapBase
{
    public Vector3 moveDirection = Vector3.forward;
    public float speed = 6f;
    public float moveDistance = 5f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool moving = false;
    private float moveProgress = 0f;

    private void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveDirection.normalized * moveDistance;
    }

    private void Update()
    {
        if (moving)
        {
            if (moveDistance <= 0f)
            {
                transform.position = targetPos;
                return;
            }

            moveProgress = Mathf.MoveTowards(moveProgress, 1f, speed * Time.deltaTime / moveDistance);
            transform.position = Vector3.Lerp(startPos, targetPos, moveProgress);
        }
    }

    protected override void Activate()
    {
        moving = true;
    }
}