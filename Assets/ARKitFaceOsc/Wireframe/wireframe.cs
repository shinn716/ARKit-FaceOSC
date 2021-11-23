using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wireframe : MonoBehaviour
{
    bool flag = false;

    private void Init()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh.SetIndices(meshFilter.mesh.GetIndices(0), MeshTopology.Lines, 0);
    }

    private void Update()
    {
        Init();

        //if (GetComponent<MeshFilter>() != null && GetComponent<MeshRenderer>().enabled)
        //{
        //    if (!flag)
        //    {
        //        Init();
        //        flag = true;
        //    }
        //}
    }
}