using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

using static MazeGenerator;

public class AStar : MonoBehaviour {
    //public Material pathMaterial, startMaterial, goalMaterial, solutionMaterial;
    //private Vector2 startNode, goalNode;
    public Material solutionMaterial;
    public Material floorMaterial;

    [SerializeField] MazeGenerator mazeGenerator;
    [SerializeField] SelectedManager selectedManager;
    private int[,] maze2D;
    private GameObject[,] mazeObj2D;
    //private GameObject[,] mazeObjMap;
    //private bool isDone;

    private void Update() {
        //HandleInput();
    }

    /*private void HandleInput() {
        if (Input.GetMouseButtonDown(0)) {
            if (isDone) ClearPath();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("Path")) {
                    int x = Mathf.RoundToInt(hitObject.transform.position.x);
                    int y = Mathf.RoundToInt(hitObject.transform.position.z);

                    if (startNode == default(Vector2)) {
                        startNode = new Vector2(x, y);
                        hitObject.GetComponent<Renderer>().material = startMaterial;
                    }
                    else if (goalNode == default(Vector2)) {
                        goalNode = new Vector2(x, y);

                        mazeObj2D = MazeGenerator.maze;
                        mazeObjMap = MazeGenerator.mazeObj;
                        List<Vector2> path = AStarSearch(startNode, goalNode);

                        foreach (Vector2 node in path)
                            mazeObjMap[node.x, node.y].GetComponent<Renderer>().material = solutionMaterial;
                        hitObject.GetComponent<Renderer>().material = goalMaterial;
                        startNode = default(Vector2);
                        goalNode = default(Vector2);
                        isDone = true;
                    }
                }
            }
        }
    }
    */

    public void findPath() {
        maze2D = mazeGenerator.getMaze2D();
        mazeObj2D = mazeGenerator.getCube3DFloor();

        List<Cube> listSelectedCubes = selectedManager.getSelectedCubes();

        Vector3 startCube = listSelectedCubes[0].transform.position;
        Vector3 endCube = listSelectedCubes[1].transform.position;

        Vector2 startCube2D = new Vector2(startCube.x, startCube.z);
        Vector2 endCube2D = new Vector2(startCube.x, startCube.z);

        //Calcula el path
        List<Vector2> path = AStarSearch(startCube2D, endCube2D);

        foreach (Vector2 node in path)
            mazeObj2D[Mathf.RoundToInt(node.x), Mathf.RoundToInt(node.y)].GetComponent<Renderer>().material = solutionMaterial;
    }

    /*void ClearPath() {
        for (int i = 0; i < mazeObj2D.GetLength(0); i++)
            for (int j = 0; j < mazeObj2D.GetLength(1); j++)
                if (mazeObj2D[i, j].CompareTag("Path"))
                    mazeObj2D[i, j].GetComponent<Renderer>().material = floorMaterial;
        isDone = false;
    }*/

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