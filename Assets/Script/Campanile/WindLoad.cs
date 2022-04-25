using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindLoad : MonoBehaviour
{
    [SerializeField]
    public Transform Min, Max, WindLoadBar, arrowbase, ShearArrow, MomentArrow;//,cube
    [SerializeField]
    public Vector3 applypoint, mOffset;
    [SerializeField]
    private ForceUpdate forceUpdate;
    public float WindForce;
    private List<Transform> Arrows = new List<Transform>();
    float WindForceScale =12.5f;
    [SerializeField]
    GameObject DefLable, Min_P, Max_P, MomentL, ShearL, seismicSlider;
    public float targetdis;

    // Start is called before the first frame update
    void Awake()
    {
        //applypoint = Max.position;
        //targetdis = Max.position.x;
        foreach (Transform a in this.transform) {
            if (a.tag == "WindForceArrow") {

                Arrows.Add(a);
            }
        }

    }
    private void OnMouseDown()
    {
        mOffset = WindLoadBar.transform.position - GetMouseWorldPos();
    }

    Vector3 GetMouseWorldPos()
    {
        var v3 = Input.mousePosition;
        v3.z = Camera.main.WorldToScreenPoint(WindLoadBar.transform.position).z;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        return v3;
    }

    private void OnMouseDrag()
    {
        Vector3 draggingpoint = GetMouseWorldPos() + mOffset;
        applypoint = new Vector3(Mathf.Clamp(draggingpoint.x, Min.position.x, Max.position.x), 0, 0);
        targetdis = applypoint.x;
        TextandForceUpdate(WindForce); seismicSlider.GetComponent<Slider>().value =-(targetdis - Mathf.Max(Min.position.x, Max.position.x)) / Mathf.Abs(Max.position.x - Min.position.x);
    }

    public void TextandForceUpdate(float windForce) {
        
        forceUpdate.Drawlines(windForce, 2000);
        //Change label and rotation 
        MomentL.GetComponent<TextMesh>().text = forceUpdate.moment.ToString() + " k-ft";
        ShearL.GetComponent<TextMesh>().text = forceUpdate.shear.ToString() + " k";
        DefLable.GetComponent<TextMesh>().text = forceUpdate.DefLabel.ToString() + " in";
        Max_P.GetComponent<TextMesh>().text = forceUpdate.Max_P.ToString() + " psf";
        Min_P.GetComponent<TextMesh>().text = forceUpdate.Min_P.ToString() + " psf";
        ShearArrow.transform.Find("Base").gameObject.transform.localScale = new Vector3(1, forceUpdate.shear / 10, 1);
        //float rotate =  Mathf.Clamp((360-Mathf.Abs(forceUpdate.moment / 34)),271,359);
        //Vector3 to = new Vector3(rotate, MomentArrow.rotation.eulerAngles.y, MomentArrow.rotation.eulerAngles.z);
        //MomentArrow.eulerAngles = to;
        
    }

    // Update is called once per frame
    void Update()
    {
        WindLoadBar.transform.position = new Vector3(targetdis, WindLoadBar.transform.position.y, WindLoadBar.transform.position.z);
        float height = Mathf.Abs(WindLoadBar.transform.localPosition.x- arrowbase.localPosition.x);
        foreach (Transform t in Arrows)
        {
            t.transform.Find("Base").gameObject.transform.localScale = new Vector3(5, height, 5);
        }
        WindForce = (height-1.01f) * WindForceScale;
    }
}
