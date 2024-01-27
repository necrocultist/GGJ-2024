using System;
using System.Collections.Generic;
using UnityEngine;

namespace DunGen
{
    public class Room : MonoBehaviour
    {
        [SerializeField]
        public int width;
        public int height;
        public int x;
        public int y;

        public Room(int X, int Y)
        {
            x = X;
            y = Y;
        }

        private bool updatedDoors = false;
        public Door leftDoor;
        public Door rightDoor;
        public Door upDoor;
        public Door downDoor;

        public List<Door> doors = new List<Door>();
        
        void Start()
        {
            // if(RoomControl.instance == null)
            // {
            //     Debug.Log("Start at wrong room");
            // }

            Door[] ds = GetComponentsInChildren<Door>();
            foreach (Door d in ds)
            {
                doors.Add(d);
                switch (d.doorType)
                {
                    case Door.DoorType.right:
                        rightDoor = d;
                        break;
                    case Door.DoorType.left:
                        leftDoor = d;
                        break;
                    case Door.DoorType.up:
                        upDoor = d;
                        break;
                    case Door.DoorType.down:
                        downDoor = d;
                        break;
                }
            }
            RoomControl.instance.RegisterRoom(this);
        }

        public void RemoveUnconnectedDoors()
        {
            foreach (Door door in doors)
            {
                switch (door.doorType)
                {
                    case Door.DoorType.right:
                        if(GetRight()==null)
                            door.gameObject.SetActive(false);
                        break;
                    case Door.DoorType.left:
                        if(GetLeft()==null)
                            door.gameObject.SetActive(false);  
                        break;
                    case Door.DoorType.up:
                        if(GetUp()==null)
                            door.gameObject.SetActive(false);                        break;
                        break;
                    case Door.DoorType.down:
                        if(GetDown()==null)
                            door.gameObject.SetActive(false);                        break;
                        break;
                }
            }
        }

        void Update()
        {
            if (name.Contains("Boss") && !updatedDoors)
            {
                RemoveUnconnectedDoors();
                updatedDoors = true;
            }
        }

        public Room GetRight()
        {
            if (RoomControl.instance.DoesRoomExist(x + 1, y))
            {
                return RoomControl.instance.FindRoom(x+1,y);
            }

            return null;
        }
        public Room GetLeft()
        {
            if (RoomControl.instance.DoesRoomExist(x - 1, y))
            {
                return RoomControl.instance.FindRoom(x-1,y);
            }

            return null;
        }
        public Room GetUp()
        {
            if (RoomControl.instance.DoesRoomExist(x , y+1))
            {
                return RoomControl.instance.FindRoom(x,y+1);
            }

            return null;
        }
        public Room GetDown()
        {
            if (RoomControl.instance.DoesRoomExist(x, y-1))
            {
                return RoomControl.instance.FindRoom(x,y-1);
            }

            return null;
        }
        void OnDrawGizmos(){
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
        }

        public Vector3 GetRoomCenter(){
            return new Vector3(x*width,y*height);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("baban");
                RoomControl.instance.OnPlayerEnterRoom(this);
            }
        }
    }
}
