using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NombreJug : MonoBehaviour
{
    public GameObject controlJuego;
    public InputField inNomJug; 



    public void enviarNombreJug()
    {
        controlJuego.GetComponent<Juego>().nomJug.text = inNomJug.text;
    }
}
