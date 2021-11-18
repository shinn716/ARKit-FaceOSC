using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacemeshSwitcher : MonoBehaviour
{
    public Dropdown dropdown;
    public FaceBlendShapeInfo faceBlendShapeInfo;

    public GameObject prefabWireframe;

    private GameObject facewireframe = null;
    private bool Instflag = false;
    public Material originmat;

    // Start is called before the first frame update
    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate { OnDropdown(dropdown); });
    }

    // Update is called once per frame
    void Update()
    {
        if (faceBlendShapeInfo.CurrentARFace != null)
        {
            if (Instflag)
                return;

            Instflag = true;

            GameObject go = Instantiate(prefabWireframe);
            go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            facewireframe = go;

            originmat = faceBlendShapeInfo.GetSkinnedMeshRenderer.material;
        }
    }

    void OnDropdown(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            default:
            case 0:
                faceBlendShapeInfo.GetSkinnedMeshRenderer.material = originmat;
                facewireframe.GetComponent<MeshRenderer>().enabled = false;
                break;
            case 1:
                faceBlendShapeInfo.GetSkinnedMeshRenderer.material = Resources.Load<Material>("transparent");
                facewireframe.GetComponent<MeshRenderer>().enabled = true;
                break;
        }
    }
}
