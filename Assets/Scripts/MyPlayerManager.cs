using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerManager : MonoBehaviour
{
    public GameObject g_end;
    // Start is called before the first frame update
    void Start()
    {
        Transform targetImg = GameObject.FindGameObjectWithTag("PlayGround").transform;
        transform.parent = targetImg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    void Shooting(){
        g_end.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
    }
}
