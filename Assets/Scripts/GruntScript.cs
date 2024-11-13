using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject John;
    public GameObject BulletPrefab;
    private float LastShoot;
    private int health = 3;
    
    public float speed = 3f;
    public float jumpForce = 5f;
    private bool isGrounded = true;
    private bool shouldJump = false; // Nueva variable para controlar el salto
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        if (John == null)
        {
            John = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        if (John == null) return;

        Vector3 direction = John.transform.position - transform.position;
        
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(direction.x);

        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }

        // Movimiento y salto
        Vector2 movement = new Vector2(0, rb2d.velocity.y);
        
        if (distance > 1.0f) 
        {
            movement.x = direction.x > 0 ? speed : -speed;
        }

        // Si debe saltar y está en el suelo
        if (shouldJump && isGrounded)
        {
            movement.y = jumpForce;
            isGrounded = false;
            shouldJump = false; // Resetea el flag de salto
        }

        rb2d.velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else // Si choca con cualquier otra cosa que no sea el suelo
        {
            shouldJump = true; // Activa el flag para saltar
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}