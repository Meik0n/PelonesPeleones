using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NaveController : MonoBehaviour
{
    [Header("Movimiento")]
    public float acceleration = 5;
    public float maxSpeed = 10;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private Vector2 moveVelocity;
    public Joystick joystick;

    [Header("Disparo")]
    public GameObject bullet;
    public Transform shootPoint;

    [Header("Sistema de vida")]
    public int MaxHealth = 5;
    [HideInInspector]public int currentHealth;
    public float Invulnerability_Time = 1f;
    private float blinkingTime = 0.1f;
    private bool invulnerability = false;
    protected float lastTimeHit = Mathf.NegativeInfinity;
    public Image hp;

    private Rigidbody2D m_Rigidbody2D;

    private AudioManager audioManager;

    private Animator anim;

    public IEnumerator StartInvulnerability()
    {
        invulnerability = true;
        yield return new WaitForSeconds(Invulnerability_Time);
        invulnerability = false;
    }
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = MaxHealth;
        audioManager = AudioManager.instance;

        audioManager.StopAllSounds();
        audioManager.Play("Music_Naves");
    }


    void Update()
    {
        JoystickController();
        InvulnerabilityManager();
        hp.fillAmount = (float)currentHealth/MaxHealth;

        if(horizontalMove > 0.1 || verticalMove > 0.1 || horizontalMove < -0.1 || verticalMove < -0.1)
        {
            anim.SetFloat("Velocity.X", 1);
        }
        else
        {
            anim.SetFloat("Velocity.X", -1);
        }
        
    }

    void FixedUpdate()
    {
        //m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + moveVelocity * Time.fixedDeltaTime);

        /** movimiento basico */
        m_Rigidbody2D.AddForce(moveVelocity, ForceMode2D.Impulse);

        /** ajustar velocidad para que no se pase de la velocidad maxima*/
        if(m_Rigidbody2D.velocity.x > maxSpeed)
        {
            m_Rigidbody2D.velocity = new Vector2(maxSpeed,m_Rigidbody2D.velocity.y);
        }
        if(m_Rigidbody2D.velocity.y > maxSpeed)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, maxSpeed);
        }
        if(m_Rigidbody2D.velocity.x < -maxSpeed)
        {
            m_Rigidbody2D.velocity = new Vector2(-maxSpeed,m_Rigidbody2D.velocity.y);
        }
        if(m_Rigidbody2D.velocity.y < -maxSpeed)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,-maxSpeed);
        }

        /** para que el cambio de direccion brusco sea mas rapido*/
        if(m_Rigidbody2D.velocity.x > 0 && horizontalMove < 0)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x - (acceleration * 10 * Time.fixedDeltaTime),m_Rigidbody2D.velocity.y);
        }
        if(m_Rigidbody2D.velocity.x < 0 && horizontalMove > 0)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x + (acceleration * 10 * Time.fixedDeltaTime),m_Rigidbody2D.velocity.y);
        }
        if(m_Rigidbody2D.velocity.y > 0 && verticalMove < 0)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,m_Rigidbody2D.velocity.y - (acceleration * 10 * Time.fixedDeltaTime));
        }
        if(m_Rigidbody2D.velocity.y < 0 && verticalMove > 0)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,m_Rigidbody2D.velocity.y + (acceleration * 10 * Time.fixedDeltaTime));
        }

        /** para que se pare cuando el player no toca el joystick (poco a poco) */
        if(verticalMove == 0 )
        {            
            if(m_Rigidbody2D.velocity.y > 0)
            {
                float deceleration = m_Rigidbody2D.velocity.y - acceleration * Time.fixedDeltaTime;
                if(deceleration > 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,deceleration);
                }
                else if(deceleration <= 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,0);
                }
            }  
            if(m_Rigidbody2D.velocity.y < 0)
            {
                float deceleration = m_Rigidbody2D.velocity.y + acceleration * Time.fixedDeltaTime;
                if(deceleration < 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,deceleration);
                }
                else if(deceleration >= 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,0);
                }
            }           
        }
        if(horizontalMove == 0)
        {
            if(m_Rigidbody2D.velocity.x > 0)
            {
                float deceleration = m_Rigidbody2D.velocity.x - acceleration * Time.fixedDeltaTime;
                if(deceleration > 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(deceleration,m_Rigidbody2D.velocity.y);
                }
                else if(deceleration <= 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(0,m_Rigidbody2D.velocity.y);
                }
            }

            if(m_Rigidbody2D.velocity.x < 0)
            {
                float deceleration = m_Rigidbody2D.velocity.x + acceleration * Time.fixedDeltaTime;
                if(deceleration < 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(deceleration,m_Rigidbody2D.velocity.y);
                }
                else if(deceleration >= 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(0,m_Rigidbody2D.velocity.y);
                }
            }  
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
                audioManager.Play("Daño_Leuko");       
                StartCoroutine(StartInvulnerability());
            }
            else
            {
                currentHealth  = 0;
            }
        }
    }

    public void Shoot()
    {
        Instantiate(bullet,shootPoint.position,Quaternion.identity);
        audioManager.Play("Disparo_Leuko");
    } 

    private void JoystickController()
    {
        if(joystick.Horizontal >= .2f)
        {
            horizontalMove = acceleration * Time.fixedDeltaTime;

            if(!audioManager.RunningAudioPlaying("Nave_Forward"))
            {
                audioManager.Play("Nave_Forward");
                audioManager.Stop("Nave_Quieta");
            }          
        }
        else if(joystick.Horizontal <= -.2f)
        {
            horizontalMove = -acceleration * Time.fixedDeltaTime;

            if(!audioManager.RunningAudioPlaying("Nave_Forward"))
            {
                audioManager.Play("Nave_Forward");
                audioManager.Stop("Nave_Quieta");
            }  
        }
        else{horizontalMove = 0;}

        if(joystick.Vertical >= .2f)
        {
            verticalMove = acceleration * Time.fixedDeltaTime;
            
            if(!audioManager.RunningAudioPlaying("Nave_Forward"))
            {
                audioManager.Play("Nave_Forward");
                audioManager.Stop("Nave_Quieta");
            }  
        }
        else if(joystick.Vertical <= -.2f)
        {
            verticalMove = -acceleration * Time.fixedDeltaTime;

            if(!audioManager.RunningAudioPlaying("Nave_Forward"))
            {
                audioManager.Play("Nave_Forward");
                audioManager.Stop("Nave_Quieta");
            }  
        }else{verticalMove = 0;}

        if(joystick.Horizontal == 0 && joystick.Vertical == 0 && audioManager.RunningAudioPlaying("Nave_Forward"))
        {
            audioManager.Stop("Nave_Forward");
            audioManager.Play("Nave_Quieta");
        }

        moveVelocity = new Vector2(horizontalMove,verticalMove);
    }  

    private void InvulnerabilityManager()
    {
        if (invulnerability)
        {
            Physics2D.IgnoreLayerCollision(9,10, true);
            
            if (Time.fixedTime >= blinkingTime)
            {
                this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                blinkingTime = Time.fixedTime + 0.10f;
            }
            else
            {
                this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
        }

        if (invulnerability == false)
        {
            Physics2D.IgnoreLayerCollision(9,10,false);
            this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }
}
