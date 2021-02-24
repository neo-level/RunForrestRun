using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public static GameObject player;
    public static GameObject currentPlatform;
    public static bool isDead;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private Rigidbody _grenadeRigidbody;
    private Vector3 _startPosition;

    [SerializeField] private SfxManager sfxManager;
    [SerializeField] private GameObject gameMusic;
    [SerializeField] private GameObject grenade;
    [SerializeField] private Transform grenadeStartPosition;
    

    private bool _canTurn;

    private sbyte _jumpForce = 5;
    private sbyte _turnAngle = 90;

    private static readonly int Jumping = Animator.StringToHash("isJumping");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    private static readonly int Grenade = Animator.StringToHash("hasGrenade");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _grenadeRigidbody = grenade.GetComponent<Rigidbody>();

        // Starting position of player.
        player = gameObject;
        _startPosition = player.transform.position;

        // creates the first platform attached to the start point.
        GenerateWorld.RunDummy();
    }

    private void Update()
    {
        if (isDead) return;
        
        UserMovement();
    }

    /// <summary>
    /// Handles the players movement. and jump mechanism.
    /// </summary>
    private void UserMovement()
    {
        if (Input.GetKeyDown(key: KeyCode.Space))
        {
            sfxManager.PlaySoundEffect("jump");
            IsJumping(true);
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            HasMagic(true);
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
            HasMagic(false);
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
    
    private void HasMagic(bool isMPressed)
    {
        _animator.SetBool(Grenade, isMPressed);
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
        if (other.transform.CompareTag("obstacle"))
        {
            sfxManager.PlaySoundEffect("crash");
            _animator.SetTrigger(IsDead);
            isDead = true;
            gameMusic.SetActive(false);
        }
        else if (other.transform.CompareTag("fire"))
        {
            sfxManager.PlaySoundEffect("fire");
            _animator.SetTrigger(IsDead);
            isDead = true;
            gameMusic.SetActive(false);
        }
        else
        {
            // Records every platform we stand on.
            currentPlatform = other.gameObject;
        }
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
    
    private void ThrowGrenade()
    {
        grenade.transform.position = grenadeStartPosition.position;
        grenade.SetActive(true);
        _grenadeRigidbody.AddForce(transform.forward * 4000);
        Invoke(nameof(RemoveGrenade),1);
    }
    
    private void RemoveGrenade()
    {
        grenade.SetActive(false);
    }
}