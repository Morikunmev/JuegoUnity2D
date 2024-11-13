using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        
    }
    private void FixedUpdate(){
        Rigidbody2D.velocity = Direction * Speed;
    }
    public void SetDirection(Vector2 direction){
        Direction = direction;
    }
    public void DestroyBullet(){
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        JohnMovement john = collision.collider.GetComponent<JohnMovement>();
        GruntScript grunt = collision.collider.GetComponent<GruntScript>();
        if (john != null){
            john.Hit();
        }
        if (grunt!= null){
            grunt.Hit();
        }
        DestroyBullet();
}}
