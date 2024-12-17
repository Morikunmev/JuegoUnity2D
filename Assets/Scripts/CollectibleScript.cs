using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public int scoreValue = 1;
    private static int totalItems = 20;  // Total de items a recolectar
    private static int itemsCollected = 0;  // Contador estático de items recolectados
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            JohnMovement player = other.GetComponent<JohnMovement>();
            if (player != null)
            {
                player.CollectItem(scoreValue);
                itemsCollected++;
                
                // Verificar si se han recolectado todos los items
                if (itemsCollected >= totalItems)
                {
                    // Buscar el Timer y detenerlo
                    Timer timer = FindObjectOfType<Timer>();
                    if (timer != null)
                    {
                        timer.Victoria();
                    }
                }
                
                Destroy(gameObject);
            }
        }
    }
}