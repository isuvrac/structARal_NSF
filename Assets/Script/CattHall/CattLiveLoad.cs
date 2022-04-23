using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CattLiveLoad : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject Liveloadstart, liveloadend,liveloadmiddel, liveloadbar, liveloadLabel, cube, cube1;
    [SerializeField]
    public Transform  Min, Max;// secondR, thirdR;
    [SerializeField]
    private Vector3  mOffset, hOffset;
    [SerializeField]
    private Vector3 applypoint;
    [SerializeField]
    public float snowliveload;
    [SerializeField]
    PointForceLoad pointForceLoad;
    [SerializeField]
    Catt_Interface catt_Interface;
    [SerializeField]
    CattWindLoad CattWindLoad;
    public float targetdis, targetdisx, bound;
    void Start()
    {
        bound = Mathf.Abs(Max.position.y - Min.position.y);
        applypoint = Min.position;
        targetdisx = applypoint.x; targetdis = applypoint.y;
    }

    private void OnMouseDown()
    {
        mOffset = liveloadbar.transform.position - GetMouseWorldPos();
       
        //skywalk_Interface.LiveloadDD.value = 4;
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
        applypoint = new Vector3(0, Mathf.Clamp(draggingpoint.y, Min.position.y, Max.position.y), 0);
        targetdisx = applypoint.x; targetdis = applypoint.y;
        catt_Interface.SnowS.value = (targetdis - Min.position.y) / bound;
        pointForceLoad.upadteForce(snowliveload, CattWindLoad.windload);
    }

   // public void changecubeposition() { pointForceLoad.upadteForce();applypoint = new Vector3(0, catt_Interface.SnowS.value * bound + Min.position.y ,0); }
    //public void updateforce() { reactionForce.updateReactionForce(); bazier_Curve.Drawlines(); }

    // Update is called once per frame
    void Update()
    {
        cube.transform.position = new Vector3(cube.transform.position.x, targetdis, cube.transform.position.y); //cube1.transform.position = new Vector3(0, initialpointM.y,0);

        

        float dis = Mathf.Abs(liveloadbar.transform.position.y - Min.position.y);
        snowliveload = Mathf.Round(Mathf.Clamp(dis * 25f / bound, 0, 25f) * 10) / 10;
        float livelabel = Mathf.Round(snowliveload * 0.16f)/10;
        liveloadLabel.transform.Find("ForceLabel").gameObject.GetComponent<TextMesh>().text = livelabel.ToString() + "k/ft";
        liveloadbar.transform.SetPositionAndRotation(new Vector3(liveloadmiddel.transform.position.x, cube.transform.position.y, liveloadmiddel.transform.position.z), liveloadbar.transform.rotation);
        liveloadLabel.transform.position = liveloadbar.transform.position;

        //Vector3.Distance(cube.transform.localPosition, cube1.transform.localPosition)/2
        
        float hight = Mathf.Abs(cube.transform.localPosition.y - cube1.transform.localPosition.y);
        Liveloadstart.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, hight, 10);
        liveloadend.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, hight, 10);
        liveloadmiddel.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, hight, 10);
    }
}
