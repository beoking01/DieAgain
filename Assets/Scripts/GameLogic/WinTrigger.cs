using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string playerTag = "Player"; // Tag của người chơi

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem vật thể chạm vào có phải người chơi không
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player reached the finish line!");
            
            // Gọi hàm WinLevel từ GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.WinLevel();
            }
            
            // (Tùy chọn) Vô hiệu hóa script này để tránh kích hoạt nhiều lần
            this.enabled = false;
        }
    }
}