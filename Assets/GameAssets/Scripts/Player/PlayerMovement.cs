using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    
    //class
    private GameManager _gameManager;
    private ControllerAnimation controllerAnimation;
    private DoorAnimation doorAnimation;
    private UIManager uiManager;
    private PlayerExplosion _playerExplosion;
    private CameraShake _cameraShake;
    private AudioManager _audioManager;
    
    //rotate
    private float nextRotateTime = 0f; //bir sonraki dönüş zamanı
    private bool isActive = false;
    
    //setRotateCenter
    public Transform playerPosition;
    
    public Animator animator;

    private float horizontal;
    private bool isFacingRight = true;

    public bool isAlive = true;

    //gfx
    [SerializeField] public GameObject gfx;
    
    //dash
    private bool canDash = true;
    private bool isDashing;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    private float jumpForce = 15f;
    
    //DOTweening
    [SerializeField] private Transform playerDeadScale;
    
    //change fade
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    //coyote and buffer variables
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private float jumpBufferCounter;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerExplosion = FindObjectOfType<PlayerExplosion>();
        _cameraShake = FindObjectOfType<CameraShake>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        playerPosition.position = transform.position;

        if (Input.GetKeyDown(KeyCode.X) && controllerAnimation != null)
        {
            if (Time.time >= nextRotateTime && isActive)
            {
                _gameManager.RotateLevel();
                controllerAnimation.OpenController();
                nextRotateTime = Time.time + 1.2f;
                
                //rotater audio
                _audioManager.Play(SoundType.Controller);
            }
        }

        if (isAlive == true)
        {
            //alive check begin point    
            
            if (isDashing)
            {
                return;
            }

            horizontal = Input.GetAxisRaw("Horizontal");

            animator.SetFloat("Speed", Mathf.Abs(horizontal));

            if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpBufferCounter = 0f;
                
                //audio jump
                _audioManager.Play(SoundType.PlayerJump);
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                coyoteTimeCounter = 0f;
            }

            if (isGrounded())
            {
                coyoteTimeCounter = coyoteTime;
                animator.SetBool("IsJump", false);
            }
            
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
                animator.SetBool("IsJump", true);
            }
            
            if (Input.GetKeyDown(KeyCode.Z) && canDash)
            {
                StartCoroutine((Dash()));
                
                //dash audio
                _audioManager.Play(SoundType.Dash);
            }

            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferCounter = jumpBufferTime;
            }

            else
            {
                jumpBufferCounter -= Time.deltaTime;    
            }
            
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x, transform.localScale.y * .3f).normalized * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Key"))
        {
            _gameManager.PlayerTouchedKey();
            Destroy(col.gameObject);
            
            //audio key
            _audioManager.Play(SoundType.Key);
        }

        DoorAnimation doorAnimation = col.GetComponent<DoorAnimation>();//hata buradaymış
        
        if (col.gameObject.CompareTag("Door"))
        {
            if (doorAnimation != null && _gameManager.hasPlayerKey)
            {
                doorAnimation.OpenDoor();
                DOVirtual.Float(0, .8f, .4f, value =>
                {
                    _spriteRenderer.material.SetFloat("_FadeAmount", value);
                }).SetDelay(.1f).SetLink(gameObject);
                
                //audio
                _audioManager.Play(SoundType.Door);
            }
            
            _gameManager.NextLevel();
        }

        if (col.gameObject.CompareTag("Controller"))
        {
            isActive = true;
            Debug.Log("isActiveTrue");
            controllerAnimation = col.GetComponent<ControllerAnimation>();
        }
        
        if (col.CompareTag("Trap") && playerDeadScale != null)
        {
            _gameManager.RespawnPlayer();
            
            //explode
            gfx.SetActive(false);
            PlayerExplosion.instance.PlayerDeadExplode();   
            _cameraShake.Shake();
            
            //audio
            _audioManager.Play(SoundType.PlayerDead);
            
            isAlive = false;
                
            if (playerDeadScale == null)
            {
                OnDestroy();
            }
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(playerDeadScale);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Controller"))
        {
            isActive = false;
            Debug.Log("isActiveFalse");
            controllerAnimation = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Trap"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            _gameManager.RespawnPlayer();
            
            //explode
            gfx.SetActive(false);
            PlayerExplosion.instance.PlayerDeadExplode();   
            //shake
            _cameraShake.Shake();
            
            _audioManager.Play(SoundType.PlayerDead);
            
            isAlive = false;    
            
            if (playerDeadScale == null)
            {
                OnDestroy();
            }
            
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJump" , false);
    }
}
