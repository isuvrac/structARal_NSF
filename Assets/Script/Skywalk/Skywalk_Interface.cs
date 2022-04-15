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
    Dropdown modeDD ;
    public Dropdown LiveloadDD;
    [SerializeField]
    Toggle LiveLoad, DeadLoad, ReactionForce, Ruler;
    [SerializeField]
    GameObject ARc,Mainc, Skywalk_Normal, Skywalk_Scale_m, Skywalk_Scale_i, ImageTarget, ModelTarget,ImageCanvas, definition_o, uparrow, downarrow;
    [SerializeField]
    Liveload liveload;
    Transform skywalk;

    // Start is called before the first frame update
    void Start()
    {
        ScreenShot.onClick.AddListener(ScreenShotonclick);
        Home.onClick.AddListener(Homeonclick);
        Definition.onClick.AddListener(Definitiononclick);
        //skywalk = Skywalk_Normal.transform;
        modeDD.value = 0;
        switchMode();
    }

    void ScreenShotonclick()
    { ScreenCapture.CaptureScreenshot("Skywalk"+System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")+".png"); }
    void Homeonclick()
    {SceneManager.LoadScene("MainMenue");  }
    void Definitiononclick()
    {
        definition_o.GetComponentInParent<VerticalLayoutGroup>().childControlHeight = !definition_o.activeSelf;
        definition_o.SetActive(!definition_o.activeSelf);
        uparrow.SetActive(!definition_o.activeSelf);
        downarrow.SetActive(definition_o.activeSelf);
    }

    public void toggleonclick() {
        skywalk.Find(LiveLoad.name).gameObject.SetActive(LiveLoad.isOn);
        LiveloadDD.value = 0;
        liveload = skywalk.Find(LiveLoad.name).gameObject.GetComponent<Liveload>();
        skywalk.Find(DeadLoad.name).gameObject.SetActive(DeadLoad.isOn);
        skywalk.Find(ReactionForce.name).gameObject.SetActive(ReactionForce.isOn);
        skywalk.Find(Ruler.name).gameObject.SetActive(Ruler.isOn);
        //print(toggle.name);
        //GameObject target= skywalk.Find(toggle.name).gameObject;
        // target.SetActive(!target.activeSelf);
    }
    public void switchLiveload()
    {
        switch (LiveloadDD.value)
        {
            case 0:
                liveload.applypoint.y = liveload.Min.position.y;
                liveload.start = liveload.startR.position;
                liveload.end = liveload.endR.position;
                liveload.updateforce();
                break;
            case 1:
                liveload.applypoint.y = liveload.Max.position.y;
                liveload.start = liveload.startR.position;
                liveload.end = liveload.endR.position;
                liveload.updateforce();
                break;
            case 2:
                liveload.applypoint.y = liveload.Max.position.y;
                liveload.start = liveload.startR.position;
                liveload.end = liveload.secondR.position;
                liveload.updateforce();
                break;
            case 3:
                liveload.applypoint.y = liveload.Max.position.y;
                liveload.start = liveload.thirdR.position;
                liveload.end = liveload.endR.position;
                liveload.updateforce();
                break;
        }


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
                ImageCanvas.SetActive(true); Skywalk_Normal.SetActive(true);
                Skywalk_Scale_i.SetActive(false);ImageTarget.SetActive(false);
                ModelTarget.SetActive(false);
                toggleonclick();
                break;
            case 1:
                print("Indoor");
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true;
                skywalk = Skywalk_Scale_i.transform;
                ImageCanvas.SetActive(false);Skywalk_Normal.SetActive(false);
                Skywalk_Scale_i.SetActive(false);ImageTarget.SetActive(true);
                ModelTarget.SetActive(false);
                toggleonclick();
                break;
            case 2:
                print("Outdoor");
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true;
                skywalk = Skywalk_Scale_m.transform;
                ImageCanvas.SetActive(false);Skywalk_Normal.SetActive(false);
                Skywalk_Scale_i.SetActive(false);ImageTarget.SetActive(false);
                ModelTarget.SetActive(true);
                toggleonclick();
                break;

        }
    
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
