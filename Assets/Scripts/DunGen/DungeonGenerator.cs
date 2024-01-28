using System;
using System.Collections;
using System.Collections.Generic;
using DunGen;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DunguenCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomControl.instance.LoadRoom("Start",0,0);
        foreach (Vector2Int roomLocation in rooms)
        {
                RoomControl.instance.LoadRoom(RoomControl.instance.GetRandomRoomName(),roomLocation.x,roomLocation.y);
        }
    }
}
