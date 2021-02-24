using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;

    public static GameObject player;
    public static GameObject currentPlatform;

    private bool _canTurn;

    private sbyte _jumpForce = 5;
    private sbyte _turnAngle = 90;

    private Vector3 _startPosition;

    private static readonly int Jumping = Animator.StringToHash("isJumping");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        // Starting position of player.
        player = gameObject;
        _startPosition = player.transform.position;

        // creates the first platform attached to the start point.
        GenerateWorld.RunDummy();
    }

    private void Update()
    {
        UserMovement();
    }

    /// <summary>
    /// Handles the players movement. and jump mechanism.
    /// </summary>
    private void UserMovement()
    {
        if (Input.GetKeyDown(key: KeyCode.Space))
        {
            IsJumping(true);
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(key: KeyCode.RightArrow) && _canTurn)
        {
            RotatePlayer(Vector3.up);
            RelocateDummyPosition();
            GenerateWorld.RunDummy();
            AddAnotherPlatform();
            StabilizePlayer();
        }
        else if (Input.GetKeyDown(key: KeyCode.LeftArrow) && _canTurn)
        {
            RotatePlayer(Vector3.down);
            RelocateDummyPosition();
            GenerateWorld.RunDummy();
            AddAnotherPlatform();
            StabilizePlayer();
        }

        else if (Input.GetKeyDown(key: KeyCode.A))
        {
            transform.Translate(Vector3.left);
        }

        else if (Input.GetKeyDown(key: KeyCode.D))
        {
            transform.Translate(Vector3.right);
        }
        else
        {
            IsJumping(false);
        }
    }

    /// <summary>
    /// Takes in a boolean that sets the animation to its respective state.
    /// </summary>
    /// <param name="isSpacePressed"></param>
    private void IsJumping(bool isSpacePressed)
    {
        _animator.SetBool(Jumping, isSpacePressed);
    }

    /// <summary>
    /// Prevents player from drifting on the x or z axis.
    /// </summary>
    private void StabilizePlayer()
    {
        transform.position = new Vector3(_startPosition.x, transform.position.y, _startPosition.z);
    }
    
    /// <summary>
    /// Adds another platform ahead of the player so that it can't see the platforms being spawned.
    /// </summary>
    private void AddAnotherPlatform()
    {
        if (!GenerateWorld.lastPlatform.CompareTag("platformTSection"))
        {
            GenerateWorld.RunDummy();
        }
    }

    /// <summary>
    /// Relocates the Dummies position accordingly when the players changes.
    /// </summary>
    private void RelocateDummyPosition()
    {
        GenerateWorld.dummy.transform.forward = -transform.forward;
    }

    /// <summary>
    /// Rotates the player in the desired direction with a given angle.
    /// </summary>
    /// <param name="direction"></param>
    private void RotatePlayer(Vector3 direction)
    {
        transform.Rotate(direction * _turnAngle);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        // Records every platform we stand on.
        currentPlatform = other.gameObject;
    }

    /// <summary>
    /// When the collider is touched, generate another platform.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && !GenerateWorld.lastPlatform.CompareTag("platformTSection"))
        {
            GenerateWorld.RunDummy();
        }

        // The only sphere collider on the T-section to only enable turning there.
        if (other is SphereCollider)
        {
            _canTurn = true;
        }
    }

    /// <summary>
    /// Prevents the user from turning when leaving the T-section. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider)
        {
            _canTurn = false;
        }
    }
}