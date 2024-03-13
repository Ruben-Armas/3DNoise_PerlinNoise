using UnityEngine;

public class Cube : MonoBehaviour {
    //EVENTO (DELEGADO)   --> Crear
    public delegate void cubeSelected(Cube cube, Vector3 position);
    public static event cubeSelected onCubeSelected;        //(EVENTO)

    //EVENTO (DELEGADO)   --> Crear
    public delegate void cubeDeselected(Cube cube, Vector3 position);
    public static event cubeDeselected onCubeDeselected;        //(EVENTO)


    private Renderer rend;
    public Color startColor;
    public bool isSelected = false;


    void Start() {
        rend = GetComponent<Renderer>();
    }

    void OnMouseDown() {
        // Manejar la selección/deselección al hacer click
        if (!isSelected) {
            //startColor = rend.material.color;
            selectCube();
        }
        else {
            deSelectCube();
        }
    }

    void selectCube() {
        //Evento Seleccionar
        if (onCubeSelected != null)
            onCubeSelected(this, transform.position);
    }
    void deSelectCube() {
        //Evento Seleccionar
        if (onCubeDeselected != null)
            onCubeDeselected(this, transform.position);
    }

    public void setColor(Color color) {
        // Cambiar el color y establecer como seleccionado
        rend.material.color = color;
    }
}