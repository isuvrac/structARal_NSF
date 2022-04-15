using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class CamInterfaceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Campanile_Normal, Campanile_Scale_i, Campanile_Scale_m, Mainc, ARc, ImageCanvas, ImageTarget, ModelTarget,
        Seismic, Wind,Deflection, Forcescale, sliderLabel, sliderNumberL, animationT, Spectral, plotSec, definition_o, modeDD, uparrow, downarrow, MomentArrow1, MomentArrow2;
    [SerializeField]
    ForceUpdate forceUpdate;
    [SerializeField]
    WindLoad windLoad;
    [SerializeField]
    SeismicLoad seismicLoad;
    [SerializeField]
    Slider seismicSlider;
    int SeisIndex;
    [SerializeField]
    Transform Campanile;

    public void ScreenShotonclick()
    { ScreenCapture.CaptureScreenshot("Skywalk" + System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png"); }
    public void Homeonclick()
    { SceneManager.LoadScene("MainMenue"); }
    public void Definitiononclick()
    {
        definition_o.GetComponentInParent<VerticalLayoutGroup>().childControlHeight = !definition_o.activeSelf;
        definition_o.SetActive(!definition_o.activeSelf);
        uparrow.SetActive(!definition_o.activeSelf);
        downarrow.SetActive(definition_o.activeSelf);
    }
    // Start is called before the first frame update
    void Start()
    {
        //skywalk = Skywalk_Normal.transform;
        modeDD.GetComponent<Dropdown>().value = 0;
        switchMode();
        animationT.GetComponent<Toggle>().interactable = false; 
    }

    private void changemode()
    {
        Seismic = Campanile.GetChild(2).gameObject;
        seismicLoad = Seismic.GetComponent<SeismicLoad>();
        Wind = Campanile.GetChild(1).gameObject;
        windLoad = Wind.GetComponentInChildren<WindLoad>();
        Deflection= Campanile.GetChild(3).gameObject;
        forceUpdate = Deflection.GetComponent<ForceUpdate>();
        onSliderChange();
    }

    public void switchMode()
    {

        switch (modeDD.GetComponent<Dropdown>().value)
        {
            case 0:
                print("Pre-loaded");
                Campanile = Campanile_Normal.transform;
                Mainc.SetActive(true);
                ARc.SetActive(false);
                ARc.transform.transform.SetPositionAndRotation(Mainc.transform.position, Mainc.transform.rotation);
                ARc.GetComponent<VuforiaBehaviour>().enabled = false;
                ImageCanvas.SetActive(true); Campanile_Normal.SetActive(true);
                Campanile_Scale_i.SetActive(false); ImageTarget.SetActive(false);
                ModelTarget.SetActive(false);
                changemode();
                break;
            case 1:
                print("Indoor");
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true;
                Campanile = Campanile_Scale_i.transform;
                ImageCanvas.SetActive(false); Campanile_Normal.SetActive(false);
                Campanile_Scale_i.SetActive(false); ImageTarget.SetActive(true);
                ModelTarget.SetActive(false);
                changemode();
                break;
            case 2:
                print("Outdoor");
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true;
                Campanile = Campanile_Scale_m.transform;
                ImageCanvas.SetActive(false); Campanile_Normal.SetActive(false);
                Campanile_Scale_i.SetActive(false); ImageTarget.SetActive(false);
                ModelTarget.SetActive(true);
                changemode();
                break;

        }


    }


    public void onModechange() {
        Seismic.SetActive(!Seismic.activeSelf);
        Wind.SetActive(!Wind.activeSelf);
        plotSec.SetActive(Seismic.activeSelf);
        animationT.GetComponent<Toggle>().interactable = Seismic.activeSelf;
        forceUpdate.WindMode = Wind.activeSelf;
        if (Seismic.activeSelf)
        {
            Forcescale.GetComponent<Text>().text = "Force Scale: 10%";
            sliderLabel.GetComponent<Text>().text = "Intensity";
            seismicSlider.GetComponent<Slider>().value = 0;
        }
        else {
            Forcescale.GetComponent<Text>().text = "Force Scale: 100%";
            sliderLabel.GetComponent<Text>().text = "Wind Speed";
            seismicSlider.GetComponent<Slider>().value = 0;
            
        }
        onSliderChange();
    }

    public void onSliderChange()
    {
        if (Seismic.activeSelf)
        {
            float[] Ss_vals = new float[8] { 0.05f, 0.25f, 0.5f, 0.75f, 1, 1.25f, 2, 3 };
            float SliderConvertvalue = seismicSlider.GetComponent<Slider>().value * 2.95f + 0.05f;
            int i = 0;

            foreach (float n in Ss_vals)
            {
                if (SliderConvertvalue >= n) { i++; }
                else if (SliderConvertvalue < n) { break; }
            }
            SeisIndex = i - 1;

            seismicLoad.OnIndexChange(SeisIndex);
            foreach (Transform a in Spectral.transform)
            {
                if (a.tag == "Plot")
                {
                    a.gameObject.SetActive(false);
                }
            }
            Spectral.transform.GetChild(SeisIndex).gameObject.SetActive(true);
            float rotate1 = 0;
            rotate1 = Mathf.Clamp(-seismicSlider.GetComponent<Slider>().value * 200, -89.9f, 0.1f);
            //print(rotate1);
            Vector3 to1 = new Vector3(rotate1, MomentArrow1.transform.rotation.eulerAngles.y, MomentArrow1.transform.rotation.eulerAngles.z);
            MomentArrow1.transform.eulerAngles = to1;

            if (seismicSlider.GetComponent<Slider>().value <= 0.45)
            {
                MomentArrow2.SetActive(false); MomentArrow1.SetActive(true);

            }
            else
            {
                MomentArrow1.SetActive(false); MomentArrow2.SetActive(true);
                //MomentArrow1.SetActive(false); MomentArrow2.SetActive(true);
                // rotate = Mathf.Clamp(seismicSlider.GetComponent<Slider>().value * 200-360, -270.1f,379.9f);
                //print(rotate);
                //Vector3 to = new Vector3(rotate, MomentArrow2.transform.rotation.eulerAngles.y, MomentArrow2.transform.rotation.eulerAngles.z);
                //MomentArrow2.transform.eulerAngles = to;
            }
        }
        else
        {
            windLoad.targetdis = -seismicSlider.GetComponent<Slider>().value * 12 - 4.6f;
            windLoad.TextandForceUpdate((seismicSlider.GetComponent<Slider>().value * 100 / 100) * 150);

        }



    }

    // Update is called once per frame
    void Update()
    {
        
        if (Seismic.activeSelf)
        {
            sliderNumberL.GetComponent<Text>().text = "Ss= " + forceUpdate.seismicSs.ToString()+" S1= " + forceUpdate.seismicS1.ToString();
        }
        else
        {
            sliderNumberL.GetComponent<Text>().text = ((Mathf.Round(seismicSlider.GetComponent<Slider>().value * 100) / 100) *150).ToString() + " mph";
        }

        seismicLoad.animate = animationT.GetComponent<Toggle>().isOn && Seismic.activeSelf;
    }
}
