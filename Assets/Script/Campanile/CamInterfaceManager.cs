using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CamInterfaceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Seismic, Wind, Forcescale, sliderLabel, sliderNumberL, animationT, Spectral, plotSec;
    [SerializeField]
    ForceUpdate forceUpdate;
    [SerializeField]
    WindLoad windLoad;
    [SerializeField]
    SeismicLoad seismicLoad;
    [SerializeField]
    Slider seismicSlider;
    int SeisIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        animationT.GetComponent<Toggle>().interactable = false;

    }
    public void onModechange() {
        Seismic.SetActive(!Seismic.activeSelf);
        Wind.SetActive(!Wind.activeSelf);
        plotSec.SetActive(Seismic.activeSelf);
        animationT.GetComponent<Toggle>().interactable = Seismic.activeSelf;
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
    }

    public void onSliderChange()
    {
        if (Seismic.activeSelf)
        {
            float[] Ss_vals = new float[8] { 0.05f, 0.25f, 0.5f, 0.75f, 1, 1.25f, 2, 3 };
            float SliderConvertvalue = seismicSlider.GetComponent<Slider>().value * 2.95f + 0.05f;
            int i = 0;

            foreach (float n in Ss_vals) {
                if ( SliderConvertvalue>= n) { i++;}
                else if (SliderConvertvalue <n ) { break; }
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
            windLoad.targetdis = -seismicSlider.GetComponent<Slider>().value * 12 - 4.6f;
            windLoad.TextandForceUpdate((seismicSlider.GetComponent<Slider>().value * 100 / 100) * 150);
        }



    }

    // Update is called once per frame
    void Update()
    {
        forceUpdate.WindMode = Wind.activeSelf;
        if (Seismic.activeSelf)
        {
            sliderNumberL.GetComponent<Text>().text = "Ss= " + forceUpdate.seismicSs.ToString()+" S1= " + forceUpdate.seismicS1.ToString();
        }
        else
        {
            sliderNumberL.GetComponent<Text>().text = ((Mathf.Round(seismicSlider.GetComponent<Slider>().value * 100) / 100) *150).ToString() + " mph";
        }

        forceUpdate.animate = animationT.GetComponent<Toggle>().isOn;
    }
}
