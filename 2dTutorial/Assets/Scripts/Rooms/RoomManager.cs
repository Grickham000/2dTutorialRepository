
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject roomToRespawn;

    public GameObject getRoom()
    {

    return roomToRespawn; 
    }

}
