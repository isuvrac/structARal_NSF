using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownLiveLoad : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject Liveloadstart, liveloadend, liveloadmiddel, liveloadbar, liveloadLabel, cube, cube1;
    [SerializeField]
    public Transform startR, endR, Min, Max;// secondR, thirdR;
    [SerializeField]
    private Vector3 initialpointS, initialpointE, initialpointM, mOffset, hOffset;
    [SerializeField]
    public Vector3 start, end, applypoint;
    [SerializeField]
    public float liveload = 0.0f;
    //[SerializeField]
    //private ReactionForce reactionForce;
    //[SerializeField]
    //private Skywalk_Interface skywalk_Interface;
    //[SerializeField]
    //private Bazier_Curve bazier_Curve;

    private float gap;
    private bool movestart;


    void Start()
    {
        start = startR.position;
        end = endR.position;
        initialpointS = Liveloadstart.transform.position;
        initialpointE = liveloadend.transform.position;
        initialpointM = liveloadmiddel.transform.position;
        gap = liveloadmiddel.transform.Find("Base").gameObject.transform.position.y - initialpointM.y;
        applypoint = Min.position;
        //updateforce();
    }

    private void OnMouseDown()
    {
        mOffset = liveloadbar.transform.position - GetMouseWorldPos();
        if (GetMouseWorldPos().x <= liveloadmiddel.transform.position.x) { movestart = true; hOffset = Liveloadstart.transform.position - GetMouseWorldPos(); }
        else { movestart = false; hOffset = liveloadend.transform.position - GetMouseWorldPos(); }
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
        Vector3 hdraggingpoint = GetMouseWorldPos() + hOffset;
        if (movestart) { start = new Vector3(Mathf.Clamp(hdraggingpoint.x, initialpointS.x, initialpointE.x), start.y, start.z); }
        else { end = new Vector3(Mathf.Clamp(hdraggingpoint.x, initialpointS.x, initialpointE.x), end.y, end.z); }
       // updateforce();
    }

    //public void updateforce() { reactionForce.updateReactionForce(); bazier_Curve.Drawlines(); }

    // Update is called once per frame
    void Update()
    {
        cube.transform.position = new Vector3(cube.transform.position.x, applypoint.y, cube.transform.position.y); //cube1.transform.position = new Vector3(0, initialpointM.y,0);

        float dis = Mathf.Abs(liveloadbar.transform.position.y - Min.position.y);
        float bounad = Mathf.Abs(Max.position.y - Min.position.y);
        liveload = Mathf.Round(Mathf.Clamp(dis * 2.5f / bounad, 0, 2.5f) * 10) / 10;
        liveloadLabel.transform.Find("ForceLabel").gameObject.GetComponent<TextMesh>().text = liveload.ToString() + "k/ft";


        Liveloadstart.transform.position = new Vector3(start.x, initialpointS.y, start.z);
        liveloadend.transform.position = new Vector3(end.x, initialpointE.y, end.z);
        liveloadmiddel.transform.position = new Vector3((Liveloadstart.transform.position.x + liveloadend.transform.position.x) / 2, initialpointM.y, end.z);


        liveloadbar.transform.SetPositionAndRotation(new Vector3(liveloadmiddel.transform.position.x, cube.transform.position.y, liveloadmiddel.transform.position.z), liveloadbar.transform.rotation);
        liveloadbar.transform.localScale = new Vector3(2, Vector3.Distance(Liveloadstart.transform.localPosition, liveloadend.transform.localPosition) / 2, 2);//
        liveloadLabel.transform.position = liveloadbar.transform.position;

        //Vector3.Distance(cube.transform.localPosition, cube1.transform.localPosition)/2

        float hight = Mathf.Abs(cube.transform.localPosition.y - cube1.transform.localPosition.y); 
        Liveloadstart.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, hight, 10);
        liveloadend.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, hight, 10);
        liveloadmiddel.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, hight, 10);
    }
}
