using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class LocalVRMovement : MonoBehaviour
{
    public float MovementSpeed;

    public float RotationSpeed;

   [SerializeField]
    private Transform _headTransform;

    [SerializeField]
    private InputActionReference _moveInput;

    [SerializeField]
    private InputActionReference _rotateInput;

    private CharacterController _characterController;

    private float _gravity =0;
    private void OnEnable()
    {
        _moveInput.action.Enable();
        _rotateInput.action.Enable();
    }

    private void OnDisable()
    {
        _moveInput.action.Disable();
        _rotateInput.action.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
       _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        Gravity();
    }

    private void Move()
    {
        Vector2 movement = _moveInput.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(movement.x, 0 , movement.y);
        direction = _headTransform.TransformDirection(direction);
        direction = Vector3.Scale(direction, new Vector3(1, 0, 1)).normalized;
        _characterController.Move(direction*MovementSpeed*Time.deltaTime);
    }

    private void Rotate()
    {
        Vector2 rotation = _rotateInput.action.ReadValue<Vector2>();
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + rotation.x, 0)* RotationSpeed);
       
    }

    private void Gravity()
    {
        if (_characterController.isGrounded)
        {
            _gravity = 0;
        }
        else
        {
            _gravity -= 9.81f * Time.deltaTime;
            _characterController.Move(new Vector3(0, _gravity, 0));
        }
             
    }
}
