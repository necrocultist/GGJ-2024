using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
        public List<Room> loadedRooms = new List<Room>();
        bool isLoadingRoom = false;
        void Awake(){
            instance = this;
        }
        public bool DoesRoomExist(int x, int y){
            return loadedRooms.Find(item=> item.x == x && item.y == y)!= null;
        }

        public void LoadRoom(string name, int x, int y)
        {
            Debug.Log("ana");
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

        public void RegisterRoom(Room room)
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
            loadedRooms.Add(room);
        }

        void UpdateRoomQueue()
        {
            if (isLoadingRoom)
            {
                return;
            }

            if (loadRoomQueue.Count == 0)
            {
                return;
            }

            currentLoadRoomData = loadRoomQueue.Dequeue();
            isLoadingRoom = true;
            StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
        }
    
    

        void Start()
        {       
            LoadRoom("Start",0,0);
            LoadRoom("Empty",1,0);
            LoadRoom("Empty",2,0);
            LoadRoom("Empty",1,1);

        }
    
    


        void Update()
        {
            UpdateRoomQueue();
        }
    }
}