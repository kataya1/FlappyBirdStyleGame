using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomServer : MonoBehaviour
{
    private List<Rooms> roomslist;
    private Rooms newRoom;

    public void updateList()
    {
        int room = 1;
    }
}

public class Rooms
{
    private string RoomName;
    private string host;
    private string port;
    private int playersLimit;
    private int timelimit;

    public Rooms()
    {
    }
}
