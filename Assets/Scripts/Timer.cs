using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timer = 30f;
    [SerializeField] private Text textoTimer; // Agregamos SerializeField para asegurarnos que se asigne en el Inspector
    public GameObject John;
    private bool timerTerminado = false;

    void Start()
    {
        // Verificar que tengamos todas las referencias necesarias
        if (textoTimer == null)
        {
            Debug.LogError("Por favor asigna el componente Text en el Inspector");
            enabled = false; // Desactivar el script si falta el Text
            return;
        }

        if (John == null)
        {
            John = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        if (timerTerminado) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            ActualizarTextoTimer();
        }
        else
        {
            timer = 0;
            ActualizarTextoTimer();
            
            if (John != null && !timerTerminado)
            {
                John.SetActive(false);
                Destroy(John);
                timerTerminado = true;
            }
        }
    }

    private void ActualizarTextoTimer()
    {
        if (textoTimer != null)
        {
            int minutos = Mathf.FloorToInt(timer / 60);
            int segundos = Mathf.FloorToInt(timer % 60);
            textoTimer.text = minutos.ToString() + ":" + segundos.ToString("00");
        }
    }
}