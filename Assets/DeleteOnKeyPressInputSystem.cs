using UnityEngine;
using UnityEngine.InputSystem;

public class DeleteOnKeyPressInputSystem : MonoBehaviour
{
    private InputAction deleteAction;

    private void OnEnable()
    {
        // Tải Input Actions từ Input System
        var playerInput = new InputActionMap("Player");
        deleteAction = playerInput.AddAction("DeleteObject", binding: "<Keyboard>/r");

        // Bắt đầu hành động
        deleteAction.performed += OnDeletePerformed;
        deleteAction.Enable();
    }

    private void OnDisable()
    {
        // Hủy đăng ký và vô hiệu hóa hành động
        deleteAction.performed -= OnDeletePerformed;
        deleteAction.Disable();
    }

    private void OnDeletePerformed(InputAction.CallbackContext context)
    {
        // Xóa GameObject này
        Destroy(gameObject);
    }
}
