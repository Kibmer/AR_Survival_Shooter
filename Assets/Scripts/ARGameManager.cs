using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARGameManager : MonoBehaviour
{
    public GameObject p_player;
    public Transform targetImg;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate(p_player, targetImg);
        PhotonNetwork.Instantiate("MyPlayer", Vector3.zero, Quaternion.identity, 0);
        // _player.transform.parent = targetImg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
