using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class Catt_Interface : MonoBehaviour
{
    [SerializeField]
    Dropdown modeDD, forceDPDD;
    [SerializeField]
    Toggle LiveLoad, DeadLoad, ReactionForce, Wind;
    [SerializeField]
    GameObject ARc, Mainc, Catt_Normal, Catt_Scale_m, Catt_Scale_i, ImageTarget, ModelTarget, ImageCanvas, Distribution, Points, snowtext,windtext;
    Transform Catt;
    [SerializeField]
    public Slider WindS, SnowS;
    [SerializeField]
    CattLiveLoad cattLiveLoad;
    [SerializeField]
    CattWindLoad cattWindLoad;
    [SerializeField]
    PointForceLoad pointForceLoad;
    // Start is called before the first frame update
    void Start()
    {
        modeDD.value = 0;
        switchMode(); switchForceDP();
    }
    void ScreenShotonclick()
    { ScreenCapture.CaptureScreenshot("Town Hall" + System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png"); }
    public void Homeonclick()
    { SceneManager.LoadScene("MainMenue"); }

    public void toggleonclick()
    {
        Catt.Find("Distribution").Find(LiveLoad.name).gameObject.SetActive(LiveLoad.isOn);
        Catt.Find("Distribution").Find(DeadLoad.name).gameObject.SetActive(DeadLoad.isOn);
        Catt.Find("Distribution").Find(Wind.name).gameObject.SetActive(Wind.isOn);
        Catt.Find(ReactionForce.name).gameObject.SetActive(ReactionForce.isOn);

    }
    public void onSliderChange() {
        windtext.GetComponent<Text>().text = (Mathf.Round(WindS.value * 1500) / 10).ToString() + " mph";
        cattWindLoad.targetdisx = cattWindLoad.Min.position.x - (WindS.value * cattWindLoad.bound) * Mathf.Tan(Mathf.PI / 6);
        cattWindLoad.targetdis = cattWindLoad.Min.position.y +(WindS.value * cattWindLoad.bound);
        snowtext.GetComponent<Text>().text = (Mathf.Round(SnowS.value * 250) / 10).ToString() + " mph";
        cattLiveLoad.targetdis = cattLiveLoad.Min.position.y + (SnowS.value * cattLiveLoad.bound);
        pointForceLoad.upadteForce(SnowS.value*25, WindS.value*150);
    }
    //print("here"); cattLiveLoad.changecubeposition(); cattWindLoad.changecubeposition();

    public void switchForceDP() {
        switch (forceDPDD.value) {
            case 0:
                print("Point");
                LiveLoad.interactable = false;
                Wind.interactable = false;
                DeadLoad.interactable = false;
                Distribution.SetActive(false);
                Points.SetActive(true);
                break;
            case 1:
                LiveLoad.interactable = true;
                Wind.interactable = true;
                DeadLoad.interactable = true;
                Distribution.SetActive(true);
                Points.SetActive(false);
                break;
        }
    
    
    
    }

    public void switchMode()
    {

        switch (modeDD.value)
        {
            case 0:
                print("Pre-loaded");
                Catt = Catt_Normal.transform;
                Mainc.SetActive(true);
                ARc.SetActive(false);
                ARc.transform.transform.SetPositionAndRotation(Mainc.transform.position, Mainc.transform.rotation);
                ARc.GetComponent<VuforiaBehaviour>().enabled = false;
                ImageCanvas.SetActive(true); Catt_Normal.SetActive(true);
                Catt_Scale_i.SetActive(false); ImageTarget.SetActive(false);
                ModelTarget.SetActive(false);
                toggleonclick();
                break;
            case 1:
                print("Indoor");
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true;
                Catt = Catt_Scale_i.transform;
                ImageCanvas.SetActive(false); Catt_Normal.SetActive(false);
                Catt_Scale_i.SetActive(false); ImageTarget.SetActive(true);
                ModelTarget.SetActive(false);
                toggleonclick();
                break;
            case 2:
                print("Outdoor");
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true;
                Catt = Catt_Scale_m.transform;
                ImageCanvas.SetActive(false); Catt_Normal.SetActive(false);
                Catt_Scale_i.SetActive(false); ImageTarget.SetActive(false);
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
