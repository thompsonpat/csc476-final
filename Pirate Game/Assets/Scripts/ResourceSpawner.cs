using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject woodPrefab;
    public GameObject crewPrefab;

    public Sprite[] crewImages = new Sprite[6];

    public List<Vector3> usableTiles;
    private Grid grid;

    void Start()
    {
        usableTiles = TileLocationsInTileMap("Tilemap_Spawnable");
        grid = GameObject.Find("Grid").GetComponent<Grid>();

        for (int i = 0; i < 50; i ++)
        {
            SpawnWood();
            SpawnCrew();
            SpawnEnemy();
        }
        
        InvokeRepeating("SpawnWood", 5.0f, 15.0f);
        InvokeRepeating("SpawnCrew", 5.0f, 15.0f);
        InvokeRepeating("SpawnEnemy", 5.0f, 60.0f);
    }

    void Update()
    {

    }

    void SpawnEnemy()
    {   
        Vector3Int cellPosition = grid.WorldToCell(usableTiles[Random.Range(0, usableTiles.Count)]);

        // Create new enemy
        var newEnemy = Instantiate(enemyPrefab, grid.GetCellCenterWorld(cellPosition), Quaternion.identity);

        // Rotate new enemy
        newEnemy.transform.Rotate(Vector3.forward * Random.Range(0, 360));
    }

    void SpawnWood()
    {   
        Vector3Int cellPosition = grid.WorldToCell(usableTiles[Random.Range(0, usableTiles.Count)]);

        // Create new piece of wood
        var newWood = Instantiate(woodPrefab, grid.GetCellCenterWorld(cellPosition), Quaternion.identity);

        // Rotate new piece of wood
        newWood.transform.Rotate(Vector3.forward * Random.Range(0, 360));
    }

    void SpawnCrew()
    {
        Vector3Int cellPosition = grid.WorldToCell(usableTiles[Random.Range(0, usableTiles.Count)]);
        
        // Create new crew
        var newCrew = Instantiate(crewPrefab, grid.GetCellCenterWorld(cellPosition), Quaternion.identity);

        // Rotate new crew
        newCrew.transform.Rotate(Vector3.forward * Random.Range(0, 360));

        // Select Random Crew Image
        newCrew.GetComponent<SpriteRenderer>().sprite = crewImages[Random.Range(0, crewImages.Length - 1)];
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
