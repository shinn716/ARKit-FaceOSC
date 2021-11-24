using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCListener : MonoBehaviour
{
    public Transform head;
    public SkinnedMeshRenderer targetSkinnedMeshRenderer;
    public BlendshapesMapper blendshapesMapper;

    OSC osc = null;
    Dictionary<string, int> blendshapesList { get; set; } = new Dictionary<string, int>();

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        blendshapesMapper.Init();
        blendshapesList = shinn.AR.Utils.GetBlendShapeNamesReturnDict(targetSkinnedMeshRenderer);

        yield return null;
        osc = OSCManager.instance.CurrentOsc;

        //foreach (var i in blendshapesMapper.dict)
        //    print(i.Key + "  " + i.Value);

        //foreach (var i in blendshapesList)
        //    print(i.Key + "  " + i.Value);

        osc.SetAllMessageHandler(OnReceive);
        osc.SetAddressHandler("/transform", OnReceiveTransform);
        osc.SetAddressHandler("/test", OnReceiveTest);
        osc.SetAddressHandler("/avatarreset", OnReceiveAvatarReset);

        qorg = head.localRotation;

        qbase = qremote;
        calib = true;
    }

    private void OnReceive(OscMessage message)
    {
        for (int i = 0; i < blendshapesMapper.contents.Count; i++)
        {
            if (message.address == "/" + blendshapesMapper.contents[i].blendshapesName.ToString())
            {
                string[] sliceName = message.address.Split('/');
                int index = shinn.AR.Utils.GetBlendShapeByIndex(blendshapesMapper.dict[sliceName[1]], blendshapesList);
                if (index != -1)
                {
                    float value = message.GetInt(0);
                    targetSkinnedMeshRenderer.SetBlendShapeWeight(index, value);
                }
            }
        }
    }

    private void OnReceiveTransform(OscMessage message)
    {
        Vector3 pos = new Vector3(message.GetFloat(0), message.GetFloat(1), message.GetFloat(2));
        Quaternion rot = new Quaternion(message.GetFloat(3), message.GetFloat(4), message.GetFloat(5), message.GetFloat(6));
        //head.rotation = rot;

        if (!calib)
            return;

        qremote = rot;
        qpose = Quaternion.Inverse(qbase) * qremote;

        Quaternion qtmp = qpose * qorg;
        Quaternion qfin = new Quaternion(qtmp.x, qtmp.y, qtmp.z, qtmp.w);
        head.localRotation = qfin;
    }

    public void OnReceiveAvatarReset(OscMessage message)
    {
        qbase = qremote;
        calib = true;
    }

    private void OnReceiveTest(OscMessage message)
    {
        print(message);
    }


    Quaternion qorg = Quaternion.identity;
    Quaternion qpose = Quaternion.identity;
    Quaternion qbase = Quaternion.identity;
    Quaternion qremote = Quaternion.identity;
    bool calib = false;
}
