using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Vida")]
    public int MaxHealth = 5;
    private int currentHealth;
    public float Invulnerability_Time = 1f;
    private float blinkingTime = 0.1f;
    private bool invulnerability = false;
    protected float lastTimeHit = Mathf.NegativeInfinity;
    public Image hpbar;
    [Header("Salto")]
    public float Gravity_Scale = 1f;
    public float m_JumpForce = 400f;
    //better jump
    public float fallMultiplier = 2.5f;
    private bool jump;
    public GameObject jumpParticles;
    public GameObject onCollisionJumpParticles;
    public Transform particlePosition;
    
    [Header("Deteccion de suelo")]
    public LayerMask m_WhatIsGround;    // A mask determining what is ground to the character
    public Transform m_GroundCheck1;
    public Transform m_GroundCheck2;
    private bool m_Grounded;            // Whether or not the player is grounded.

    [Header("Movimiento")]
    public float runSpeed = 40f;   //
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    public float knockbackForce = 5f;

    private float horizontalMove = 0f;

    [Range(0, .3f)] public float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    [Header("Disparo")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public GameObject shotParticlesRight;
    public GameObject shotParticlesLeft;

    [Header("Joystick")]
    public Joystick joystick;
    [Header("Sonido")]
    private AudioManager audioManager;

    private Scene escena;

    private Animator anim;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        audioManager = AudioManager.instance;
        currentHealth = MaxHealth;
        anim = gameObject.GetComponentInChildren<Animator>();

        audioManager.StopAllSounds();
        if(!audioManager.RunningAudioPlaying("Music_Plataformas"))
        {
            audioManager.Play("Music_Plataformas");
        }
        
    }

    public IEnumerator StartInvulnerability()
    {
        invulnerability = true;
        yield return new WaitForSeconds(Invulnerability_Time);
        invulnerability = false;
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        escena = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escena.name);
        //yield break;
    }

    void Update()
    {
        m_Rigidbody2D.gravityScale = Gravity_Scale;
        if(joystick.Horizontal >= .2f)
        {
            horizontalMove = runSpeed;

            if(!m_FacingRight)
            {
                Flip();
            }
        }else if(joystick.Horizontal <= -.2f)
        {
            horizontalMove = -runSpeed;
            if(m_FacingRight)
            {
                Flip();
            }
        }else
        {
            horizontalMove = 0;
            audioManager.Stop("Correr");
        }

        if (m_Rigidbody2D.velocity.y < 0) //caes mas rápido de lo que subes
        {
            m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if((horizontalMove >0 || horizontalMove < 0) && m_Grounded)
        {
            if(!audioManager.RunningAudioPlaying("Correr"))
            {
                audioManager.Play("Correr");
            }
        }
        else{audioManager.Stop("Correr");}

        if(Mathf.Abs(m_Rigidbody2D.velocity.x) < 0.1f)
        {
            m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
        }

        anim.SetFloat("Speed",Mathf.Abs( m_Rigidbody2D.velocity.x));
        anim.SetFloat("SpeedY",m_Rigidbody2D.velocity.y);
        anim.SetFloat("Life", currentHealth - 1);

        hpbar.fillAmount = (float)currentHealth/MaxHealth;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        RaycastHit2D ray1 = Physics2D.Raycast(m_GroundCheck1.position, Vector2.down, .5f, m_WhatIsGround);
        RaycastHit2D ray2 = Physics2D.Raycast(m_GroundCheck2.position, Vector2.down, .5f, m_WhatIsGround);
        // The player is grounded if a raycast to the groundcheck position hits anything designated as ground
           
        if(m_Rigidbody2D.velocity.y >0)
        {
            m_Grounded = false;
            anim.SetBool("Grounded", false);
        }
        
        if (ray1 || ray2 && m_Rigidbody2D.velocity.y <=0)
        {
            m_Grounded = true;
            anim.SetBool("Grounded", true);
        }
     
        Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        
    }
    public void Move(float move, bool jump)
    {
		
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        
       

        if (jump)
        {
            audioManager.Play("Salto");   
            m_Grounded = false;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
        }

        if (invulnerability)
        {
            Physics2D.IgnoreLayerCollision(9,10, true);
            
            if (Time.fixedTime >= blinkingTime)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                blinkingTime = Time.fixedTime + 0.10f;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (invulnerability == false)
        {
            Physics2D.IgnoreLayerCollision(9,10,false);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void Jump()
    {      
        if (m_Grounded)
        {
            jump = true;
        }
    }

    public void LooseLife(int LifeToLoose)
    {
        if (Time.time - lastTimeHit > 1)
        {
            lastTimeHit = Time.time;
            if (currentHealth - LifeToLoose > 0)
            {
                currentHealth -= LifeToLoose;
                audioManager.Play("RecibirDaño");         
                StartCoroutine(StartInvulnerability());
            }
            else
            {
                currentHealth  = 0;
            }
        }

        if (currentHealth  <= 0)
        {          
            StartCoroutine(RestartGame());
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab,shootPoint);
        audioManager.Play("DisparoBP");
        if(m_FacingRight)
        {
            Instantiate(shotParticlesRight, shootPoint.position, shotParticlesRight.transform.rotation);
        }
        else if(!m_FacingRight)
        {
            Instantiate(shotParticlesLeft, shootPoint.position, shotParticlesLeft.transform.rotation);
        }
    }

    public void Flip()
    {
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if(m_FacingRight)
            {
                m_Rigidbody2D.AddForce(-Vector2.right * knockbackForce, ForceMode2D.Impulse);
            }
            else if(!m_FacingRight)
            {
                m_Rigidbody2D.AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
            }             
        }

        if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Seta")
        {
            Instantiate(onCollisionJumpParticles,particlePosition.position,Quaternion.identity);
            audioManager.Play("Aterrizar");
        }  

        if(col.gameObject.CompareTag("enemybullet"))
        {
            LooseLife(1);
        }      
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Seta")
        {
            Instantiate(jumpParticles,particlePosition.position,Quaternion.identity);
        }
    }
}
