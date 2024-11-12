using UnityEngine;
public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    
    public float Speed;
    public float JumpForce;

    private bool Grounded;
    private int jumpCount = 0;  // Contador de saltos
    public int maxJumps = 1;    // Máximo número de saltos permitidos

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");

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
}