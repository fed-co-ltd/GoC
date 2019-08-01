using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    // Start is called before the first frame update
    int tilesNumber;
    
    void Awake()
    {
        tilesNumber = ComputeNumTiles();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int ComputeNumTiles(){
        switch (GameManager.numPlayers)
        {
            case 4:
                return 324;
            case 3:
                return 256;
            default:
                return 121;
        }
    }
}
