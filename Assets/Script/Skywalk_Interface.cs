using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class Skywalk_Interface : MonoBehaviour
{

    [SerializeField]
    Button ScreenShot, Home, Definition;
    [SerializeField]
    Dropdown modeDD;
    [SerializeField]
    Toggle LiveLoad, DeadLoad, ReactionForce, Ruler;
    [SerializeField]
    GameObject Liveload_o, Deadload_o, ReactionForce_o, Ruler_o, ARc,Mainc, Skywalk_Normal, Skywalk_Scale_m, Skywalk_Scale_i, ImageTarget, ModelTarget,ImageCanvas, definition_o;
   
    Transform skywalk;

    // Start is called before the first frame update
    void Start()
    {
        ScreenShot.onClick.AddListener(ScreenShotonclick);
        Home.onClick.AddListener(Homeonclick);
        Definition.onClick.AddListener(Definitiononclick);
        //skywalk = Skywalk_Normal.transform;
        switchMode();
    }

    void ScreenShotonclick()
    { ScreenCapture.CaptureScreenshot("Skywalk"); }
    void Homeonclick()
    {SceneManager.LoadScene("MainMenue");  }
    void Definitiononclick()
    {
        definition_o.GetComponentInParent<VerticalLayoutGroup>().childControlHeight = !definition_o.activeSelf;
        definition_o.SetActive(!definition_o.activeSelf);
    }

    public void toggleonclick() {
        skywalk.Find(LiveLoad.name).gameObject.SetActive(LiveLoad.isOn);
        skywalk.Find(DeadLoad.name).gameObject.SetActive(DeadLoad.isOn);
        skywalk.Find(ReactionForce.name).gameObject.SetActive(ReactionForce.isOn);
        skywalk.Find(Ruler.name).gameObject.SetActive(Ruler.isOn);
        //print(toggle.name);
        //GameObject target= skywalk.Find(toggle.name).gameObject;
        // target.SetActive(!target.activeSelf);
    }

    public void switchMode() {

        switch (modeDD.value) {
            case 0:
                print("Pre-loaded");
                skywalk = Skywalk_Normal.transform;
                Mainc.SetActive(true);
                ARc.SetActive(false);
                ARc.transform.transform.SetPositionAndRotation(Mainc.transform.position, Mainc.transform.rotation);
                ARc.GetComponent<VuforiaBehaviour>().enabled=false;
                toggleonclick();
                ImageCanvas.SetActive(true); Skywalk_Normal.SetActive(true);
                Skywalk_Scale_i.SetActive(false);ImageTarget.SetActive(false);
                ModelTarget.SetActive(false);
                break;
            case 1:
                print("Indoor");
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true;
                toggleonclick();
                skywalk = Skywalk_Scale_i.transform;
                ImageCanvas.SetActive(false);Skywalk_Normal.SetActive(false);
                Skywalk_Scale_i.SetActive(true);ImageTarget.SetActive(true);
                ModelTarget.SetActive(false);
                break;
            case 2:
                print("Outdoor");
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true;
                toggleonclick();
                skywalk = Skywalk_Scale_m.transform;
                ImageCanvas.SetActive(false);Skywalk_Normal.SetActive(false);
                Skywalk_Scale_i.SetActive(false);ImageTarget.SetActive(false);
                ModelTarget.SetActive(true);
                break;

        }
    
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
