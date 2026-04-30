using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float rotationSpeed = 12f;

    [Header("Jump & Gravity")]
    public float jumpHeight = 1.8f;
    public float gravity = -9.81f;
    public bool isGround;

    [Header("VFX")]
    [SerializeField] private ParticleSystem runDust;
    [SerializeField] private Transform dustSpawnPoint;

    [Header("Die")]
    [SerializeField] private float dieDelay = 1.5f;

    private CharacterController controller;
    private Vector3 verticalVelocity;
    private Animator animator;

    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isDead) return;

        if (GameManager.Instance != null && !GameManager.Instance.isGameActive) return;
        if (GameManager.Instance != null && GameManager.Instance.isPaused) return;

        isGround = controller.isGrounded;

        Vector3 moveDirection = CalculateMovement();

        CalculateGravityAndJump();

        Vector3 finalMovement = moveDirection * moveSpeed + verticalVelocity;
        controller.Move(finalMovement * Time.deltaTime);

        RotatePlayer(moveDirection);

        HandleAnimation(moveDirection);

        HandleRunDust(moveDirection);
    }

    private Vector3 CalculateMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(horizontal, 0f, vertical);

        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

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
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(SoundType.Jump);
            }

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
        Vector3 horizontalVelocity = new Vector3(
            controller.velocity.x,
            0f,
            controller.velocity.z
        );

        bool isMoving = horizontalVelocity.magnitude > 0.2f;
        bool isJumping = !isGround || verticalVelocity.y > 0.2f;

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isJumping", isJumping);
    }

    private void HandleRunDust(Vector3 move)
    {
        if (runDust == null || dustSpawnPoint == null) return;

        bool isRunning = move.sqrMagnitude > 0.01f && isGround;

        if (isRunning)
        {
            runDust.transform.position = dustSpawnPoint.position;
            runDust.transform.rotation = Quaternion.LookRotation(-move.normalized);

            if (!runDust.isPlaying)
            {
                runDust.Play();
            }
        }
        else
        {
            if (runDust.isPlaying)
            {
                runDust.Stop();
            }
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("Player Die được gọi");

        if (runDust != null && runDust.isPlaying)
        {
            runDust.Stop();
        }

        if (animator == null)
        {
            Debug.LogError("Không tìm thấy Animator trên Player hoặc object con!");
            return;
        }

        animator.SetBool("isMoving", false);
        animator.SetBool("isJumping", false);

        // Gọi thẳng animation Die1, không cần transition
        animator.Play("Die1", 0, 0f);

        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(dieDelay);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}