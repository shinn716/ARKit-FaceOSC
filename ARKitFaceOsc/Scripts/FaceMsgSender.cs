using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(FaceBlendShapeInfo))]
public class FaceMsgSender : MonoBehaviour
{
    [SerializeField]
    FaceBlendShapeInfo faceBlendShapeInfo;


    bool flag = false;

    private void Start()
    {
        print("[SystemInfo.deviceModel] " + SystemInfo.deviceModel);
        InvokeRepeating("WDT", 1, 5);
    }

    private void OnDisable() 
    {
        if (faceBlendShapeInfo.CurrentARFace != null)
            faceBlendShapeInfo.CurrentARFace.updated -= OnUpdated;
    }

    private void Update()
    {
        if (faceBlendShapeInfo.CurrentARFace != null)
        {
            if (!flag)
            {
                flag = true;
                faceBlendShapeInfo.CurrentARFace.updated += OnUpdated;
            }
            else
                return;
        }
    }

    void OnUpdated(ARFaceUpdatedEventArgs eventArgs)
    {
        UpdateFaceFeatures();
    }

    void UpdateFaceFeatures()
    {
        if (faceBlendShapeInfo.GetSkinnedMeshRenderer == null || !faceBlendShapeInfo.GetSkinnedMeshRenderer.enabled || faceBlendShapeInfo.GetSkinnedMeshRenderer.sharedMesh == null)
            return;

        // OSC
        foreach (var i in faceBlendShapeInfo.m_FaceArkitBlendShapeIndexMap)
        {
            OscMessage message = new OscMessage();
            message.address = "/" + i.Key;
            message.values.Add(i.Value);
            OSCManager.instance.CurrentOsc.Send(message);
        }

        OscMessage message_transform = new OscMessage();
        message_transform.address = "/transform";
        message_transform.values.Add(faceBlendShapeInfo.CurrentARFace.gameObject.transform.position.x);
        message_transform.values.Add(faceBlendShapeInfo.CurrentARFace.gameObject.transform.position.y);
        message_transform.values.Add(faceBlendShapeInfo.CurrentARFace.gameObject.transform.position.z);
        message_transform.values.Add(faceBlendShapeInfo.CurrentARFace.gameObject.transform.rotation.x);
        message_transform.values.Add(faceBlendShapeInfo.CurrentARFace.gameObject.transform.rotation.y);
        message_transform.values.Add(faceBlendShapeInfo.CurrentARFace.gameObject.transform.rotation.z);
        message_transform.values.Add(faceBlendShapeInfo.CurrentARFace.gameObject.transform.rotation.w);
        OSCManager.instance.CurrentOsc.Send(message_transform);
    }

    void WDT()
    {
        OscMessage message = new OscMessage();
        message.address = "/wdt";
        message.values.Add(SystemInfo.deviceModel);
        OSCManager.instance.CurrentOsc.Send(message);
    }



    public void Test()
    {
        OscMessage message = new OscMessage();
        message.address = "/test";
        message.values.Add(12345);
        OSCManager.instance.CurrentOsc.Send(message);
    }

    public void AvatarReset()
    {
        OscMessage message = new OscMessage();
        message.address = "/avatarreset";
        OSCManager.instance.CurrentOsc.Send(message);
    }
}
