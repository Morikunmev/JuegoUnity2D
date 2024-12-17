using Unity.Mathematics;
using UnityEngine;
public class JohnMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    private Rigidbody2D Rigidbody2D;
    public int collectedItems = 0;
    public event System.Action<int> OnItemCollected;
    private float Horizontal;
    public float Speed;
    public float JumpForce;
    private bool Grounded;
    private int jumpCount = 0;
    public int maxJumps = 1;
    private Animator Animator;
    private float LastShoot;
    public int health = 5;
    public int maxHealth = 5;
    private float healTimer = 0f;
    private float healInterval = 3f;
    
    // Nuevas variables para el control de la caída
    public float fallThreshold = -1.017f;
    public float resetYPosition = -0.627f;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        health = maxHealth;
        gameObject.tag = "Player";
    }

    void Update()
    {
        // Verificar si el jugador cayó por debajo del límite
        if (transform.position.y < fallThreshold)
        {
            ResetPosition();
        }

        // Sistema de curación automática
        healTimer += Time.deltaTime;
        if (healTimer >= healInterval)
        {
            healTimer = 0f;
            Heal();
        }

        Horizontal = Input.GetAxis("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
            jumpCount = 0;
        }
        else
        {
            Grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && jumpCount < maxJumps)
        {
            Jump();
            jumpCount++;
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void ResetPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = resetYPosition;
        transform.position = newPosition;
        Rigidbody2D.velocity = Vector2.zero;
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void Jump()
    {
        Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    private void Heal()
    {
        if (health < maxHealth)
        {
            health = maxHealth;
            Debug.Log("¡Curación completa!");
        }
    }

    public void Hit()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem(int value)
    {
        collectedItems += value;
        OnItemCollected?.Invoke(collectedItems);

        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.AumentarTiempo();
        }

        Debug.Log($"Items Collected: {collectedItems}");
    }
}