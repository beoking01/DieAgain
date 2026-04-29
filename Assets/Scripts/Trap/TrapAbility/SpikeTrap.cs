using UnityEngine;

public class SpikeTrap : TrapBase
{
    public Vector3 moveOffset = new Vector3(0, 1.5f, 0);
    public float speed = 8f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool moving = false;

    private void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveOffset;
    }

    private void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );
        }
    }

    protected override void Activate()
    {
        moving = true;
    }
}