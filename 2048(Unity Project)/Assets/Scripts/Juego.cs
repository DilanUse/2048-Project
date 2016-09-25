using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Juego : MonoBehaviour
{
    public int[,] tablero; // tablero del juego
    private bool[,] combinado; // determina que casillas se han combinado
    public Text puntaje; // puntajes del juego 
    public Text nomJug; // nombre del jugador
    public Sprite[] casillasSprite; // sprites de las casillas
    System.Random aleatorio; // para enteros aleatorios
    static bool press = false; // indica si se tiene presionada una tecla
    static int puntuacion = 0;


    // inicia la matriz de centinelas que indica si se combino una casilla
    void iniciarCombinacines()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                combinado[i, j] = false;
            }
        }
    } // fin de iniciarCombinaciones


    // Use this for initialization ----------------------------------------------------------
    void Start()
    {
        aleatorio = new System.Random(); // valor aleatorio
        tablero = new int[4, 4]; // inicia el tablero del juego
        combinado = new bool[4, 4];

        crearCasillas(2); // crea dos casillas
        actualizarImgCasillas(); 
    } // fin de Star ------------------------------------------------------------------------------


	// FixedUpdate is called once per physic actualization-----------------------------------
	void FixedUpdate ()
    {
        bool moved = false; // indica si se realizo algun movimiento
        

        if (Input.GetAxis("Horizontal") < 0 && !press)
        {
            // procesa los tres movimientos posibles para una casilla
            for (int move = 0; move < 3; move++)
            {
                // prosesa las filas 
                for (int fila = 0; fila < 4; fila++)
                {
                    // procesa las columnas
                    for (int col = 0; col < 4; col++)
                    {
                        if( tablero[fila,col] != 0 ) // si la pos en el tablero es una casilla
                        {
                            if (col > 0 && tablero[fila, col - 1] == 0)
                            {
                                moved = true;
                                tablero[fila, col - 1] = tablero[fila, col];
                                tablero[fila, col] = 0;
                            }
                            else if (col > 0 && tablero[fila, col - 1] == tablero[fila, col] &&
                               !combinado[fila, col] && !combinado[fila, col - 1] )
                            {
                                combinado[fila, col - 1] = true;
                                moved = true;
                                puntuacion += tablero[fila, col - 1] = tablero[fila, col] * 2;
                                tablero[fila, col] = 0;
                            } // fin del if...else
                        } // fin del if
                    } // fin del for que procesa las columnas
                } // fin del for que procesa las fila
            } // fin del for para los movimientos de una casilla


            if( moved )
                crearCasillas();


            iniciarCombinacines();
        } // fin del movimiento a la izquierda



        if (Input.GetAxis("Horizontal") > 0 && !press)
        {
            // procesa los tres movimientos posibles para una casilla
            for (int move = 0; move < 3; move++)
            {
                // prosesa las filas 
                for (int fila = 0; fila < 4; fila++)
                {
                    // procesa las columnas
                    for (int col = 3; col >= 0; col--)
                    {
                        if (tablero[fila, col] != 0) // si la pos en el tablero es una casilla
                        {
                            if (col < 3 && tablero[fila, col + 1] == 0)
                            {
                                moved = true;
                                tablero[fila, col + 1] = tablero[fila, col];
                                tablero[fila, col] = 0;
                            }
                            else if (col < 3 && tablero[fila, col + 1] == tablero[fila, col] &&
                                !combinado[fila, col] && !combinado[fila, col + 1] )
                            {
                                combinado[fila, col + 1] = true;
                                moved = true;
                                puntuacion += tablero[fila, col + 1] = tablero[fila, col] * 2;
                                tablero[fila, col] = 0;
                            } // fin del if...else
                        } // fin del if
                    } // fin del for que procesa las columnas
                } // fin del for que procesa las fila
            } // fin del for para los movimientos de una casilla

            if(moved)
                crearCasillas();


            iniciarCombinacines();
        } // fin del movimiento a la derecha



        if (Input.GetAxis("Vertical") < 0 && !press)
        {
            // procesa los tres movimientos posibles para una casilla
            for (int move = 0; move < 3; move++)
            {
                // prosesa las filas 
                for (int col = 0; col < 4; col++)
                {
                    // procesa las columnas
                    for (int fila = 3; fila >= 0; fila--)
                    {
                        if (tablero[fila, col] != 0) // si la pos en el tablero es una casilla
                        {
                            if (fila < 3 && tablero[fila + 1, col] == 0)
                            {
                                moved = true;
                                tablero[fila + 1, col] = tablero[fila, col];
                                tablero[fila, col] = 0;
                            }
                            else if (fila < 3 && tablero[fila + 1, col] == tablero[fila, col] &&
                                !combinado[fila,col] && !combinado[fila + 1,col] )
                            {
                                combinado[fila + 1, col] = true;
                                moved = true;
                                puntuacion += tablero[fila + 1, col] = tablero[fila, col] * 2;
                                tablero[fila, col] = 0;
                            } // fin del if...else
                        } // fin del if
                    } // fin del for que procesa las columnas
                } // fin del for que procesa las fila
            } // fin del for para los movimientos de una casilla


            if (moved)
                crearCasillas();


            iniciarCombinacines();
        } // fin del movimiento hacia abajo



        if (Input.GetAxis("Vertical") > 0 && !press)
        {
            // procesa los tres movimientos posibles para una casilla
            for (int move = 0; move < 3; move++)
            {
                // prosesa las filas 
                for (int col = 0; col < 4; col++)
                {
                    // procesa las columnas
                    for (int fila = 0; fila < 4; fila++)
                    {
                        if (tablero[fila, col] != 0) // si la pos en el tablero es una casilla
                        {
                            if (fila > 0 && tablero[fila - 1, col] == 0)
                            {
                                moved = true;
                                tablero[fila - 1, col] = tablero[fila, col];
                                tablero[fila, col] = 0;
                            }
                            else if (fila > 0 && tablero[fila - 1, col] == tablero[fila, col] &&
                                 !combinado[fila, col] && !combinado[fila - 1, col] )
                            {
                                combinado[fila - 1, col] = true;
                                moved = true;
                                puntuacion += tablero[fila - 1, col] = tablero[fila, col] * 2;
                                tablero[fila, col] = 0;
                            } // fin del if...else
                        } // fin del if
                    } // fin del for que procesa las columnas
                } // fin del for que procesa las fila
            } // fin del for para los movimientos de una casilla


            if (moved)
                crearCasillas();


            iniciarCombinacines();
        } // fin del movimiento hacia arriba


        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            press = true;
            puntaje.text = puntuacion.ToString();
        }
        else
            press = false;


        actualizarImgCasillas();
    } // fin de FixedUpdate-------------------------------------------------------------------


    // devuelve el indice de la casilla de acuerdo a su valor---------------------------------
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
    } // fin de getIndiceCasilla------------------------------------------------------------------


    // actualiza las imagenes de las casillas-------------------------------------------------------
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


    // crea la cantidad de casillas indicadas
    private void crearCasillas( int numCasillas = 1 )
    {
        int fila;
        int columna;

        for (int i = 0; i < numCasillas; i++)
        {
            do
            {
                fila = aleatorio.Next(4);
                columna = aleatorio.Next(4);
            } while (tablero[fila, columna] != 0);

            tablero[fila, columna] = aleatorio.Next(1, 3) * 2;
        } // fin del for
    } // fin de crearCasilla
} // fin de Juego
