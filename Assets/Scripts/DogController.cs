using UnityEngine;

public class DogController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Joystick joystick; // Reference to your Joystick script
    public Animator animator; // Reference to the Animator

    private void Update()
    {
        // Get input from the joystick
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        // Set direction for the dog to move
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Set the speed parameter in the Animator based on the magnitude of the direction vector
        animator.SetFloat("Speed", direction.magnitude * moveSpeed);

        // Move the dog in the direction based on joystick input
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // Rotate the dog to face the movement direction if it's moving
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
