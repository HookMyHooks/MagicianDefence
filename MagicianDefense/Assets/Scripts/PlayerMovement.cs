using Assets.Scripts.Utils;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _MovementSpeed = 5f;
    [SerializeField] private float _RotationSpeed = 200f;
    [SerializeField] private float _JumpSpeed = 5f;
    [SerializeField] private Animator _animator;

    private bool _isGrounded; // Verifică dacă personajul este pe sol

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        // Blochează rotațiile nedorite
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY |
                            RigidbodyConstraints.FreezeRotationX |
                            RigidbodyConstraints.FreezeRotationY |
                            RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        //HandleRotation();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        bool isMoving = false;
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += transform.forward;
            _animator.SetInteger("AnimState", 1);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection -= transform.forward * 0.5f;
            _animator.SetInteger("AnimState", 2);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection -= transform.right;
            _animator.SetInteger("AnimState", 3);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += transform.right;
            _animator.SetInteger("AnimState", 4);
            isMoving = true;
        }

        // Aplică forță pentru mișcare
        if (isMoving)
        {
            _rigidbody.AddForce(moveDirection.normalized * _MovementSpeed, ForceMode.VelocityChange);
        }
        else
        {
            // Frânare treptată dacă nu se mișcă
            _rigidbody.linearVelocity = new Vector3(0, _rigidbody.linearVelocity.y, 0);
            _animator.SetInteger("AnimState", 0);
        }
    }

    private void HandleRotation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontal * _RotationSpeed * Time.deltaTime, 0);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _JumpSpeed, ForceMode.Impulse);
            _isGrounded = false;
        }
    }


}
