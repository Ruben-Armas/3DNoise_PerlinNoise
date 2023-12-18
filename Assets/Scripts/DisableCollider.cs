using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollider : MonoBehaviour {
    void Start() {
        // Desactiva el collider, si tiene otro objeto encima
        if (Physics.Raycast(transform.position, transform.up)) {
            GetComponent<Collider>().enabled = false;
        }
    }
}
