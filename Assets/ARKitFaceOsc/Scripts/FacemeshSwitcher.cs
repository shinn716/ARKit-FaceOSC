using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacemeshSwitcher : MonoBehaviour
{
    public Dropdown dropdown;
    public FaceBlendShapeInfo faceBlendShapeInfo;

    private bool Instflag = false;
    private Material matTransparent = null;
    private Material matSloth = null;
    private Material matWireframe = null;

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

            matSloth = faceBlendShapeInfo.GetSkinnedMeshRenderer.material;
            matWireframe = faceBlendShapeInfo.CurrentARFace.gameObject.transform.GetChild(1).GetComponent<Renderer>().material;
            matTransparent = Resources.Load<Material>("transparent");

            dropdown.value = 0;
            OnDropdown(dropdown);
        }
    }

    void OnDropdown(Dropdown dropdown)
    {
        if (faceBlendShapeInfo.GetSkinnedMeshRenderer == null)
            return;
        switch (dropdown.value)
        {
            default:
                faceBlendShapeInfo.GetSkinnedMeshRenderer.material = matTransparent;
                faceBlendShapeInfo.CurrentARFace.gameObject.transform.GetChild(1).GetComponent<Renderer>().material = matTransparent;
                break;
            case 0:
                faceBlendShapeInfo.GetSkinnedMeshRenderer.material = matTransparent;
                faceBlendShapeInfo.CurrentARFace.gameObject.transform.GetChild(1).GetComponent<Renderer>().material = matWireframe;
                break;
            case 1:
                faceBlendShapeInfo.GetSkinnedMeshRenderer.material = matSloth;
                faceBlendShapeInfo.CurrentARFace.gameObject.transform.GetChild(1).GetComponent<Renderer>().material = matTransparent;
                break;
        }
    }
}
