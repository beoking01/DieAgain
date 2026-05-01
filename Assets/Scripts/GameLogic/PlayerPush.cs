using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float pushForce = 5f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        // Không có rigidbody → bỏ qua
        if (rb == null || rb.isKinematic)
            return;

        // Không đẩy xuống dưới
        if (hit.moveDirection.y < -0.3f)
            return;

        // Tạo lực đẩy ngang
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        rb.linearVelocity = pushDir * pushForce;
    }
}