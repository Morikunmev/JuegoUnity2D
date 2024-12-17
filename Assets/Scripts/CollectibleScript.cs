using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public int scoreValue = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            JohnMovement player = other.GetComponent<JohnMovement>();
            if (player != null)
            {
                player.CollectItem(scoreValue);
                Destroy(gameObject);
            }
        }
    }
}