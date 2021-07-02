using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float speedLimit;
    public float rotationSpeed;
    public float jumpForce;
    public float playerSize = 1;
    public float playerHeight = 0.5f;

    [SerializeField]
    public Timer jumpCD;

    public Transform playerCamera;
    private Rigidbody playerRB;
    public LayerMask ground;

    float rotationY = 0;

    private void Awake()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveBody();
        RotateCamera();
        Jump();
    }

    public void Move(Vector3 newPostion, Quaternion newRotation)
    {
        transform.position = newPostion;
        playerCamera.rotation = newRotation;
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed / 100;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed / 100;

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
    }

    private void MoveBody()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = transform.TransformDirection(input) * movementSpeed;


        MoveBody(direction);
        AddFriction(direction);
        ClampRamp();
        ClampVelocity();
    }

    private void MoveBody(Vector3 direction)
    {
        playerRB.AddForce(direction * Time.deltaTime * 100);
    }

    private void AddFriction(Vector3 moveDirection)
    {
        if (moveDirection.magnitude == 0 && IsGrounded())
        {
            var currentVelocity = playerRB.velocity;
            var half = 0.5f;
            var halfedVelocityXZ = new Vector3(currentVelocity.x * half, currentVelocity.y, currentVelocity.z * half);
            playerRB.velocity = halfedVelocityXZ;
        }
    }

    private void ClampRamp()
    {
        if (IsGrounded(out var noramal) && Vector3.Dot(Vector3.up, noramal) != 1)
        {
            var currentVelocity = playerRB.velocity;
            currentVelocity.y = Mathf.Clamp(currentVelocity.y, currentVelocity.y, 0);
            playerRB.velocity = currentVelocity;
        }
    }

    private void ClampVelocity()
    {
        Vector3 current = playerRB.velocity;
        Vector2 clampedVelocity = Vector2.ClampMagnitude(new Vector2(current.x, current.z), speedLimit);
        playerRB.velocity = new Vector3(clampedVelocity.x, current.y, clampedVelocity.y);
    }

    private void Jump()
    {
        jumpCD.UpdateTimer(Time.deltaTime);
        if (Input.GetKey(KeyCode.Space) && IsGrounded() && jumpCD.isReady)
        {
            playerRB.AddForce(transform.up * jumpForce);
            jumpCD.Reset();
        }
    }

    private bool IsGrounded()
    {
        return IsGrounded(out _);
    }

    private bool IsGrounded(out Vector3 surfaceNormal)
    {
        var result = Physics.SphereCast(transform.position, playerSize, -transform.up, out var hit, playerHeight, ground);
        surfaceNormal = hit.normal;
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position - transform.up * playerHeight, playerSize);

        var result = Physics.SphereCast(transform.position, playerSize, -transform.up, out var hit, playerHeight, ground);
        if (result && Vector3.Dot(Vector3.up, hit.normal) != 1)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(hit.point, hit.point + hit.normal);
        }
    }
}
