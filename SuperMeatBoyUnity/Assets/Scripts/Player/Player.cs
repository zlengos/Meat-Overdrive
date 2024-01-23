using UnityEngine;
using Assets.Scripts;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRendererPlayer;
    [SerializeField] private Animator _animator;
    [SerializeField] public GameObject bloodSteps;
    [SerializeField] private LayerMask _earth, _invisibleWall;

    [SerializeField] private float _speed = 7, _jumpForce;
    private float _startTimeJump, _horizontalMove = 0f, _touchingLeftOrRight = 0;
    private bool _isGround, _isTouchingLeft, _isTouchingRight, _wallJumping, _facingRight = true;
    public Vector3 startPlayerPosition;

    private Recorder _recorder;

    private void Awake()
    {
        _recorder = GetComponent<Recorder>(); 
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRendererPlayer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        //EventManager.instance.onLevelFinished += OnGoalReached;
        //EventManager.instance.onSpawnPlayerOnStartPosition += OnRestartLevel;
        SetStartPlayerPosition();
    }


    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        _isGround = Physics2D.OverlapBox(new Vector2(transform.position.x + 0.03f, transform.position.y - 0.7f), new Vector2(0.8f, 0.2f), 0f, _earth);
        _isTouchingLeft = Physics2D.OverlapBox(new Vector2(transform.position.x - 0.4f, transform.position.y - 0.1f), new Vector2(0.2f, 1.15f), 0f, _earth);
        _isTouchingRight = Physics2D.OverlapBox(new Vector2(transform.position.x + 0.48f, transform.position.y - 0.1f), new Vector2(0.2f, 1.15f), 0f, _earth);

        _animator.SetFloat("HorizontalMove", Mathf.Abs(_horizontalMove));

        if (Input.GetAxis("Horizontal") != 0)
            FlipPlayer(_horizontalMove);

        if (Input.GetKey(KeyCode.LeftShift))
            _speed = 12f;
        else _speed = 7f;

        if ((!_isTouchingLeft && !_isTouchingRight) || _isGround)
        {
            _rigid.velocity = new Vector2(_horizontalMove * _speed, _rigid.velocity.y);
            if (_horizontalMove == 0 || !_isGround)
                bloodSteps.SetActive(false);
            if (_horizontalMove > 0 && !_facingRight)
                FlipStepsPlayer();
            if (_horizontalMove < 0 && _facingRight)
                FlipStepsPlayer();
            if (_horizontalMove != 0 && _isGround)
                bloodSteps.SetActive(true);
        }

        if (_isTouchingLeft && !_isGround)
        {
            _touchingLeftOrRight = 1;
            _animator.SetBool("WallJumping", true);
        }
        else if (_isTouchingRight && !_isGround)
        {
            _touchingLeftOrRight = -1;
            _animator.SetBool("WallJumping", true);
        }
        else
            _animator.SetBool("WallJumping", false);

        if ((Input.GetKeyDown(KeyCode.Space)) && _isGround)
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);

        if (Input.GetKeyDown(KeyCode.Space) && (_isTouchingLeft || _isTouchingRight) && !_isGround)
        {
            _wallJumping = true;
            Invoke(nameof(SetJumpingToFalse), 0.02f);
        }
        if (_wallJumping)
            _rigid.velocity = new Vector2(_speed * _touchingLeftOrRight, _jumpForce);

        if (_isGround == false)
            _animator.SetBool("Jumping", true);
        else
            _animator.SetBool("Jumping", false);
    }

    private void LateUpdate()
    {
        ReplayData data = new ReplayData(this.transform.position);
        _recorder.RecordReplayFrame(data);
    }

    //private void OnGoalReached()
    //{

    //}

    //private void OnRestartLevel()
    //{

    //}


    public Vector3 SetStartPlayerPosition()
    {
        startPlayerPosition = transform.position;
        Debug.Log($"Player position on start is {startPlayerPosition}");
        return startPlayerPosition;
    }
    private void FlipPlayer(float horizontalMove)
    {
        if (horizontalMove < 0)
            _spriteRendererPlayer.flipX = true;
        else if (horizontalMove > 0)
            _spriteRendererPlayer.flipX = false;
    }

    private void FlipStepsPlayer()
    {
        Quaternion currentRotation = bloodSteps.transform.localRotation;
        Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, -currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);
        bloodSteps.transform.localRotation = newRotation;
        _facingRight = !_facingRight;
    }

    private void SetJumpingToFalse() => _wallJumping = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        Gizmos.DrawCube(new Vector2(transform.position.x - 0.4f, transform.position.y - 0.1f), new Vector2(0.2f, 1.15f));
        Gizmos.DrawCube(new Vector2(transform.position.x + 0.48f, transform.position.y - 0.1f), new Vector2(0.2f, 1.15f));
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x + 0.04f, transform.position.y - 0.7f), new Vector2(0.8f, 0.2f));
    }

}
