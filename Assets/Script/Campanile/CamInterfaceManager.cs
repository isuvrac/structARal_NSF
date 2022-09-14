using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class CamInterfaceManager : MonoBehaviour
{
    [SerializeField]
    public Transform Min, Max;
    [SerializeField]
    private GameObject Campanile_Normal, Campanile_Scale_i, Campanile_Scale_m, Mainc, ARc, ImageCanvas, ImageTarget, ModelTarget,InterstructureModel, modelT,WindmodeDD,
        Seismic, Wind,Deflection, Forcescale, sliderLabel, sliderNumberL, animationT, Spectral, plotSec, definition_o, modeDD, uparrow, downarrow, ScreenshotIndicate;
    [SerializeField]
    ForceUpdate forceUpdate;
    [SerializeField]
    WindLoad windLoad;
    [SerializeField]
    SeismicLoad seismicLoad;
    [SerializeField]
    Slider seismicSlider;
    int SeisIndex;
    float Renderwidth;
    [SerializeField]
    Transform Campanile;

    //When Home, screenshot, and definition buttons clicked
    public void ScreenShotonclick()
    { ScreenCapture.CaptureScreenshot("Skywalk" + System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png"); StartCoroutine(TakeScreenshotAndSave()); }

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
        //reset scene to pre-load
        modeDD.GetComponent<Dropdown>().value = 0;
        switchMode();
        animationT.GetComponent<Toggle>().interactable = false;
        animationT.GetComponent<Toggle>().isOn = true;
        WindmodeDD.GetComponent<Dropdown>().value = 0;
    }
    //screenshot function
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

    //change between modes
    private void changemode()
    {
        if (Seismic.activeSelf) { WindmodeDD.GetComponent<Dropdown>().value = 0; }
        Seismic = Campanile.GetChild(2).gameObject;
        seismicLoad = Seismic.GetComponent<SeismicLoad>();
        Wind = Campanile.GetChild(1).gameObject;
        windLoad = Wind.GetComponentInChildren<WindLoad>();
        Deflection= Campanile.GetChild(3).gameObject;
        forceUpdate = Deflection.GetComponent<ForceUpdate>();
        forceUpdate.WindMode = Wind.activeSelf;
        Min = windLoad.transform.Find("min");
        Max = windLoad.transform.Find("max");
        InterstructureModel = Campanile.GetChild(0).gameObject;

        Seismic.SetActive(false);
        Wind.SetActive(true);
        plotSec.SetActive(false);
        animationT.GetComponent<Toggle>().interactable = false;
        animationT.GetComponent<Toggle>().isOn = true;
        print(Seismic.activeSelf);
        
        Forcescale.GetComponent<Text>().text = "Force Scale: 100%";
        sliderLabel.GetComponent<Text>().text = "Wind Speed";
        seismicSlider.GetComponent<Slider>().value = 0;
        print(Seismic.activeSelf);
        
    }
    public void switchMode()
    {

        forceUpdate.DestryLines();
        switch (modeDD.GetComponent<Dropdown>().value)
        {
            case 0:
                print("Pre-loaded");
                Mainc.SetActive(true);
                ARc.SetActive(false);
                ARc.transform.transform.SetPositionAndRotation(Mainc.transform.position, Mainc.transform.rotation);
                ARc.GetComponent<VuforiaBehaviour>().enabled = false;
                Campanile = Campanile_Normal.transform;ImageCanvas.SetActive(true); Campanile_Normal.SetActive(true);
                Campanile_Scale_i.SetActive(false); ImageTarget.SetActive(false);
                ModelTarget.SetActive(false);
                changemode();forceUpdate.width = 1f; forceUpdate.StartUp(); onSliderChange();
                break;
            case 1:
                print("Indoor");
                
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true; //if (Seismic.activeSelf) { WindmodeDD.GetComponent<Dropdown>().value = 0; }
                Campanile = Campanile_Scale_i.transform;
                ImageCanvas.SetActive(false); Campanile_Normal.SetActive(false);
                Campanile_Scale_i.SetActive(true); ImageTarget.SetActive(true);
                ModelTarget.SetActive(false);
                changemode();forceUpdate.width = 0.5f; forceUpdate.StartUp(); onSliderChange();
                break;
            case 2:
                print("Outdoor");
                
                Mainc.SetActive(false);
                ARc.SetActive(true);
                ARc.GetComponent<VuforiaBehaviour>().enabled = true; //if (Seismic.activeSelf) { WindmodeDD.GetComponent<Dropdown>().value = 0; }
                Campanile = Campanile_Scale_m.transform;
                ImageCanvas.SetActive(false); Campanile_Normal.SetActive(false);
                Campanile_Scale_i.SetActive(false); ImageTarget.SetActive(false);
                ModelTarget.SetActive(true);
                changemode();forceUpdate.width = 0.2f; forceUpdate.StartUp(); onSliderChange();
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
    public void onModeltoggelchange() { InterstructureModel.SetActive(modelT.GetComponent<Toggle>().isOn); }
    //slider function
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
 

        }
        else
        {
            
            windLoad.TextandForceUpdate((seismicSlider.GetComponent<Slider>().value * 100 / 100) * 150);
            windLoad.targetdis = -seismicSlider.GetComponent<Slider>().value * Mathf.Abs(Max.position.x-Min.position.x)+Mathf.Max(Min.position.x, Max.position.x);

        }



    }
    public void onanimationchange() {
        if (!animationT.GetComponent<Toggle>().isOn) { onSliderChange(); }
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
