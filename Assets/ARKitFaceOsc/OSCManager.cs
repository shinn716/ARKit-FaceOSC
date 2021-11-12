using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OSCManager : MonoBehaviour
{
    public static OSCManager instance;

    private enum Type
    {
        Lintener,
        Sender,
        Both
    }

    [SerializeField]
    Type type = Type.Lintener;

    [Header("Listener"), SerializeField]
    int listenerPort = 12001;

    [Header("Sender"), SerializeField]
    string targetIp = "127.0.0.1";
    [SerializeField]
    int targetPort = 12000;

    public int ListenerPort
    {
        get => listenerPort;
        set => listenerPort = value;
    }
    public string TargetIp
    {
        get => targetIp;
        set => targetIp = value;
    }
    public string TargetPort
    {
        get => targetPort.ToString();
        set => targetPort = int.Parse(value);
    }
    public OSC CurrentOsc
    {
        get;
        set;
    }

    private GameObject go_osc = null;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateOSC();
    }

    [ContextMenu("CreateOSC")]
    public void CreateOSC()
    {
        if (go_osc == null)
        {
            var tOSC = Resources.Load<GameObject>("osc").GetComponent<OSC>();
            switch (type)
            {
                default:
                    tOSC.inPort = listenerPort;
                    tOSC.outIP = targetIp;
                    tOSC.outPort = targetPort;
                    break;
                case Type.Lintener:
                    tOSC.inPort = listenerPort; 
                    break;
                case Type.Sender:
                    tOSC.outIP = targetIp;
                    tOSC.outPort = targetPort;
                    break;
            }

            go_osc = Instantiate(Resources.Load<GameObject>("osc"));
            go_osc.transform.SetParent(transform);
            CurrentOsc = go_osc.GetComponent<OSC>();
        }
    }

    [ContextMenu("DestroyOSC")]
    public void DestroyOSC()
    {
        CurrentOsc.Close();
        Destroy(go_osc);
        go_osc = null;
    }
}
