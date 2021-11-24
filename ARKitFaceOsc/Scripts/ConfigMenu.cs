using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConfigMenu : MonoBehaviour
{
    [SerializeField] InputField port;
    [SerializeField] InputField ip;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        try
        {
            OSCManager.instance.TargetIp = PlayerPrefs.GetString("TargetIp");
            OSCManager.instance.TargetPort = PlayerPrefs.GetString("TargetPort");
        }
        catch (System.Exception)
        {
            OSCManager.instance.TargetIp = "127.0.0.1";
            OSCManager.instance.TargetPort = "12000";
        }

        yield return new WaitForEndOfFrame();
        ip.text = OSCManager.instance.TargetIp;
        port.text = OSCManager.instance.TargetPort.ToString();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetString("TargetIp", ip.text);
        PlayerPrefs.SetString("TargetPort", port.text);
    }
}
