using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Necesario para usar Image


public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private JohnMovement johnMovement;
    private float vidaMaxima;
    void Start()
    {
        johnMovement = GameObject.Find("John").GetComponent<JohnMovement>();
        vidaMaxima = johnMovement.health;
    }

    // Update is called once per frame
    void Update()
    {
        rellenoBarraVida.fillAmount = johnMovement.health / vidaMaxima;
    }
}
