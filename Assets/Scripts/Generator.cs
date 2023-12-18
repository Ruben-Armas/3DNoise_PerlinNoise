using UnityEngine;
using UnityEngine.UIElements;

public class Generator : MonoBehaviour {
    public GameObject cube;
    public GameObject cube_2;
    public int width, heigth, large;
    public float detail;
    public int seed;

    void Start() {
        seed = Random.Range(100000, 900000); // Aleatoriedad
        GenerateMap();
    }

    /*public void GenerateMap() {
        for (int x=0; x < width; x++) {
            for (int z=0; z < large; z++) {
                // PerlinNoise
                heigth = (int)(Mathf.PerlinNoise((x / 2 + seed) / detail, (z / 2 + seed) / detail) * detail);
                for (int y=0; y < heigth; y++) {
                    Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
                }
            }
        }
    }*/
    public void GenerateMap() {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < large; z++) {
                // PerlinNoise
                int terrainHeight = (int)(Mathf.PerlinNoise((x / 2 + seed) / detail, (z / 2 + seed) / detail) * detail);

                // Pone un limite al valor del PerlinNoise
                if (terrainHeight >= large)
                    terrainHeight = large;

                // Generar cubos desde y = 0 hasta terrainHeight
                for (int y = 0; y <= terrainHeight; y++) {
                    if (y == terrainHeight)
                        Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
                    else 
                        Instantiate(cube_2, new Vector3(x, y, z), Quaternion.identity);
                    //Debug.Log(x + "," + y + "," + z);
                }
            }
        }
    }
}