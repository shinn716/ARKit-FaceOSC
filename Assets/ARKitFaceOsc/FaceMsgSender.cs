using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(FaceBlendShapeInfo))]
public class FaceMsgSender : MonoBehaviour
{
    [SerializeField]
    FaceBlendShapeInfo faceBlendShapeInfo;

    //[SerializeField]
    //ARFace arface;
    
    // Start is called before the first frame update
    void Start()
    {
        //arface.updated += OnUpdated;
        //ARSession.stateChanged += OnSystemStateChanged;
    }

    void OnDisable()
    {
        //arface.updated -= OnUpdated;
        //ARSession.stateChanged -= OnSystemStateChanged;
    }

    private void Update()
    {
        UpdateFaceFeatures();
    }

    //void OnUpdated(ARFaceUpdatedEventArgs eventArgs)
    //{
    //    //UpdateVisibility();
    //    UpdateFaceFeatures();
    //}

    void UpdateFaceFeatures()
    {
        if (faceBlendShapeInfo.GetSkinnedMeshRenderer == null || !faceBlendShapeInfo.GetSkinnedMeshRenderer.enabled || faceBlendShapeInfo.GetSkinnedMeshRenderer.sharedMesh == null)
            return;

        foreach (var i in faceBlendShapeInfo.blendshapesList)
        {
            OscMessage message = new OscMessage();
            message.address = "/" + i.Key;
            message.values.Add(faceBlendShapeInfo.GetSkinnedMeshRenderer.GetBlendShapeWeight(i.Value));
            OSCManager.instance.CurrentOsc.Send(message);
        }
    }
}
