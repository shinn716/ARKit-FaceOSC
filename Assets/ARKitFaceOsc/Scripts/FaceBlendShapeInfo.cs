using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARKit;
using UnityEngine.XR.ARFoundation;


public class FaceBlendShapeInfo : MonoBehaviour
{
    [SerializeField]
    Text blendShapeInfo;

    public Dictionary<ARKitBlendShapeLocation, float> m_FaceArkitBlendShapeIndexMap { get; set; } = null;
    public SkinnedMeshRenderer GetSkinnedMeshRenderer
    {
        get => skinnedMeshRenderer;
        set => skinnedMeshRenderer = value;
    }
    public ARFace CurrentARFace { get; set; } = null;

    SkinnedMeshRenderer skinnedMeshRenderer = null;
    Dictionary<string, int> blendshapesList { get; set; } = new Dictionary<string, int>();

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
                CurrentARFace = target;
                skinnedMeshRenderer = target.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
                blendshapesList = shinn.AR.Utils.GetBlendShapeNamesReturnDict(skinnedMeshRenderer);
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
            string info = $"{i.Key}\t{(int)i.Value}";
            sb.AppendLine(info);
        }
        blendShapeInfo.text = sb.ToString();
    }

    void UpdateBlendShapes()
    {
        const string strPrefix = "blendShape2.";
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowDownLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "browDown_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowDownRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "browDown_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowInnerUp] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "browInnerUp", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowOuterUpLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "browOuterUp_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowOuterUpRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "browOuterUp_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekPuff] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "cheekPuff", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekSquintLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "cheekSquint_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekSquintRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "cheekSquint_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeBlinkLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeBlink_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeBlinkRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeBlink_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookDownLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeLookDown_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookDownRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeLookDown_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookInLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeLookIn_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookInRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeLookIn_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookOutLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeLookOut_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookOutRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeLookOut_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookUpLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeLookUp_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookUpRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeLookUp_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeSquintLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeSquint_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeSquintRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeSquint_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeWideLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeWide_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeWideRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "eyeWide_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawForward] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "jawForward", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "jawLeft", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawOpen] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "jawOpen", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "jawRight", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthClose] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthClose", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthDimpleLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthDimple_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthDimpleRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthDimple_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFrownLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthFrown_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFrownRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthFrown_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFunnel] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthFunnel", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthLeft", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLowerDownLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthLowerDown_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLowerDownRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthLowerDown_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPressLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthPress_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPressRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthPress_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPucker] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthPucker", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthRight", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRollLower] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthRollLower", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRollUpper] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthRollUpper", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthShrugLower] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthShrugLower", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthShrugUpper] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthShrugUpper", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthSmileLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthSmile_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthSmileRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthSmile_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthStretchLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthStretch_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthStretchRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthStretch_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthUpperUpLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthUpperUp_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthUpperUpRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "mouthUpperUp_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.NoseSneerLeft] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "noseSneer_L", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.NoseSneerRight] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "noseSneer_R", blendshapesList));
        m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.TongueOut] = skinnedMeshRenderer.GetBlendShapeWeight(shinn.AR.Utils.GetBlendShapeByIndex(strPrefix + "tongueOut", blendshapesList));
    }
}
