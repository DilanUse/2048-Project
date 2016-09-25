using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Juego : MonoBehaviour
{
    public int[,] tablero; // tablero del juego
    public Text puntaje; // puntajes del juego 
    public Sprite[] casillasSprite; // sprites de las casillas


    // Use this for initialization
    void Start()
    {
        System.Random aleatorio = new System.Random(); // valor aleatorio
        tablero = new int[4, 4]; // inicia el tablero del juego


        // creo dos casilla aleatorias y actualizo sus imagenes
        tablero[ aleatorio.Next(4), aleatorio.Next(4) ] = aleatorio.Next(1, 3) * 2;
        tablero[aleatorio.Next(4), aleatorio.Next(4)] = aleatorio.Next(1, 3) * 2;
        actualizarImgCasillas();
    } // fin de Star 


	// FixedUpdate is called once per physic actualization
	void FixedUpdate ()
    {

	} // fin de FixedUpdate


    // devuelve el indice de la casilla de acuerdo a su valor
    private int getIndiceCasilla( int numCasilla )
    {
        int indice = 0; // indice de la casilla


        // mientras numero de la casilla sea mayor a 2
        while( numCasilla != 2 )
        {
            numCasilla /= 2;
            ++indice; 
        } // fin del while 


        return indice; // devuelve el indice 
    } // fin de getIndiceCasilla


    // actualiza las imagenes de las casillas
    private void actualizarImgCasillas()
    {
        GameObject casillaObj; // casilla a ser actualizada
        Image casillaImg; // componente Img de la casilla a ser actualizada


        // procesa y actualiza todas las casillas del tablero
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                casillaObj = GameObject.Find("Casilla" + i + "," + j); // obtengo cada casilla
                casillaImg = casillaObj.GetComponent<Image>(); // obtengo el componente Img de cada casilla


                // si no hay un cero en el tablero en la posicion (i,j) y no se a actualizado la imagne
                if (tablero[i, j] != 0 )
                    casillaImg.sprite = casillasSprite[getIndiceCasilla(tablero[i, j])]; // actualizo la imagen
                else if (casillaImg.sprite) // si no enotnces es cero, y si tiene una imagen 
                    casillaImg.sprite = null; // quito la imagen de la casilla
            } // fin del for interior
        } // fin del for exterior
    } // fin de actualizarImgCasillas
} // fin de Juego
