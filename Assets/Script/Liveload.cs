using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liveload : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    private GameObject Liveloadstart, liveloadend, liveloadmiddel, liveloadbar, liveloadLabel;
    [SerializeField]
    private Transform startR, endR;
    [SerializeField]
    private Vector3 applypoint, initialpointS, initialpointE, initialpointM, mOffset,hOffset;
    [SerializeField]
    public Vector3 start, end;
    [SerializeField]
    public float liveload =0.0f;
    [SerializeField]
    private ReactionForce reactionForce;

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
        reactionForce.updateReactionForce();
    }

    private void OnMouseDown()
    {
        mOffset = liveloadbar.transform.position - GetMouseWorldPos();
        if (GetMouseWorldPos().x <= liveloadmiddel.transform.position.x) { movestart = true; hOffset = Liveloadstart.transform.position - GetMouseWorldPos(); }
        else { movestart = false; hOffset = liveloadend.transform.position - GetMouseWorldPos(); }
    }

    Vector3 GetMouseWorldPos() {
        var v3 = Input.mousePosition;
        v3.z = Camera.main.WorldToScreenPoint(liveloadbar.transform.position).z;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        return v3;
    }

    private void OnMouseDrag()
    {
        Vector3 draggingpoint = GetMouseWorldPos() + mOffset;
        applypoint = new Vector3 (0,Mathf.Clamp(draggingpoint.y, 20,30), 0) ;
        Vector3 hdraggingpoint= GetMouseWorldPos() + hOffset;
        if (movestart) { start = new Vector3(Mathf.Clamp(hdraggingpoint.x, initialpointS.x, initialpointE.x), start.y, start.z); }
        else { end = new Vector3(Mathf.Clamp(hdraggingpoint.x, initialpointS.x, initialpointE.x), end.y, end.z); }
        reactionForce.updateReactionForce();
    }

    // Update is called once per frame
    void Update()
    {

        liveload = Mathf.Clamp((Mathf.Round((liveloadmiddel.transform.Find("Base").gameObject.transform.localScale.y - 4.4f) / 6 * 10) / 10), 0, 1.5f);

        Liveloadstart.transform.position = new Vector3 (start.x, initialpointS.y, start.z);
        liveloadend.transform.position = new Vector3(end.x, initialpointE.y, end.z);
        liveloadmiddel.transform.position = new Vector3((Liveloadstart.transform.position.x+ liveloadend.transform.position.x)/2, initialpointM.y, end.z);
        Liveloadstart.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, Mathf.Abs(applypoint.y - initialpointM.y - gap), 10);
        liveloadend.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, Mathf.Abs(applypoint.y - initialpointM.y - gap), 10);
        liveloadmiddel.transform.Find("Base").gameObject.transform.localScale = new Vector3(10, Mathf.Abs(applypoint.y- initialpointM.y-gap), 10);
        liveloadLabel.transform.Find("ForceLabel").gameObject.GetComponent<TextMesh>().text = liveload.ToString()+"k/ft";

        //liveloadbar.transform.LookAt(liveloadend.transform);
        
        liveloadbar.transform.SetPositionAndRotation(new Vector3(liveloadmiddel.transform.position.x, applypoint.y, liveloadmiddel.transform.position.z), liveloadbar.transform.rotation);
        liveloadbar.transform.localScale = new Vector3(2,Vector3.Distance(Liveloadstart.transform.position, liveloadend.transform.position) / 2,2);
        liveloadLabel.transform.position = liveloadbar.transform.position;
                
    }
}
