using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

public class Test : MonoBehaviour
{
    [SerializeField]
    int height = 5;
    [SerializeField]
    int width = 8;
    [SerializeField]
    float cellSizeX = 10;
    [SerializeField]
    float cellSizeY = 10;
    [SerializeField]
    bool showDebug = true;
    [SerializeField] 
    int aiToGenerate = 10;

    [SerializeField]
    GameObject aiPrefab;

    private GridCore<PNode> gridCore;

    // Start is called before the first frame update
    void Start()
    {
        gridCore = new GridCore<PNode>(width, height, cellSizeX, (GridCore<PNode> g, int x, int y, Vector3 position, float width, float height) => new PNode(g, x, y, position, width, height));
        gridCore.DebugText = showDebug;

        float spawnRadius = (width > height) ? height : width;

        ObjectSpawnerHelper objectSpawner = new ObjectSpawnerHelper();

        foreach (var item in objectSpawner.SpawnObjects(aiPrefab, aiToGenerate, Vector2.zero, new Vector2(width * (cellSizeX / 2), height * (cellSizeX / 2)), Quaternion.identity, SpawnContainerType.Cube))
        {
            Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            item.GetComponent<AIController>().Initialize(gridCore, color);
        }
        

        //for (int i = 0; i < aiToGenerate; i++)
        //{
        //    Vector3 position = Random.insideUnitCircle * new Vector2 (width * (cellSizeX / 2), height * (cellSizeX / 2));
        //    Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //    Instantiate(aiPrefab, position, Quaternion.identity).GetComponent<AIController>().Initialize(gridCore, color);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public interface ICANHASGIZMO
{

}