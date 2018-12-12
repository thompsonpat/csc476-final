using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject woodPrefab;

    void Start()
    {
        InvokeRepeating("SpawnWood", 2.0f, 5.0f);
    }

    void Update()
    {

    }

    void SpawnWood()
    {
        List<Vector3> usableTiles = TileLocationsInTileMap("Tilemap_Spawnable");
        Grid grid = GameObject.Find("Grid").GetComponent<Grid>();
        Vector3Int cellPosition = grid.WorldToCell(usableTiles[Random.Range(0, usableTiles.Count)]);
        // Create new piece of wood
        var newWood = Instantiate(woodPrefab, grid.GetCellCenterWorld(cellPosition), Quaternion.identity);
        // Rotate new piece of wood
        newWood.transform.Rotate(Vector3.forward * Random.Range(0, 360));
    }

    public List<Vector3> TileLocationsInTileMap(string tilemapName)
    {
        Tilemap tileMap = GameObject.Find(tilemapName).GetComponent<Tilemap>();
        List<Vector3> tileWorldLocations = new List<Vector3>();

        foreach (var pos in tileMap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tileMap.CellToWorld(localPlace);
            if (tileMap.HasTile(localPlace))
            {
                tileWorldLocations.Add(place);
            }
        }

        return tileWorldLocations;
    }

    public List<int[]> Vect3ListToArray(List<Vector3> tileLocations)
    {
        List<int[]> newTileLocations = new List<int[]>();

        foreach (var vectorLocation in tileLocations)
        {
            int[] newArray = new int[3];
            newArray[0] = (int)vectorLocation.x;
            newArray[1] = (int)vectorLocation.y;
            newArray[2] = (int)vectorLocation.z;
            newTileLocations.Add(newArray);
        }

        return newTileLocations;
    }
}
