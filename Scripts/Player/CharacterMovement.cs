using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float sprintEnergyCost = 0.2f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _gravity = 20f;
    [SerializeField] private Animator _handAnimator;
    [Range(0, 10), SerializeField] private float _airControl = 5;

    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _controller;
    private PlayerStats _playerStats;
    private bool _isReadyToSprint = true;
    private bool _isReadyToJump = true;
    private float _moveSpeedMax = 5f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _playerStats = GetComponent<PlayerStats>();
    }

    private void FixedUpdate()
    {
        Movement();
        Sprint();
        CheckPossibilityJumpAndSprint();
    }

    private void Sprint()
    {
        if (_isReadyToSprint == false)
            _moveSpeed = _moveSpeedMax;

        if (Input.GetKey(KeyCode.LeftShift) && _isReadyToSprint && Vector3.ClampMagnitude(_moveDirection, 1).magnitude == 1)
        {
            _moveSpeed = sprintSpeed;
            _playerStats.PlayerWasteEnergy(sprintEnergyCost);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _moveSpeed = _moveSpeedMax;
        }
    }

    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        _handAnimator.SetFloat("speed", Vector3.ClampMagnitude(input, 1).magnitude);
        input *= _moveSpeed;
        input = transform.TransformDirection(input);

        if (_controller.isGrounded)
        {
            _moveDirection = input;

            if (Input.GetButton("Jump") && _isReadyToJump)
            {
                _moveDirection.y = Mathf.Sqrt(2 * _gravity * _jumpForce);
                _playerStats.PlayerWasteEnergy(20);
            }
            else
            {
                _moveDirection.y = 0;
            }
        }
        else
        {
            input.y = _moveDirection.y;
            _moveDirection = Vector3.Lerp(_moveDirection, input, _airControl * Time.deltaTime);
        }
        _moveDirection.y -= _gravity * Time.deltaTime;
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    private void CheckPossibilityJumpAndSprint()
    {
        if (_playerStats.GetPlayerEnergy() <= 0)
            _isReadyToSprint = false;
        else
            _isReadyToSprint = true;

        if (_playerStats.GetPlayerEnergy() <= 19)
            _isReadyToJump = false;
        else
            _isReadyToJump = true;
    }
}