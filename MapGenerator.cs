using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class MapGenerator : MonoBehaviour  {
    public ColorToPrefab[] colorToPrefabArray;
    public Texture2D texture;
    private void Start() {
        GenerateMap(new Vector2(-12f,-12f),new Vector2(2f,2f));
    }
    public void GenerateMap(Texture2D map, Vector2 originPoint, Vector2 cellSize) {
        //Grid<Pathfinding.PathNode> grid = Pathfinding.Instance.GetGrid();
        for(int x =0; x < map.width; x++) {
            for(int y =0; y< map.height; y++) {
                Color pixel = map.GetPixel(x,y);
                Vector3 position = originPoint + Vector2.one *(cellSize/2f)  + new Vector2(cellSize.x*x,cellSize.y*y);
                foreach(ColorToPrefab ctp in colorToPrefabArray) {
                    if(ctp.color == pixel) {
                        UtilFunc.Instantiate(new SpawnInfo<Transform>(ctp.gameOject.transform, position, Quaternion.identity));
                        //grid.GetGridObject(x, y).IsObstacle = true;
                    }
                }
                
            }
        }
    }
    public void GenerateMap(Vector2 originPoint, Vector2 cellSize) {
        GenerateMap(texture, originPoint, cellSize);
    }

}
