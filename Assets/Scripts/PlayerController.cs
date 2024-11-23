using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMove(InputAction.CallbackContext context)
    {
        moveInput = ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }
}
