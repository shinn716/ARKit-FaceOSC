using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receiver : MonoBehaviour
{
    public OSC osc;

    // Start is called before the first frame update
    void Start()
    {
        osc.SetAddressHandler("/test", OnReceiveTest);
        //osc.SetAddressHandler("/CubeXYZ", OnReceiveXYZ);
        //osc.SetAddressHandler("/CubeX", OnReceiveX);
        //osc.SetAddressHandler("/CubeY", OnReceiveY);
        //osc.SetAddressHandler("/CubeZ", OnReceiveZ);
    }

    void OnReceiveTest(OscMessage message)
    {
        //float x = message.GetInt(0);
        //float y = message.GetInt(1);
        //float z = message.GetInt(2);

        print(message);
    }


    [ContextMenu("Test")]
    public void Test()
    {
        OscMessage message = new OscMessage();
        message.address = "/test";
        message.values.Add(456);
        osc.Send(message);
    }

    //void OnReceiveX(OscMessage message)
    //{
    //    float x = message.GetFloat(0);

    //    Vector3 position = transform.position;

    //    position.x = x;

    //    transform.position = position;
    //}

    //void OnReceiveY(OscMessage message)
    //{
    //    float y = message.GetFloat(0);

    //    Vector3 position = transform.position;

    //    position.y = y;

    //    transform.position = position;
    //}

    //void OnReceiveZ(OscMessage message)
    //{
    //    float z = message.GetFloat(0);

    //    Vector3 position = transform.position;

    //    position.z = z;

    //    transform.position = position;
    //}

}
