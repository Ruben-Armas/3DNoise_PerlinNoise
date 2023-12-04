using UnityEngine;

public class Generator : MonoBehaviour {
    public GameObject cube;
    public int width, heigth, large;
    public float detail;
    public int seed;

    void Start() {
        seed = Random.Range(100000, 900000); // Aleatoriedad
        GenerateMap();
    }

    public void GenerateMap() {
        for (int x=0; x < width; x++) {
            for (int z=0; z < large; z++) {
                // PerlinNoise
                heigth = (int)(Mathf.PerlinNoise((x / 2 + seed) / detail, (z / 2 + seed) / detail) * detail);
                for (int y=0; y < heigth; y++) {
                    Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
                }
            }
        }
    }
}
