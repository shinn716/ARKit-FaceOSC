using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace shinn.AR
{
    public static class Utils
    {
        public static string[] GetBlendShapeNamesReturnStrArray(SkinnedMeshRenderer obj)
        {
            Mesh m = obj.sharedMesh;
            string[] arr;
            arr = new string[m.blendShapeCount];
            for (int i = 0; i < m.blendShapeCount; i++)
            {
                string s = m.GetBlendShapeName(i);
                arr[i] = s;
            }
            return arr;
        }

        public static Dictionary<string, int> GetBlendShapeNamesReturnDict(SkinnedMeshRenderer obj)
        {
            Dictionary<string, int> blendshapesList = new Dictionary<string, int>();
            Mesh m = obj.sharedMesh;
            for (int i = 0; i < m.blendShapeCount; i++)
            {
                var s = m.GetBlendShapeName(i);
                //var key = dict2Lower ? s.ToLower() : s;
                blendshapesList.Add(s, i);
            }
            return blendshapesList;
        }

        public static int GetBlendShapeByIndex(string name, Dictionary<string, int> blendshapesList)
        {
            //var key = dict2Lower ? name.ToLower() : name;
            blendshapesList.TryGetValue(name, out int returnvalue);
            return returnvalue;
        }
    }
}