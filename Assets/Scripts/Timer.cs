using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timer = 30f;
    public Text textoTimer;
    public GameObject John;
    private bool timerTerminado = false;

    void Start()
    {
        if (textoTimer == null)
        {
            textoTimer = GetComponentInChildren<Text>();
        }
    }

    void Update()
    {
        if (timerTerminado) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            timer = 0;
            UpdateTimerText();
            
            if (John != null && !timerTerminado)
            {
                John.SetActive(false);
                Destroy(John);
                timerTerminado = true;
            }
        }
    }

    void UpdateTimerText()
    {
        if (textoTimer != null)
        {
            textoTimer.text = "0:" + Mathf.FloorToInt(timer).ToString("00");
        }
    }

    public void AumentarTiempo()
    {
        timer += 5f;
        UpdateTimerText();
    }

    // Nuevo método para manejar la victoria
    public void Victoria()
    {
        timerTerminado = true;
        if (textoTimer != null)
        {
            textoTimer.text = "¡VICTORIA!";
        }
        Debug.Log("¡Has ganado! Recolectaste todos los items.");
    }
}