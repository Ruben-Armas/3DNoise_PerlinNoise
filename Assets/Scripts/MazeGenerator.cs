using UnityEngine;

public class MazeGenerator : MonoBehaviour {
    public int width = 10; // Ancho del laberinto
    public int large = 10; // Altura del laberinto
    public int height = 10; // Altura del laberinto

    public float detail;
    public int seed;

    public int[,] maze; // Representación del laberinto en un array bidimensional
    private int[,,] map; // Representación del mapa en un array tridimensional

    public GameObject floor;
    public GameObject wall;
    public GameObject wall_2;

    void Start() {
        seed = Random.Range(100000, 900000); // Aleatoriedad
        GenerateMaze();
        // Mover cámara al centro del mapa generado
        //Camera.main.transform.position =
        //new Vector3(((float)width / 2) - 0.5f, ((float)large / 2) - 0.5f, -10);
    }

    void GenerateMaze() {
        maze = new int[width, large];
        // Inicializar el laberinto
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < large; j++) {
                maze[i, j] = 1; // 1 representa un muro, 0 representa un camino
            }
        }
        // Llamar al método de generación recursiva
        GeneratePath(1, 1);
        // Añade altura usando perlinNoise
        PerlinNoiseY(maze);

        // Puedes imprimir el laberinto en la consola para ver la representación
        //PrintMaze();
    }

    void GeneratePath(int x, int y) {
        maze[x, y] = 0; // Marcar la posición actual como parte del camino
                        // Direcciones posibles (arriba, derecha, abajo, izquierda)
        int[] directions = { 0, 1, 2, 3 };
        Shuffle(directions);
        // Explorar direcciones posibles
        for (int i = 0; i < directions.Length; i++) {
            int nextX = x + 2 * (directions[i] == 1 ? 1 : (directions[i] == 3 ? -1 : 0));
            int nextY = y + 2 * (directions[i] == 2 ? 1 : (directions[i] == 0 ? -1 : 0));
            // Verificar si la próxima posición está dentro de los límites y aún no ha sido visitada
            if (nextX > 0 && nextX < width - 1 && nextY > 0 && nextY < large - 1 && maze[nextX, nextY] == 1) {
                maze[x + (nextX - x) / 2, y + (nextY - y) / 2] = 0; // Romper el muro entre las posiciones
                GeneratePath(nextX, nextY);
            }
        }
    }

    void Shuffle(int[] array) {
        // Método para barajar un array
        for (int i = array.Length - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    // Añade altura con Perlin Noise cuando hay muros en el maze 2D
    void PerlinNoiseY(int[,] maze) {
        for (int x = 0; x < maze.GetLength(0); x++) {   // width
            for (int z = 0; z < maze.GetLength(1); z++) {   // large
                //Si hay muros en el 2D, le doy altura con PerlinNoise
                if (maze[x, z] != 0) {
                    // PerlinNoise
                    int terrainHeight = (int)(Mathf.PerlinNoise((x / 2 + seed) / detail, (z / 2 + seed) / detail) * detail);

                    // Pone un limite al valor del PerlinNoise
                    if (terrainHeight >= large)
                        terrainHeight = large;

                    // Generar cubos desde y = 0 hasta terrainHeight
                    for (int y = 0; y < terrainHeight; y++) {
                        if (y == terrainHeight - 1) {
                            Instantiate(wall, new Vector3(x, y, z), Quaternion.identity);
                            //map[x, y, z] = 1; // 1 representa un muro, 0 representa un camino
                        }
                        else
                            Instantiate(wall_2, new Vector3(x, y, z), Quaternion.identity);
                        //Debug.Log(x + "," + y + "," + z);
                    }
                } else
                    Instantiate(floor, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }

    // Se puede modificar para mostrar que lo muestre el map[][][] y no con los Instance del bucle, actualmente muestra el maze 2D
    void PrintMaze() {
        // Método para imprimir el laberinto en la consola
        for (int j = large - 1; j >= 0; j--) {
            for (int i = 0; i < width; i++) {
                if (maze[i, j] == 1) {
                    Instantiate(wall, new Vector2(i, j), Quaternion.identity);
                    //Debug.Log("#"); // Muro
                }
                else {
                    Instantiate(floor, new Vector2(i, j), Quaternion.identity);
                    //Debug.Log(" "); // Camino
                }                    
            }
        }
    }

    public int[,] getMaze2D() {
        return maze;
    }
}