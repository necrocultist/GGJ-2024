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

        // Start is called before the first frame update
        void Start()
        {
            if(RoomControl.instance == null)
            {
                Debug.Log("Start at wrong room");
            }
            
            RoomControl.instance.RegisterRoom(this);
        }

        void OnDrawGizmos(){
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
        }

        public Vector3 GetRoomCenter(){
            return new Vector3(x*width,y*height);
        }


    }
}
