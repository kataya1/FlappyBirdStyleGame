﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class networking : NetworkBehaviour
{
    public GameObject Playerunitprefab;
    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer == true)
        {
            Instantiate(Playerunitprefab);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
