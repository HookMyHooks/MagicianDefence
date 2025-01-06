using Assets.Scripts.Utils;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _MovementSpeed;
    [SerializeField] private float _RotationSpeed;
    [SerializeField] private float _JumpSpeed;
    [SerializeField] private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        bool isMoving = false; // Tracks whether the player is moving

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * _MovementSpeed * Time.deltaTime;
            _animator.SetInteger("AnimState", 1);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.forward * _MovementSpeed * Time.deltaTime * 0.5f;
            _animator.SetInteger("AnimState", 2); // Adjust state for backward movement if needed
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.eulerAngles += Vector3.down * _RotationSpeed * Time.deltaTime;
            transform.Translate(Vector3.left * _MovementSpeed * Time.deltaTime);
            _animator.SetInteger("AnimState", 3);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * _MovementSpeed * Time.deltaTime);
            _animator.SetInteger("AnimState", 4);
            isMoving = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(new Vector3(0, _JumpSpeed), ForceMode.Impulse);
        }

        // If no movement keys are pressed, set the animation state to idle
        if (!isMoving)
        {
            _animator.SetInteger("AnimState", 0);
        }
    }
}
