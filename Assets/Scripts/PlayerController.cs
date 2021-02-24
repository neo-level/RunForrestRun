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
        else if (Input.GetKeyDown(key: KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * 90);
        }
        else if (Input.GetKeyDown(key: KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down * 90);
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
}