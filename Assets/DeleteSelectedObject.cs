using UnityEngine;
using UnityEngine.InputSystem;

public class DeleteSelectedObject : MonoBehaviour
{
    private GameObject selectedObject;
    private InputAction deleteAction;

    private void OnEnable()
    {
        var playerInput = new InputActionMap("Player");
        deleteAction = playerInput.AddAction("DeleteObject", binding: "<Keyboard>/r");

        deleteAction.performed += OnDeletePerformed;
        deleteAction.Enable();
    }

    private void OnDisable()
    {
        deleteAction.performed -= OnDeletePerformed;
        deleteAction.Disable();
    }

    private void Update()
    {
        // Chọn GameObject bằng chuột
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                selectedObject = hit.collider.gameObject;
                Debug.Log(selectedObject.tag);
            }
        }
    }

    private void OnDeletePerformed(InputAction.CallbackContext context)
    {
        if (selectedObject != null)
        {
            Destroy(selectedObject);
            selectedObject = null;
        }
    }
}
