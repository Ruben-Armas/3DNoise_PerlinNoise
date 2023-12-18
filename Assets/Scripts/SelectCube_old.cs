using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCube_old : MonoBehaviour {
    [SerializeField] bool isSelected = false;

    private static SelectCube_old firstCube; // Almacena el primer cubo seleccionado

    private Renderer rend;
    [SerializeField] Color startColor;

    void Start() {
        rend = GetComponent<Renderer>();
    }

    void OnMouseDown() {
        // Manejar la selección/deselección al hacer clic
        if (!isSelected) {
            startColor = rend.material.color;
            selectCube();
        }
        else {
            deselectCube();
        }
    }

    void selectCube() {
        // Cambiar el color y establecer como seleccionado
        rend.material.color = firstCube == null ? Color.green : Color.blue;
        isSelected = true;
        firstCube = this;
    }

    void deselectCube() {
        // Cambiar el color y establecer como no seleccionado
        rend.material.color = startColor;
        isSelected = false;

        // Resetear el primer cubo si este es el que está siendo deseleccionado
        if (firstCube == this) {
            firstCube = null;
        }
    }
}
