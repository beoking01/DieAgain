using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // Nhân vật
    public Vector3 offset;       // Khoảng cách

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}