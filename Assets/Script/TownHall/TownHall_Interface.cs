using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class TownHall_Interface : MonoBehaviour
{
   
    [SerializeField]
    Dropdown modeDD;
    [SerializeField]
    Toggle LiveLoad, DeadLoad, ReactionForce, Model;
    [SerializeField]
    GameObject ARc, Mainc, Towwn_Normal, Towwn_Scale_m, Towwn_Scale_i, ImageTarget, ModelTarget, ImageCanvas, ScreenshotIndicate;
    Transform Towwn;
    [SerializeField]
    FixJoinUI fixJoin;
    [SerializeField]
    TownDeflection townDeflection;
    [SerializeField]
    Town_ReactionForce town_ReactionForce;
    bool firststart = false;
    // Start is called before the first frame update
    void Start()
    {
        modeDD.value = 0;
        switchMode();
        firststart = true;
    }
     public void ScreenShotonclick()
    { ScreenCapture.CaptureScreenshot("Town Hall" + System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png"); StartCoroutine(TakeScreenshotAndSave()); }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "structARal", "ScreenShot.png");

        ScreenshotIndicate.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        ScreenshotIndicate.SetActive(false);
        // To avoid memory leaks
        Destroy(ss);
    }

    public void Homeonclick()
    { SceneManager.LoadScene("MainMenue"); }

    public void toggleonclick()
    {
        Towwn.Find(LiveLoad.name).gameObject.SetActive(LiveLoad.isOn);
        Towwn.Find("WindForce").gameObject.SetActive(LiveLoad.isOn);
        Towwn.Find(DeadLoad.name).gameObject.SetActive(DeadLoad.isOn);
        Towwn.Find(ReactionForce.name).gameObject.SetActive(ReactionForce.isOn);
        Towwn.Find(Model.name).gameObject.SetActive(Model.isOn);
        
    }
    private void changeScript()
    {
        townDeflection = Towwn.Find("Deflection").gameObject.GetComponent<TownDeflection>();
        fixJoin = Towwn.Find("FixedPoint").gameObject.GetComponent<FixJoinUI>();
        town_ReactionForce = Towwn.Find(ReactionForce.name).gameObject.GetComponent<Town_ReactionForce>();
        
    }
    private void updateforce()
    {
        townDeflection.setupline();
        fixJoin.setupFixed();
        town_ReactionForce.updatereactionforce();
    }

    public void switchMode()
    {
        if (firststart)
        {
            town_ReactionForce.DestroyLines();
            fixJoin.DestroyLines();
            townDeflection.DestroyLines();
        }
        switch (modeDD.value)
        {
            case 0:
                
                print("Pre-loaded");
                Mainc.SetActive(true);
                ARc.SetActive(false);
                ARc.transform.transform.SetPositionAndRotation(Mainc.transform.position, Mainc.transform.rotation);
                ARc.GetComponent<VuforiaBehaviour>().enabled = false;
                ImageCanvas.SetActive(true); Towwn_Normal.SetActive(true);
                Towwn_Scale_i.SetActive(false); ImageTarget.SetActive(false);
                ModelTarget.SetActive(false);
                Towwn = Towwn_Normal.transform; changeScript();
                town_ReactionForce.scale = 1;
                townDeflection.scale = 1;
                fixJoin.scale = 1;
                town_ReactionForce.startup();
                townDeflection.Startup();
                fixJoin.startup();
                toggleonclick();updateforce();
                break;
            case 1:
                print("Indoor");
               
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true;
                ImageCanvas.SetActive(false); Towwn_Normal.SetActive(false);
                Towwn_Scale_i.SetActive(false); ImageTarget.SetActive(true);
                ModelTarget.SetActive(false); 
                Towwn = Towwn_Scale_i.transform; changeScript(); 
                town_ReactionForce.scale = 0.5f;
                townDeflection.scale = 0.5f;
                fixJoin.scale = 0.5f;
                town_ReactionForce.startup();
                townDeflection.Startup();
                fixJoin.startup();
                toggleonclick(); updateforce();
                break;
            case 2:
                print("Outdoor");      
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true;
                ImageCanvas.SetActive(false); Towwn_Normal.SetActive(false);
                Towwn_Scale_i.SetActive(false); ImageTarget.SetActive(false);
                ModelTarget.SetActive(true);
                Towwn = Towwn_Scale_m.transform; changeScript();
                town_ReactionForce.scale = 0.2f;
                townDeflection.scale = 0.2f;
                fixJoin.scale = 0.1f;
                town_ReactionForce.startup();
                townDeflection.Startup();
                fixJoin.startup();
                toggleonclick(); updateforce();
                break;

        }


    }
}
