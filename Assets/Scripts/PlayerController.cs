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

    private Vector3 _startPosition;

    private static readonly int Jumping = Animator.StringToHash("isJumping");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
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
            transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummy.transform.forward = -transform.forward;
            GenerateWorld.RunDummy();
        }
        else if (Input.GetKeyDown(key: KeyCode.LeftArrow) && _canTurn)
        {
            transform.Rotate(Vector3.down * 90);
            GenerateWorld.dummy.transform.forward = -transform.forward;
            GenerateWorld.RunDummy();
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

    private void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider)
        {
            _canTurn = false;
        }
    }
}