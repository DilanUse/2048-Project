using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;


/**
 * Actualiza las 10 maximas puntuaciones 
 */
public class Puntuaciones : MonoBehaviour
{
    public Text nombres; // campo de texto de los nombres
    public Text puntos; // campo de testo de los puntos
    Puntuacion[] registros; // registros de puntajes



    // muestra los puntajes al entrar en maximas puintuaciones
    void Start()
    {
        this.LeerPuntaje();

        nombres.text = puntos.text = "";


        for (int i = 0; i < registros.Length - 1; i++)
        {
            nombres.text += (i + 1) + ".-" +  ( registros[i].nombre != null ? registros[i].nombre : "AAAAA" )
                + System.Environment.NewLine;
            puntos.text += registros[i].puntuacion + System.Environment.NewLine;
        } // fin del for
    } // fin de estar


    // lee los puntajes alamcenados en disco
    private void LeerPuntaje()
    {
        FileStream puntajes; // archivo
        StreamReader puntajes_in; // flujo de entrada
        string[] buffer; // lineas del archivo
        char[] separador = { ';' }; // delimitador



        try
        {
            puntajes = new FileStream("Assets/Save/Puntajes.txt", FileMode.Open, FileAccess.Read);

           
            using ( puntajes_in = new StreamReader(puntajes) )
            {
                registros = new Puntuacion[11];


                
                for ( int i = 0; i < registros.Length ; ++i )
                {
                    if( !puntajes_in.EndOfStream )
                    {
                        buffer = puntajes_in.ReadLine().Split(separador);
                        registros[i] = new Puntuacion( buffer[0], Convert.ToInt32(buffer[1]));
                    }
                    else
                        registros[i] = new Puntuacion();
                } // fin del for
            } // fin del using
        } // fin del try
        catch ( IOException e )
        {
            Debug.Log(e.Message);
        } // fin del try...catch
    } // fin de leer 



    // actualiza los puntajes ingresando uno nuevo y ordenandolos
    public void actualizarPuntajes()
    {
        FileStream puntajes; //archivo
        StreamWriter puntajes_out; // flujo de salida
        int registro = 0;


        Debug.Log("Se actualizo el puntaje");
        this.LeerPuntaje();
        registros[10] = new Puntuacion( nombres.text, Convert.ToInt32(puntos.text) );


        try
        {
            puntajes = new FileStream("Assets/Save/Puntajes.txt", FileMode.Truncate, FileAccess.Write);


            using (puntajes_out = new StreamWriter(puntajes))
            {
                var PuntajesOrdenados =
                    from puntaje in registros
                    orderby puntaje.puntuacion descending
                    select puntaje;


                foreach (var puntaje in PuntajesOrdenados )
                {
                    if (registro >= 10)
                        break;


                    puntajes_out.WriteLine(puntaje);
                    ++registro;
                } // fin del foreach
            } // fin del using
        } // fin del try
        catch (IOException e)
        {
            Debug.Log(e.Message);
        } // fin del try...catch
    } // fin de actualizarPuntajes
} // fin de Puntuaciones 
