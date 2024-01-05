using System.Collections.Generic;
using UnityEngine;
using static Cube;

public class SelectedManager : MonoBehaviour {
    [ColorUsage(true, true)]
    [SerializeField] Color startColor = Color.green; // Utiliza ColorUsageAttribute para mostrar un selector de color en el Inspector
    [ColorUsage(true, true)]
    [SerializeField] Color endColor = Color.cyan;

    [SerializeField] List<Cube> selectedCubes = new List<Cube>();

    //SUSCRIPCIÓN al EVENTO
    void OnEnable() {
        Cube.onCubeSelected += OnCubeSelected;
        Cube.onCubeDeselected += OnCubeDeselected;
    }
    //DESUSCRIPCIÓN al EVENTO
    void OnDisable() {
        Cube.onCubeSelected -= OnCubeSelected;
        Cube.onCubeDeselected -= OnCubeDeselected;
    }


    private void OnCubeSelected(Cube cube, Vector3 position) {
        int selectedAmount = selectedCubes.Count;
        if (!cube.isSelected && selectedAmount < 2) {
            Debug.Log("Selected");
            // Cambia el color según si es el 1º o 2º cubo
            cube.setColor(selectedAmount == 0 ? startColor : endColor);
            cube.isSelected = true; // Marca el cubo como seleccionado

            // Verifica si el cubo ya está en la lista, si no, lo añade
            if (!selectedCubes.Contains(cube))
                selectedCubes.Add(cube);
        }
    }
    private void OnCubeDeselected(Cube cube, Vector3 position) {
        cube.setColor(cube.startColor); // Reinicia su color
        cube.isSelected = false;    // Marca el cubo como deseleccionado
        Debug.Log("Deselectd");

        // Verifica si el cubo ya está en la lista, si es así, lo borra
        if (selectedCubes.Contains(cube))
            selectedCubes.Remove(cube);
    }

    public List<Cube> getSelectedCubes() {
        return selectedCubes;
    }
}
