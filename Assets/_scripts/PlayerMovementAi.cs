using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Fusion;

public class PlayerMovement : NetworkBehaviour
{
    // The Tilemap that contains the hexagonal tiles
    public Tilemap tilemap;

    // The maximum number of moves per turn
    public int maxMoves = 3;

    // The current number of moves left
    private int movesLeft;

    // The hexagonal tile that the player is currently on
    private Vector3Int currentTile;

    // The hexagonal tiles that the player can move to
    private List<Vector3Int> reachableTiles;

    // The color of the reachable tiles
    public Color reachableColor = Color.green;

    // The color of the unreachable tiles
    public Color unreachableColor = Color.red;

    // The color of the current tile
    public Color currentColor = Color.blue;

    // The original colors of the tiles
    private Dictionary<Vector3Int, Color> originalColors;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the moves left to the max moves
        movesLeft = maxMoves;

        // Initialize the reachable tiles list
        reachableTiles = new List<Vector3Int>();

        // Initialize the original colors dictionary
        originalColors = new Dictionary<Vector3Int, Color>();

        // Find the tile that the player is on
        currentTile = tilemap.WorldToCell(transform.position);

        // Find the reachable tiles
        FindReachableTiles();

        // Color the tiles
        ColorTiles();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if it is the local player's turn
        if (IsLocalPlayerTurn())
        {
            // Check if the player has any moves left
            if (movesLeft > 0)
            {
                // Check if the player clicks the mouse
                if (Input.GetMouseButtonDown(0))
                {
                    // Get the mouse position in world coordinates
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    // Get the tile that the mouse is on
                    Vector3Int clickedTile = tilemap.WorldToCell(mousePosition);

                    // Check if the clicked tile is reachable
                    if (reachableTiles.Contains(clickedTile))
                    {
                        // Move the player to the clicked tile
                        MoveToTile(clickedTile);

                        // Decrement the moves left
                        movesLeft--;

                        // Update the current tile
                        currentTile = clickedTile;

                        // Find the new reachable tiles
                        FindReachableTiles();

                        // Color the tiles
                        ColorTiles();
                    }
                }
            }
        }
    }

    // Move the player to the given tile
    void MoveToTile(Vector3Int tile)
    {
        // Get the world position of the tile
        Vector3 tilePosition = tilemap.GetCellCenterWorld(tile);

        // Set the player's position to the tile position
        transform.position = tilePosition;
    }

    // Find the reachable tiles using BFS
    void FindReachableTiles()
    {
        // Clear the previous reachable tiles
        reachableTiles.Clear();

        // Create a queue for BFS
        Queue<Vector3Int> queue = new Queue<Vector3Int>();

        // Create a set to store the visited tiles
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

        // Enqueue the current tile and mark it as visited
        queue.Enqueue(currentTile);
        visited.Add(currentTile);

        // Loop until the queue is empty or the moves left is zero
        while (queue.Count > 0 && movesLeft > 0)
        {
            // Dequeue a tile from the queue
            Vector3Int tile = queue.Dequeue();

            // Get the neighbors of the tile
            List<Vector3Int> neighbors = GetNeighbors(tile);

            // Loop through the neighbors
            foreach (Vector3Int neighbor in neighbors)
            {
                // Check if the neighbor is valid and not visited
                if (IsValidTile(neighbor) && !visited.Contains(neighbor))
                {
                    // Enqueue the neighbor and mark it as visited
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);

                    // Add the neighbor to the reachable tiles
                    reachableTiles.Add(neighbor);
                }
            }

            // Decrement the moves left
            movesLeft--;
        }

        // Reset the moves left to the max moves
        movesLeft = maxMoves;
    }

    // Get the neighbors of a given tile
    List<Vector3Int> GetNeighbors(Vector3Int tile)
    {
        // Create a list to store the neighbors
        List<Vector3Int> neighbors
