using UnityEngine;

public class GruntScript : MonoBehaviour 
{
    public GameObject John;
    public GameObject BulletPrefab;
    public Vector3 enemyScale = new Vector3(1.0f, 1.0f, 1.0f);
    public LayerMask obstacleLayer;
    private float LastShoot;
    private int health = 3;
    public float speed = 3f;
    public float jumpForce = 5f;
    private bool isGrounded = true;
    private bool shouldJump = false;
    private Rigidbody2D rb2d;
    private float scaleX;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.localScale = enemyScale;
        scaleX = enemyScale.x;
        
        if (John == null)
        {
            John = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        if (John == null || isDead) return;

        Vector3 direction = John.transform.position - transform.position;
        
        if (direction.x >= 0.0f) 
            transform.localScale = new Vector3(Mathf.Abs(scaleX), enemyScale.y, enemyScale.z);
        else 
            transform.localScale = new Vector3(-Mathf.Abs(scaleX), enemyScale.y, enemyScale.z);

        float distance = Mathf.Abs(direction.x);

        CheckForObstacles();

        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }

        Vector2 movement = new Vector2(0, rb2d.velocity.y);
        
        if (distance > 1.0f)
        {
            movement.x = direction.x > 0 ? speed : -speed;
        }

        if (shouldJump && isGrounded)
        {
            movement.y = jumpForce;
            isGrounded = false;
            shouldJump = false;
        }

        rb2d.velocity = movement;
    }

    void CheckForObstacles()
    {
        Vector2 rayDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 1f, obstacleLayer);
        Debug.DrawRay(transform.position, rayDirection * 1f, Color.red);

        if (hit.collider != null && isGrounded)
        {
            shouldJump = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x > 0) 
            direction = Vector3.right;
        else 
            direction = Vector3.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.5f, Quaternion.identity);
        float bulletScale = (enemyScale.x + enemyScale.y) / 2;
        bullet.transform.localScale = new Vector3(bulletScale, bulletScale, 1);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        health--;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("death", true);
        rb2d.velocity = Vector2.zero;
        rb2d.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        
        // Destruir el objeto después de que termine la animación
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}