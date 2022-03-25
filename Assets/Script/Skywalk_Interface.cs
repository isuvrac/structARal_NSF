using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Skywalk_Interface : MonoBehaviour
{

    [SerializeField]
    Button ScreenShot, Home, Definition;
    [SerializeField]
    Dropdown modeDD;
    [SerializeField]
    Toggle LiveLoad, DeadLoad, ReactionForce, Ruler;
    [SerializeField]
    GameObject Liveload_o, Deadload_o, ReactionForce_o, Ruler_o, Mainc,ARc, Skywalk_Normal, Skywalk_Scale, ImageTarget, ModelTarget,ImageCanvas;

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
    }

    public void toggleonclick(Toggle toggle) {
        print(toggle.name);
        GameObject target= skywalk.Find(toggle.name).gameObject;
        target.SetActive(!target.activeSelf);
    }

    public void switchMode() {

        switch (modeDD.value) {
            case 0:
                print("Pre-loaded");
                skywalk = Skywalk_Normal.transform;
                Skywalk_Normal.SetActive(true);
                ImageCanvas.SetActive(Mainc.activeSelf);
                Skywalk_Normal.SetActive(false);
                Skywalk_Normal.SetActive(true);
                ARc.SetActive(false);
                ImageTarget.SetActive(false);
                ModelTarget.SetActive(false);
                break;
            case 1:
                print("Indoor");
                skywalk = Skywalk_Normal.transform;
                Mainc.SetActive(false);
                ImageCanvas.SetActive(Mainc.activeSelf);
                ARc.SetActive(true);
                Skywalk_Normal.SetActive(false);
                Skywalk_Normal.SetActive(true);
                ImageTarget.SetActive(true);
                ModelTarget.SetActive(false);
                break;
            case 2:
                print("Outdoor");
                skywalk = Skywalk_Scale.transform;
                Mainc.SetActive(false);
                ImageCanvas.SetActive(Mainc.activeSelf);
                ARc.SetActive(true);
                Skywalk_Normal.SetActive(false);
                ImageTarget.SetActive(false);
                ModelTarget.SetActive(true);
                break;

        }
    
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
