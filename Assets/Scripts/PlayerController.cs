using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _animator;
    public static GameObject player;
    public static GameObject currentPlatform;
    bool _canTurn = false;
    Vector3 _startPosition;
    private static readonly int Jumping = Animator.StringToHash("isJumping");
    private static readonly int Magic = Animator.StringToHash("HasMagic");


    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        player = this.gameObject;
        _startPosition = player.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        UserMovement();
    }

    private void UserMovement()
    {
        if (Input.GetKeyDown(key: KeyCode.Space) && !_animator.GetBool(Magic))
        {
            IsJumping(true);
        }
        else if (Input.GetKeyDown(key: KeyCode.M) && !_animator.GetBool(Jumping))
        {
            HasMagic(true);
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
            HasMagic(false);
        }
    }

    private void IsJumping(bool isSpacePressed)
    {
        _animator.SetBool(Jumping, isSpacePressed);
    }

    private void HasMagic(bool isMPressed)
    {
        _animator.SetBool(Magic, isMPressed);
    }
    

    private void OnCollisionEnter(Collision other)
    {
        currentPlatform = other.gameObject;
    }
}