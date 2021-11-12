using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARKit;
using UnityEngine.XR.ARFoundation;


public class FaceBlendShapeInfo : MonoBehaviour
{
    //[SerializeField]
    SkinnedMeshRenderer skinnedMeshRenderer = null;

    [SerializeField]
    Text blendShapeInfo;

    public Dictionary<string, int> blendshapesList { get; set; } =  new Dictionary<string, int>();
    public SkinnedMeshRenderer GetSkinnedMeshRenderer
    {
        get => skinnedMeshRenderer;
        set => skinnedMeshRenderer = value;
    }

    Dictionary<ARKitBlendShapeLocation, float> m_FaceArkitBlendShapeIndexMap;

    // Start is called before the first frame update
    void Start()
    {
        m_FaceArkitBlendShapeIndexMap = new Dictionary<ARKitBlendShapeLocation, float>();
    }

    // Update is called once per frame
    void Update()
    {
        if (skinnedMeshRenderer == null)
        {
            var target = GameObject.FindObjectOfType<ARFace>();
            if (target != null)
            {
                skinnedMeshRenderer = target.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
                GetBlendShapeNames(skinnedMeshRenderer);
            }
        }

        if (skinnedMeshRenderer == null)
            return;

        if (!skinnedMeshRenderer.enabled)
            return;

        UpdateBlendShapes();

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"All blendshapes: {m_FaceArkitBlendShapeIndexMap.Count}");
        foreach (var i in m_FaceArkitBlendShapeIndexMap)
        {
            string info = $"{i.Key}\t{i.Value}";
            sb.AppendLine(info);
        }
        blendShapeInfo.text = sb.ToString();
    }

    void UpdateBlendShapes()
    {
        const string strPrefix = "blendShape2.";
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowDownLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "browDown_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowDownRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "browDown_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowInnerUp] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "browInnerUp"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowOuterUpLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "browOuterUp_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowOuterUpRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "browOuterUp_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekPuff] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "cheekPuff"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekSquintLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "cheekSquint_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekSquintRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "cheekSquint_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeBlinkLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeBlink_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeBlinkRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeBlink_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookDownLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeLookDown_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookDownRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeLookDown_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookInLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeLookIn_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookInRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeLookIn_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookOutLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeLookOut_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookOutRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeLookOut_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookUpLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeLookUp_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookUpRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeLookUp_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeSquintLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeSquint_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeSquintRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeSquint_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeWideLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeWide_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeWideRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "eyeWide_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawForward] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "jawForward"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "jawLeft"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawOpen] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "jawOpen"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "jawRight"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthClose] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthClose"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthDimpleLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthDimple_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthDimpleRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthDimple_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFrownLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthFrown_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFrownRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthFrown_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFunnel] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthFunnel"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthLeft"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLowerDownLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthLowerDown_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLowerDownRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthLowerDown_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPressLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthPress_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPressRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthPress_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPucker] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthPucker"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthRight"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRollLower] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthRollLower"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRollUpper] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthRollUpper"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthShrugLower] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthShrugLower"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthShrugUpper] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthShrugUpper"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthSmileLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthSmile_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthSmileRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthSmile_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthStretchLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthStretch_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthStretchRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthStretch_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthUpperUpLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthUpperUp_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthUpperUpRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "mouthUpperUp_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.NoseSneerLeft] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "noseSneer_L"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.NoseSneerRight] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "noseSneer_R"));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.TongueOut] = skinnedMeshRenderer.GetBlendShapeWeight(GetBlendShapeByIndex(strPrefix + "tongueOut"));
    }

    private int GetBlendShapeByIndex(string name)
    {
        blendshapesList.TryGetValue(name, out int returnvalue);
        return returnvalue;
    }

    private string[] GetBlendShapeNames(SkinnedMeshRenderer obj)
    {
        //SkinnedMeshRenderer head = obj.GetComponent<SkinnedMeshRenderer>();
        Mesh m = obj.sharedMesh;
        string[] arr;
        arr = new string[m.blendShapeCount];
        for (int i = 0; i < m.blendShapeCount; i++)
        {
            string s = m.GetBlendShapeName(i);
            //print("Blend Shape: " + s + " " + i); // Blend Shape: 4 FightingLlamaStance
            blendshapesList.Add(s, i);
            arr[i] = s;
        }
        return arr;
    }
}
