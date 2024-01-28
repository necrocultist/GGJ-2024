using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace DunGen
{
    public class RoomInfo{
        public string name;
        public int x;
        public int y;
    }


    public class RoomControl : MonoBehaviour
    {

        public static RoomControl instance;
        string currentWorldName = "FirstFloor";
        RoomInfo currentLoadRoomData;
        Room currRoom;
        Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
        public List<Room> loadedRooms = new List<Room>();
        bool isLoadingRoom = false;
        bool spawnedBossRoom = false;
        bool updatedRooms = false;
        void Awake(){
            instance = this;
        }
        public bool DoesRoomExist(int x, int y){
            return loadedRooms.Find(item=> item.x == x && item.y == y)!= null;
        }
        public Room FindRoom(int x, int y)
        {
            return loadedRooms.Find(item=> item.x == x && item.y == y);
        }

        public void OnPlayerEnterRoom(Room room)
        {
            CameraController.instance.currRoom = room;
            currRoom = room;
            UpdateRooms();
        }

        public void UpdateRooms()
        {
            foreach (Room room in loadedRooms)
            {
                if (currRoom != room)
                {
                    Enemy[] enemies = room.GetComponentsInChildren<Enemy>();
                    if (enemies != null)
                    {
                        foreach (Enemy enemy in enemies)
                        {
                            enemy.enemyMovement.notInRoom = true;
                        }

                        foreach (Door door in room.GetComponentsInChildren<Door>())
                        {
                            door.doorCollider.SetActive(false);
                        }
                    }
                    else
                    {
                        foreach (Door door in room.GetComponentsInChildren<Door>())
                        {
                            door.doorCollider.SetActive(false);
                        }
                    }
                }
                else
                {
                    Enemy[] enemies = room.GetComponentsInChildren<Enemy>();
                    if (enemies.Length > 0)
                    {
                        foreach (Enemy enemy in enemies)
                        {
                            enemy.enemyMovement.notInRoom = false;
                        }
                        foreach (Door door in room.GetComponentsInChildren<Door>())
                        {
                            door.doorCollider.SetActive(true);
                        }
                    }
                    else
                    {
                        foreach (Door door in room.GetComponentsInChildren<Door>())
                        {
                            door.doorCollider.SetActive(false);
                        }
                    }
                }
            }
            
        }

        public void LoadRoom(string name, int x, int y)
        {
            if(DoesRoomExist(x,y))
            {
                return;
            }
            RoomInfo newRoomData = new RoomInfo();
            newRoomData.name = name;
            newRoomData.x = x;
            newRoomData.y = y;
        
            loadRoomQueue.Enqueue(newRoomData);
        }

        IEnumerator LoadRoomRoutine(RoomInfo info)
        {
            string roomName = currentWorldName + info.name;
            AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
            while (loadRoom.isDone == false)
            {
                yield return null;
            }
        }

        public string GetRandomRoomName()
        {
            string[] possibleRooms = new string[]
            {
                "Set1"
            };
            return possibleRooms[Random.Range(0, possibleRooms.Length)];
        }

        public void RegisterRoom(Room room)
        {
            if (!DoesRoomExist(currentLoadRoomData.x, currentLoadRoomData.y))
            {
                room.transform.position = new Vector3
                (
                    currentLoadRoomData.x * room.width,
                    currentLoadRoomData.y * room.height,
                    0
                );
                room.x = currentLoadRoomData.x;
                room.y = currentLoadRoomData.y;
                room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.x + ", " + room.y;
                room.transform.parent = transform;
                isLoadingRoom = false;
                if (loadedRooms.Count == 0)
                {
                    CameraController.instance.currRoom = room;
                }

                loadedRooms.Add(room);
            }
            else
            {
                Destroy(room.gameObject);
                isLoadingRoom = false;
            }
        }

        void UpdateRoomQueue()
        {
            if (isLoadingRoom)
            {
                return;
            }

            if (loadRoomQueue.Count == 0)
            {
                if (!spawnedBossRoom)
                {
                    StartCoroutine(SpawnedBossRoom());
                }
                else if (spawnedBossRoom && !updatedRooms)
                {
                    foreach (Room room in loadedRooms)
                    {
                        room.RemoveUnconnectedDoors();
                    }
                    UpdateRooms();
                    updatedRooms = true;
                    
                }

                return;
            }

            
            
            currentLoadRoomData = loadRoomQueue.Dequeue();
            isLoadingRoom = true;
            StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
        }


        IEnumerator SpawnedBossRoom()
        {
            spawnedBossRoom = true;
            yield return new WaitForSeconds(0.5f);
            if (loadRoomQueue.Count == 0)
            {
                Room bossRoom = loadedRooms[loadedRooms.Count - 1];
                Room tempRoom = new Room(bossRoom.x, bossRoom.y);
                Destroy(bossRoom.gameObject);
                var roomToRemove = loadedRooms.Single(r => r.x == tempRoom.x && r.y == tempRoom.y);
                loadedRooms.Remove(roomToRemove);
                LoadRoom("Boss",tempRoom.x,tempRoom.y);
            }
        }


        void Update()
        {
            UpdateRoomQueue();
        }
    }
}