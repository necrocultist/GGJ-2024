using System.Collections.Generic;
using UnityEngine;

namespace DunGen
{
    public class GridController : MonoBehaviour
    {
        public Room room;
    
        [System.Serializable]
    
        public struct Grid
        {
            public int columns, rows;
            public float verticalOffset, horizontalOffset;
        }

        public Grid grid;
        public GameObject gridTile;
        public List<Vector2> availablePoints = new List<Vector2>();

        void Awake()
        {
            room = GetComponentInParent<Room>();
            grid.columns = room.width - 2;
            grid.rows = room.height - 2;
            GenerateGrid();
        }

        public void GenerateGrid()
        {
            grid.verticalOffset += room.transform.localPosition.y;
            grid.horizontalOffset += room.transform.localPosition.x;

            for (int y = 1; y < grid.rows - 1; y++)
            {
                for (int x = 1; x < grid.columns - 1; x++)
                {
                    GameObject go = Instantiate(gridTile, transform);
                    go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset),
                        y - (grid.rows - grid.verticalOffset));
                    go.name = "X: " + x + ", Y: " + y;
                    availablePoints.Add((go.transform.position));
                    go.SetActive(false);
                }
            }
            GetComponentInParent<ObjectRoomSpawner>().InitializeObjectSpawning();
        }
    }
}
