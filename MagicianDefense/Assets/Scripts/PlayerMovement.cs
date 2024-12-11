using System;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * _MovementSpeed * Time.deltaTime;
            _animator.SetInteger("AnimState", 1);
            Debug.Log(_animator.GetInteger("AnimState"));

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += -transform.forward * _MovementSpeed * Time.deltaTime;
            //will be another value for a back running animation
            _animator.SetInteger("AnimState", 1);

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles += Vector3.down * _RotationSpeed * Time.deltaTime;
            _animator.SetInteger("AnimState", 2);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles += Vector3.up * _RotationSpeed * Time.deltaTime;
            _animator.SetInteger("AnimState", 3);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(new Vector3(0, _JumpSpeed), ForceMode.Impulse);
        }

        _animator.SetInteger("AnimState", 0);

    }
}
