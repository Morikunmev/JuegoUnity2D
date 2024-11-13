// Estas líneas importan las librerías necesarias de Unity
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define la clase CameraScript que hereda de MonoBehaviour (necesario para scripts de Unity)
public class CameraScript : MonoBehaviour
{
    // Variables públicas que puedes modificar desde el Inspector de Unity
    public GameObject John;          // Referencia al personaje que la cámara seguirá
    public float xOffset = 0f;       // Desplazamiento horizontal de la cámara respecto al personaje
    public float yOffset = 0f;       // Desplazamiento vertical de la cámara respecto al personaje
    public float smoothSpeed = 2f;   // Velocidad de suavizado del movimiento
    
    // Variable privada para el cálculo del suavizado
    private Vector3 velocity = Vector3.zero;  // Vector que almacena la velocidad actual del movimiento

    // LateUpdate se ejecuta después de Update, mejor para seguimiento de cámara
    void LateUpdate()
    {
        // Comprueba si existe referencia al personaje para evitar errores
        if (John == null) return;

        // Calcula la posición objetivo donde queremos que esté la cámara
        Vector3 targetPosition = new Vector3(
            John.transform.position.x + xOffset,  // Posición X del personaje + desplazamiento
            John.transform.position.y + yOffset,  // Posición Y del personaje + desplazamiento
            transform.position.z                  // Mantiene la misma posición Z de la cámara
        );

        // Mueve la cámara suavemente hacia la posición objetivo
        transform.position = Vector3.SmoothDamp(
            transform.position,  // Posición actual de la cámara
            targetPosition,      // Posición a la que queremos llegar
            ref velocity,        // Vector de velocidad (se actualiza automáticamente)
            0.4f                // Tiempo que toma hacer la transición (más alto = más suave)
        );
    }
}