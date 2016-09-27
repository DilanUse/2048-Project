using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;


public class NombreJug : MonoBehaviour
{
    public InputField inNomnJug; // campo de texto con el nombre del jugador


    // envia el nombre del Jugador al controlador del Juego 
    public void enviarNombreJug()
    {
        FileStream nomJug; // archivo
        StreamWriter nom_out; // flujo de salida


        try
        {
            nomJug = new FileStream( Application.persistentDataPath + "/Nombre.txt", FileMode.Create, FileAccess.Write);


            using (nom_out = new StreamWriter(nomJug))
            {
                nom_out.WriteLine( inNomnJug.text );
            } // fin del using
        } // fin del try
        catch (IOException e)
        {
            Debug.Log(e.Message);
        } // fin del try...catch
    } // fin de enviarNombreJug
} // fin de NombreJug
