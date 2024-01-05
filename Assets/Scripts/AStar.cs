using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

using static MazeGenerator;

public class AStar : MonoBehaviour {
    public Material solutionMaterial;
    public Material floorMaterial;

    [SerializeField] MazeGenerator mazeGenerator;
    [SerializeField] SelectedManager selectedManager;
    private int[,] maze2D;
    private GameObject[,] mazeObj2D;
    private bool pathDone = false;

    public void FindPath() {
        maze2D = mazeGenerator.GetMaze2D();
        mazeObj2D = mazeGenerator.GetCube3DFloor();

        List<Cube> listSelectedCubes = selectedManager.getSelectedCubes();

        Vector3 startCube = listSelectedCubes[0].transform.position;
        Vector3 endCube = listSelectedCubes[1].transform.position;

        Vector2 startCube2D = new Vector2(startCube.x, startCube.z);
        Vector2 endCube2D = new Vector2(endCube.x, endCube.z);

        // Borra el camino anterior
        if (pathDone) ClearPath();

        //Calcula el camino
        List<Vector2> path = AStarSearch(startCube2D, endCube2D);

        // Pinta el nuevo camino
        if(path.Count > 0) {
            foreach (Vector2 node in path)
                mazeObj2D[Mathf.RoundToInt(node.x), Mathf.RoundToInt(node.y)].GetComponent<Renderer>().material = solutionMaterial;
            pathDone = true;
        }
    }

    void ClearPath() {
        for (int i = 0; i < mazeObj2D.GetLength(0); i++)
            for (int j = 0; j < mazeObj2D.GetLength(1); j++) {
                string etiqueta = mazeObj2D[i, j].tag;
                //Debug.Log("(" + i + ", " + j + ")" + mazeObj2D[i, j].GetComponent<Renderer>().material.color + "tag " + etiqueta);

                if (!string.IsNullOrEmpty(etiqueta) && etiqueta.Equals("Floor")) {
                    mazeObj2D[i, j].GetComponent<Renderer>().material = floorMaterial;
                }
            }
        pathDone = false;
    }

    List<Vector2> AStarSearch(Vector2 start, Vector2 end) {
        Queue<Vector2> queue = new Queue<Vector2>();
        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();

        queue.Enqueue(start);
        cameFrom[start] = start;

        while (queue.Count > 0) {
            Vector2 current = queue.Dequeue();

            if (current == end) break;

            foreach (var neighbor in GetNeighbors(current)) {
                if (!cameFrom.ContainsKey(neighbor)) {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        List<Vector2> path = new List<Vector2>();
        Vector2 currentPos = end;

        while (currentPos != start) {
            path.Add(currentPos);
            currentPos = cameFrom[currentPos];
        }

        path.Reverse();
        path.RemoveAt(path.Count - 1);
        return path;
    }

    List<Vector2> GetNeighbors(Vector2 position) {
        List<Vector2> neighbors = new List<Vector2>();

        Vector2[] possibleNeighbors = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
        foreach (var offset in possibleNeighbors) {
            Vector2 neighbor = position + offset;

            if (neighbor.x >= 0 && neighbor.x < mazeObj2D.GetLength(0) && neighbor.y >= 0 && neighbor.y < mazeObj2D.GetLength(1) &&
                maze2D[Mathf.RoundToInt(neighbor.x), Mathf.RoundToInt(neighbor.y)] == 0) {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }
}