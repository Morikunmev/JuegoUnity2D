using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioClip Sound;
    public float Speed;
    public float volumen = 0.3f;
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;
    
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound, volumen);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        JohnMovement john = collision.collider.GetComponent<JohnMovement>();
        GruntScript grunt = collision.collider.GetComponent<GruntScript>();
        
        if (john != null)
        {
            john.Hit();
        }
        if (grunt != null && grunt.gameObject != transform.parent)
        {
            grunt.Hit();
        }
        DestroyBullet();
    }
}