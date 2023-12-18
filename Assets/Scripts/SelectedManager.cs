using UnityEngine;
using static Cube;

public class SelectedManager : MonoBehaviour {

    [SerializeField] int contSelected;

    //SUSCRIPCIÓN al EVENTO
    void OnEnable() {
        Cube.onCubeSelected += OnCubeSelected;
    }
    //DESUSCRIPCIÓN al EVENTO
    void OnDisable() {
        Cube.onCubeDeselected -= OnCubeDeselected;
    }


    private void OnCubeSelected(Cube cube, Vector3 position) {
        if (!cube.isSelected && contSelected < 2) {
            // Cambia el color según si es el 1º o 2º cubo
            cube.setColor(contSelected == 0 ? Color.green : Color.blue);
            cube.isSelected = true; // Marca el cubo como seleccionado
            contSelected++;
        }
    }
    private void OnCubeDeselected(Cube cube, Vector3 position) {
        cube.setColor(cube.startColor); // Reinicia su color
        cube.isSelected = false;    // Marca el cubo como deseleccionado
        contSelected--;
    }
}
