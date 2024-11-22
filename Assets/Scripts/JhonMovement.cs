using Unity.Mathematics;
using UnityEngine;
public class JohnMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public float Speed;
    public float JumpForce;
    private bool Grounded;
    private int jumpCount = 0;  // Contador de saltos
    public int maxJumps = 1;    // Máximo número de saltos permitidos
    //Parte 3
    private Animator Animator;
    
    private float LastShoot; //Para calcular el tiempo del ultimo disparo
    public int health = 5;
    public int maxHealth = 5;   // Vida máxima
    private float healTimer = 0f; // Temporizador para la curación
    private float healInterval = 3f; // Intervalo de curación en segundos

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        health = maxHealth; // Iniciamos con vida completa
    }

    void Update()
    {
        // Sistema de curación automática
        healTimer += Time.deltaTime;
        if (healTimer >= healInterval)
        {
            healTimer = 0f; // Reinicia el temporizador
            Heal(); // Cura al personaje
        }

        Horizontal = Input.GetAxis("Horizontal");

        if(Horizontal <0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if(Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal!=0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
            jumpCount = 0; // Reiniciamos el contador cuando tocamos el suelo
        }
        else 
        {
            Grounded = false;
        }

        // Permitimos saltar si aún tenemos saltos disponibles
        if (Input.GetKeyDown(KeyCode.W) && jumpCount < maxJumps)
        {
            Jump();
            jumpCount++; // Incrementamos el contador de saltos
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f){
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot(){
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction *0.1f, quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void Jump()
    {
        Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0); // Opcional: resetea la velocidad vertical
        Rigidbody2D.AddForce(Vector2.up * JumpForce); 
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    // Nuevo método para curar
    private void Heal()
    {
        if (health < maxHealth)
        {
            health = maxHealth;
            Debug.Log("¡Curación completa!"); // Opcional: para debug
        }
    }

    public void Hit(){
        health--;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}