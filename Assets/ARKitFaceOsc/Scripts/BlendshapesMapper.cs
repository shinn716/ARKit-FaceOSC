using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BlendshapesMapper", order = 1)]
public class BlendshapesMapper : ScriptableObject
{
    [System.Serializable]
    public class Content
    {
        public Content(BlendshapesDefine.List _blendshapesName, string _TargetBlendshapesName)
        {
            blendshapesName = _blendshapesName;
            TargetBlendshapesName = _TargetBlendshapesName;
        }

        public BlendshapesDefine.List blendshapesName;
        //public BlendshapesDefine.ARKitBlendShapeLocation aRKitBlendShapeLocation;

        public string TargetBlendshapesName = string.Empty;
    }

    public SkinnedMeshRenderer target;

    public List<Content> contents = new List<Content>();

    private string[] tmp;

    public Dictionary<string, string> dict = new Dictionary<string, string>();

    public void Init()
    {
        foreach (var i in contents)
            dict.Add(i.blendshapesName.ToString(), i.TargetBlendshapesName);
    }

    [ContextMenu("ShowAllNameInConsole")]
    private void ShowAllNameInConsole()
    {
        Mesh m = target.sharedMesh;
        Debug.Log("===Blend Shap===");
        for (int i = 0; i < m.blendShapeCount; i++)
        {
            string s = m.GetBlendShapeName(i);
            Debug.Log(s); // Blend Shape: 4 FightingLlamaStance
        }
    }

    [ContextMenu("Clear")]
    private void Clear()
    {
        contents.Clear();
    }

    [ContextMenu("AutoGetTargetBlendshapesName")]
    private void AutoGetTargetBlendshapesName()
    {
        tmp = shinn.AR.Utils.GetBlendShapeNamesReturnStrArray(target);

        foreach(var i in tmp)
        {
            contents.Add(new Content(BlendshapesDefine.List.BrowDownLeft, i));
        }
    }
}
