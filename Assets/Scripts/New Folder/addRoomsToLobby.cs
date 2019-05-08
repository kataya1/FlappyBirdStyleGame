using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addRoomsToLobby : MonoBehaviour
{
    public GameObject RoomTemplate;

    public GameObject content;

    public void AddRoomClick()
    {
        var copy = Instantiate(RoomTemplate);
        copy.transform.parent = content.transform;
    }
}
