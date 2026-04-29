using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float rotationSpeed = 12f;

    [Header("Jump & Gravity")]
    public float jumpHeight = 1.8f;
    public float gravity = -9.81f; // Giá trị trọng trường chuẩn
    public bool isGround;

    private CharacterController controller;
    private Vector3 verticalVelocity; // Tách riêng vận tốc trọng trường
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGround = controller.isGrounded;
        
        // Tính toán Vector di chuyển ngang
        Vector3 moveDirection = CalculateMovement();
        
        // Tính toán Vector nhảy và trọng lực
        CalculateGravityAndJump();

        // TỔNG HỢP DI CHUYỂN (Chỉ gọi Move 1 lần duy nhất)
        Vector3 finalMovement = moveDirection * moveSpeed + verticalVelocity;
        controller.Move(finalMovement * Time.deltaTime);

        // Xử lý xoay nhân vật
        RotatePlayer(moveDirection);
        // Xử lý animation
        HandleAnimation(moveDirection);
    }

    private Vector3 CalculateMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(horizontal, 0f, vertical);
        if (move.magnitude > 1f) move.Normalize();
        
        return move;
    }

    private void CalculateGravityAndJump()
    {
        if (isGround && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f; 
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity.y += gravity * Time.deltaTime;
    }

    private void RotatePlayer(Vector3 move)
    {
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void HandleAnimation(Vector3 move)
    {
        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        bool isMoving = horizontalVelocity.magnitude > 0.2f;
        bool isJumping = !isGround || verticalVelocity.y > 0.2f;

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isJumping", isJumping);
    }
}