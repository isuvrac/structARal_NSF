using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CattWindLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject Liveloadstart, liveloadend, liveloadmiddel, liveloadbar, liveloadLabel, cube, cube1;
    [SerializeField]
    public Transform Min, Max;
    [SerializeField]
    private Vector3 mOffset;
    [SerializeField]
    private Vector3 applypoint;
    [SerializeField]
    public float windload;
    [SerializeField]
    PointForceLoad pointForceLoad;
    [SerializeField]
    Catt_Interface catt_Interface;
    [SerializeField]
    CattLiveLoad cattLiveLoad;
    public float targetdis, targetdisx,bound;
    // Start is called before the first frame update
    void Start()
    {
        bound = Mathf.Abs(Max.position.y - Min.position.y);
        applypoint = Min.position;
        targetdisx = applypoint.x; targetdis = applypoint.y;
    }
    private void OnMouseDown()
    {
        mOffset = liveloadbar.transform.position - GetMouseWorldPos();
    }

    Vector3 GetMouseWorldPos()
    {
        var v3 = Input.mousePosition;
        v3.z = Camera.main.WorldToScreenPoint(liveloadbar.transform.position).z;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        return v3;
    }

    private void OnMouseDrag()
    {
        Vector3 draggingpoint = GetMouseWorldPos() + mOffset;
        applypoint = new Vector3(Min.position.x-(Mathf.Clamp(draggingpoint.y, Min.position.y, Max.position.y)-Min.position.y)*Mathf.Tan(Mathf.PI/6), Mathf.Clamp(draggingpoint.y, Min.position.y, Max.position.y), 0);
        targetdisx = applypoint.x;targetdis = applypoint.y; 
        catt_Interface.WindS.value = (targetdis - Min.position.y) / bound;
        pointForceLoad.upadteForce(cattLiveLoad.snowliveload, windload); 
    }
    // Update is called once per frame
    void Update()
    {
        cube.transform.position = new Vector3(targetdisx, targetdis, cube.transform.position.z);
        float dis = Mathf.Abs(liveloadbar.transform.position.y - Min.position.y);
        windload = Mathf.Round(Mathf.Clamp(dis * 150f / bound, 0, 150) * 10) / 10;
        float windloadlabel = Mathf.Round(windload * 0.02f)/10;
        liveloadLabel.transform.Find("ForceLabel").gameObject.GetComponent<TextMesh>().text = windloadlabel.ToString() + "k/ft";
        liveloadbar.transform.SetPositionAndRotation(new Vector3(cube.transform.position.x, cube.transform.position.y, liveloadmiddel.transform.position.z), liveloadbar.transform.rotation);
        liveloadLabel.transform.position = liveloadbar.transform.position;
        float hight = Mathf.Abs(cube.transform.localPosition.y - cube1.transform.localPosition.y) /(Mathf.Cos(Mathf.PI/6));
        Liveloadstart.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, hight, 10);
        liveloadend.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, hight, 10);
        liveloadmiddel.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, hight, 10);

    }
}
