
/**
 * Representación de una puntuación
 */
public class Puntuacion
{
    public string nombre; // nombre para el puntaje
    public int puntuacion; // puntacion para el puntaje


    // contructor predeterminado
    public Puntuacion() : this( null, 0 ){}


    // contructor paramterico
    public Puntuacion( string nombre, int puntuacion )
    {
        this.nombre = nombre;
        this.puntuacion = 0;
    } // fin del constructor 


    // retorna una representacion de una puntuacion en la forma
    // nombre;puntuacion
    public override string ToString()
    {
        return this.nombre + ";" + this.puntuacion;
    } // fin de toString
} // fin de puntuacion;
