using UnityEngine;

public class CubeOptimizer : MonoBehaviour {
    void Start() {
        // Desactiva su collider y meshRenderer, si tiene otro objeto encima
        if (Physics.Raycast(transform.position, transform.up)) {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
