using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindLoad : MonoBehaviour
{
    [SerializeField]
    public Transform Min, Max, WindLoadBar, arrowbase, ShearArrow, MomentArrow;//,cube
    [SerializeField]
    public Vector3 applypoint, mOffset;
    [SerializeField]
    private ForceUpdate forceUpdate;
    private List<Transform> Arrows = new List<Transform>();
    float WindForceScale =12.5f, WindForce;
    bool rotating = true;


    // Start is called before the first frame update
    void Start()
    {
        applypoint = Max.position;
       
        foreach (Transform a in this.transform) {
            print(a.tag);
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
        
        forceUpdate.Drawlines(WindForce);
        ShearArrow.transform.Find("Base").gameObject.transform.localScale = new Vector3(1, forceUpdate.shear / 10, 1);
        float rotate =  Mathf.Clamp((360-Mathf.Abs(forceUpdate.moment / 34)),271,359);
        Vector3 to = new Vector3(rotate, MomentArrow.rotation.eulerAngles.y, MomentArrow.rotation.eulerAngles.z);
        MomentArrow.eulerAngles = to;print(MomentArrow.eulerAngles.x);
       
    }

    private void OnMouseUp()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //cube.transform.position= new Vector3(applypoint.x, 0, 0);
        WindLoadBar.transform.position = new Vector3(applypoint.x, WindLoadBar.transform.position.y, WindLoadBar.transform.position.z);
        float height = Mathf.Abs(WindLoadBar.transform.localPosition.x- arrowbase.localPosition.x);
        foreach (Transform t in Arrows)
        {
            t.transform.Find("Base").gameObject.transform.localScale = new Vector3(5, height, 5);
        }
        WindForce = (height-1.01f) * WindForceScale;
    }
}
