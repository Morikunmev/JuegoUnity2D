using UnityEngine;
public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    //VARIABLES QUE CONTROLAN LA FUERZA DEL SALTO Y LA VELOCIDAD DEL OBJETO
    public float Speed;
    public float JumpForce; //VARIABLE PUBLICA PARA AJUSTAR LA FUERZA

    //DENTRO DEL PASO 2
    private bool Grounded;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    //----------------CODIGO QUE DETECTA LOS INPUTS DEL USUARIO----------------

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W)){
            Jump();
        };
    }

    //----------------CODIGO QUE TIENE TODAS LOS RIGIDBODY----------------
    private void Jump()
    {
    Rigidbody2D.AddForce(Vector2.up * JumpForce); 

    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal *Speed, Rigidbody2D.velocity.y);
    }
}