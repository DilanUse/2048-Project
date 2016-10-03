using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;


public class Juego : MonoBehaviour
{
    public int[,] tablero; // tablero del juego
    private int[,] tableroAux; // tablero auxiliar para verificaciones del juego
    private bool[,] combinado; // determina que casillas se han combinado
    public Text puntaje; // puntajes del juego 
    public Text nomJug; // nombre del jugador
    public Text record; // maxima puntuacion 
    public GameObject win; // ventana emergente para indicar la victoria del usuario
    public GameObject gameOver; // ventana emergente para indicar la derrota del usuario 
    public Sprite[] casillasSprite; // sprites de las casillas
    System.Random aleatorio; // para enteros aleatorios
    static bool press = false; // indica si se tiene presionada una tecla
    int puntuacion; // cuenta la puntuacion 
    bool winner; // determina si ya el usuario gano el juego

    enum Direccion{ derecha = 1, arriba = 2, izquierda = 3, abajo = 4, noImporta = 5 };


    float timeTouch;  /// instante en el que se realizo un toque valido
    /// ////////////////////
    /// </summary>




    // inicia la matriz de centinelas que indica si se combino una casilla
    void iniciarCombinacines()
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                combinado[i, j] = false;
    } // fin de iniciarCombinaciones


    // Use this for initialization ----------------------------------------------------------
    void Start()
    {



        // lee los valores iniciales del juego 
        FileStream archivo; // archivo
        StreamReader flujoIn; // flujo de salida
        string[] buffer;
        char[] separador = { ';' };



        try
        {
            // abre el archivo con el nombre y lee el nombre
            archivo = new FileStream( Application.persistentDataPath + "/Nombre.txt", 
                FileMode.OpenOrCreate, FileAccess.Read);
            using (flujoIn = new StreamReader(archivo))
            {
                nomJug.text = flujoIn.ReadLine();
            } // fin del using


            // abre el archivo con las puntuaciones y lee el primer puntaje
            archivo = new FileStream( Application.persistentDataPath + "/Puntajes.txt",
                FileMode.OpenOrCreate, FileAccess.Read);
            using (flujoIn = new StreamReader(archivo))
            {
                // si el archivo no esta vacio
                if (archivo.Length > 0)
                {
                    buffer = flujoIn.ReadLine().Split(separador);
                    record.text = buffer[1];
                }
            } // fin del using
        } // fin del try
        catch (IOException e)
        {
            Debug.Log(e.Message);
        } // fin del try...catch */



        timeTouch = 0;  /////////////////////////



        press = false;
        winner = false;
        puntuacion = 0; // iniciar la puntuacion
        aleatorio = new System.Random(); // valor aleatorio
        tablero = new int[4, 4]; // inicia el tablero del juego
        tableroAux = new int[4, 4]; // inicia el tablero auxiliar
        combinado = new bool[4, 4];

        crearCasillas(2); // crea dos casillas
        actualizarImgCasillas(); 
    } // fin de Star ------------------------------------------------------------------------------


	// FixedUpdate is called once per physic actualization-----------------------------------
	void Update ()
    {
        bool moved = false; // indica si se realizo algun movimiento


        //      if (Input.GetAxis("Horizontal") < 0 && !press)
        // izquierda
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && Time.time - timeTouch > 0.5f &&
            getDireccion(Input.GetTouch(0).deltaPosition) == Direccion.izquierda)
        {
            timeTouch = Time.time;
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
                                puntaje.text = puntuacion.ToString();
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



        //if (Input.GetAxis("Horizontal") > 0 && !press)
     /*   if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && /*press &&*/
         /*   (Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime) > 1500 &&
             (Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime) < 1800 ) */
             // derecha
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved  && Time.time - timeTouch > 0.5f && 
            getDireccion( Input.GetTouch(0).deltaPosition ) == Direccion.derecha )
        {
            timeTouch = Time.time;

            /*
            Depurar.text = (Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime).ToString() +
                " / " + (Time.time - timeTouch).ToString() + " / " +  
                ( getDireccion( Input.GetTouch(0).deltaPosition ).ToString() ); */





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
                                puntaje.text = puntuacion.ToString();
                                tablero[fila, col] = 0;
                            } // fin del if...else
                        } // fin del if
                    } // fin del for que procesa las columnas
                } // fin del for que procesa las fila
            } // fin del for para los movimientos de una casilla


        if (moved)
                crearCasillas();


            iniciarCombinacines();
        } // fin del movimiento a la derecha



        //if (Input.GetAxis("Vertical") < 0 && !press)
        // abajo
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && Time.time - timeTouch > 0.5f &&
            getDireccion(Input.GetTouch(0).deltaPosition) == Direccion.abajo)
        {
            timeTouch = Time.time;

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
                                puntaje.text = puntuacion.ToString();
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



        // arriba
        //  if (Input.GetAxis("Vertical") > 0 && !press)
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && Time.time - timeTouch > 0.5f &&
            getDireccion(Input.GetTouch(0).deltaPosition) == Direccion.arriba)
        {
            timeTouch = Time.time;
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
                                puntaje.text = puntuacion.ToString();
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


        // probando la entrada tactil 
        //      if (Input.touchCount > 0)
        //        GameObject.Find("Musica").GetComponent<AudioSource>().Stop();



       // if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
       /*      if( Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved )
              {
                  press = true;
                  puntaje.text = puntuacion.ToString();
              }
              else
                  press = false;  */



        copiarTablero();
        switch ( verificar() )
        {
            case 0:
                break;
            case 1:
                if (!winner)
                {
                    winner = true;
                    Debug.Log("Gano");
                    win.SetActive(true);
                    this.enabled = false;
                }
                break;
            case 2:
                Debug.Log("Perdio");
                gameOver.SetActive(true);
                this.enabled = false;
                break;
            default:
                break;
        } 


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



    // evalua si se gano o perdio
    int verificar()
    {
        int estado = 0; // determina el estado del Juego( 0:Continua/1:Gano/2:Perdio)

        //-------------------------------VERIFICANDO SI GANO------------------------------------
        for (int i = 0; i < 4; i++ )
            for (int j = 0; j < 4; j++ )
                if ( tablero[i, j] == 2048 )
                    estado = 1;


         //----------------------------VERIFICANDO SI PERDIO-----------------------------------------
        int estadoAux = 0; // determina si el tablero esta vacio


        // verifico si el tablero esta llena
        for (int fila = 0; fila < 4; fila++)
            for (int col = 0; col < 4; col++)
                if (tablero[fila, col] == 0)
                    estadoAux = 1;


        // si el tablero esta lleno
        if (estadoAux == 0)
        {
            estado = 2; // Supongo que se perdio


            // verifico si a la izquierda existe una combinacion
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if ( j > 0 && tableroAux[i, j - 1] == tableroAux[i, j])
                    {
                        estado = 0; // Continua el juego( se encontro una combinacion)
                        return estado; // retorno el estado del juego 
                    }//fin de else if


            copiarTablero(); // copio de nuevo al tablero auxiliar el original 


            // verifico si a la derecha existe una combinacion
            for (int fila = 0; fila < 4; fila++)
                for (int col = 3; col >= 0; col--)

                    if (col < 3 && tableroAux[fila, col + 1] == tableroAux[fila, col])
                    {
                        estado = 0; // Continua el juego( se encontro una combinacion)
                        return estado; // retorno el estado del juego
                    }//fin de else if



            copiarTablero(); // copio de nuevo al tablero auxiliar el original 


            // verifico hacia abajo existe una combinacion
            for (int fila = 3; fila >= 0; fila--)
                for (int col = 0; col < 4; col++)
                    if (fila < 3 && tableroAux[fila + 1, col] == tableroAux[fila, col])
                    {
                        estado = 0; // Continua el juego( se encontro una combinacion)
                        return estado; // retorno el estado del juego
                    }//fin de else if


            copiarTablero(); // copio de nuevo al tablero auxiliar el original 


            // verifico hacia abajo existe una combinacion
            for (int fila = 0; fila < 4; fila++)
                for (int col = 0; col < 4; col++)
                    if (fila > 0 && tableroAux[fila - 1, col] == tableroAux[fila, col])
                    {
                        estado = 0; // Continua el juego( se encontro una combinacion)
                        return estado; // retorno el estado del juego
                    }//fin de else if
        }//FIN DE VERIFICAR SI TIENE ALGUN MOVIMIENTO POSIBLE
        

        return estado; // retorno el estado del juego
    }//fin de verificacion


    // copia el tablero original a uno auxiliar
    private void copiarTablero()
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                tableroAux[i, j] = tablero[i, j];
    }//FIN DE COPIAR MATRIZ


    // determina la direccion de un deslizamiento 
    private Direccion getDireccion( Vector2 deslizamiento )
    {
        float maxAngle = 45.0f; // maximo angulo permitido para una direccion


        if (Vector2.Angle(Vector2.right, deslizamiento) < maxAngle) // derecha
            return Direccion.derecha;
        else if (Vector2.Angle(Vector2.up, deslizamiento) < maxAngle) // arriba
            return Direccion.arriba;
        else if (Vector2.Angle(Vector2.left, deslizamiento) < maxAngle) // izquierda
            return Direccion.izquierda;
        else if (Vector2.Angle(Vector2.down, deslizamiento) < maxAngle) // abajo
            return Direccion.abajo;


        return Direccion.noImporta;
    } // fin de getDireccion
} // fin de Juego
