using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;


public class Client : MonoBehaviour
{
    //if we're ready to recieve messages and send message
    private bool socketReady = false;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;
    public GameObject playerscontainer;
    public GameObject chatcontainer;
    public GameObject messagePrefab;
    public string clientName;


    // connecting to a server by pressing a button
    public void ConnectToServer()
    {
        if(socketReady)
            return;
        //default host / port values
        string host = "127.0.0.1";
        int port = 6321;
        //overwriting default values
        // string h;
        // int p;
        // h = GameObject.Find("IPinput").GetComponent<InputField>().text;
        // if(h != "")
        //     host = h;
        // //fancy code
        // int.TryParse(GameObject.Find("Portinput").GetComponent<InputField>().text, out p);
        // if(p != 0)
        //     port = p;

        //creating a socket
        try
        {   //create new tcp client on this host and port
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true ;
            
        }
        catch(Exception e)
        {
            Debug.Log("socket error: " + e.Message);
        }
    }

    public void Update()
    {
        if(socketReady)
        {
            if(stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if(data != null)
                    OnIncomingData(data);
            }
        }
    }

    private void OnIncomingData(string data)
    {
        //checks if it's a chat or player
        Debug.Log(data.Substring(0,4));
        if ((data.Substring(0,4)).Equals("PJin"))
        {
            GameObject go = Instantiate(messagePrefab,playerscontainer.transform) as GameObject;
            go.GetComponentInChildren<Text>().text = data.Substring(4);
        }
        else if ((data.Substring(0,4)).Equals("CHAT"))
        {
            GameObject go = Instantiate(messagePrefab,chatcontainer.transform) as GameObject;
            go.GetComponentInChildren<Text>().text = data.Substring(4);
        }
            
    }
    private void Send(string data)
    {
        //check if it's connected
        if(!socketReady)
            {Debug.Log("why not ready?");
            return;}
        //writes tcp packet to server
        Debug.Log("socket ready");
        writer.WriteLine(data); 
        writer.Flush();
    }
    public void OnSendButton()
    {
        //takes input in chat input and turn it into a message

        try
        {
            string message = GameObject.Find("chatinput").GetComponent<InputField>().text;
            Debug.Log("problem is not here, message " + message);
            Send(message);
        }
        catch(Exception e)
        {
            Debug.Log("chatinput error: " + e.Message);
        }
       
    }

}
