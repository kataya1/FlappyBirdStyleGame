using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum ServerState
{
    
    Playing,
    halted,
    not_playing
}

public static class ServerStateManager
{
    public static ServerState ServerState { get; set; }

    static ServerStateManager ()
    {
        ServerState = ServerState.Playing;
    }



}