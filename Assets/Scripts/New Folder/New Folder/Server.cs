using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;

public class Server : MonoBehaviour
{
    private List<ServerClient> clients;
    private List<ServerClient> disconectList;

    private List<GameObject> ListofRooms;
    public GameObject RoomContainer;

    public int port = 6321;
    private TcpListener server;
    private bool serverStarted;
    private void Start()
    {
        //make a list with all the serverClient instances
        clients = new List<ServerClient>();
        disconectList = new List<ServerClient>();
        try{
            //gonna be listening to anyone that connects to the server
            // socket.bind(ipaddress,port) 
            server = new TcpListener(IPAddress.Any, port );
            // socket.Listen(1)
            server.Start();
            startListening();
            serverStarted = true;
            Debug.Log("server as been started on port " + port.ToString());
        }
        catch (Exception e){
            Debug.Log("Socket error: " + e.Message);
        }

    }
    
    void Update()
    {
        if(!serverStarted)
            return;
        
        foreach(ServerClient c in clients)
        {
            //is the client still connectd?
            if(!IsConnected(c.tcp))
            {
                Debug.Log("did it just disconnect me...!!!");
                c.tcp.Close();
                disconectList.Add(c);
                continue;
            }
            // check for messages from the client
            else
            {
                NetworkStream s = c.tcp.GetStream();
                
                if(s.DataAvailable)
                {
                    //he has data for us
                    StreamReader reader = new StreamReader(s, true);
                    
                    string data = reader.ReadLine();
                    Debug.Log("it's having trouble reading data how cute");
                    if(data != null)
                        Debug.Log("chat recieved at server side");
                        OnIncomingData(c,data);
                }
            }
        }
    }

    //lostening for client connection
    private void startListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient,server);
    }
    private bool IsConnected(TcpClient c)
    {
        try{
            if(c != null && c.Client != null && c.Client.Connected)
            {
                if(c.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }
                return true;
            }
            else
                return false;
        }
        catch{
            return false;
        }
    }
    //information for the coming client
    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;
        // the incoming client gets added to the server client list
        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        startListening();
        //sending a message for connection

        //
        // SendMessage a message, to everyone, say someone has connected

        Broadcast("PJin" + clients[clients.Count-1].ClientName + "joined #in room" + clients.Count.ToString(),clients) ;
    }
    private void OnIncomingData(ServerClient c, string data)
    {
        Debug.Log(data);
        Broadcast("CHAT" + c.ClientName + ":" + data,clients);
    }
    private void Broadcast(string data,List<ServerClient> cl)
    {
        foreach( ServerClient c in cl)
        {
            try{
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                Debug.Log("actully sending to clients");
                writer.WriteLine(data);
                writer.Flush();
            }
            catch(Exception e)
            {
                Debug.Log("write error: " + e.Message + " to client " + c.ClientName);
            }
        }
    }
}


public class ServerClient
{
    public TcpClient tcp;
    public string ClientName;
    //
    public ServerClient(TcpClient clientSocket)
    {
        ClientName = "Guest";
        tcp = clientSocket;
    }
}
