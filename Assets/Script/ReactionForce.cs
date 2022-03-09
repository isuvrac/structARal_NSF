using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionForce : MonoBehaviour
{
    float skywalkloadvalue;
    [SerializeField]
    Liveload liveloadS;
    [SerializeField]
    float Beam1rf, Beam2rf, Beam3rf, Beam4rf,ratrio;
    [SerializeField]
    Transform refP1,refP2,refP3,refP4;
    // Start is called before the first frame update
    void Start()
    {
        ratrio =200/Mathf.Abs(refP1.position.x - refP4.position.x);
        updateReactionForce();
    }

    public void updateReactionForce() {
        Beam1rf = 0;
        Beam2rf = 0;
        Beam3rf = 0;
        Beam4rf = 0;
        (float, float) rcn=(0,0);
        rcn = GetReactionForce(Beam1rf, Beam2rf,Mathf.Abs(refP1.position.x- refP1.position.x), Mathf.Abs(refP2.position.x - refP1.position.x), Mathf.Abs(liveloadS.start.x - refP1.position.x), Mathf.Abs(liveloadS.end.x - refP1.position.x), liveloadS.liveload);
        Beam1rf = Mathf.Max(0, Mathf.Round(rcn.Item1* 10) / 10);
        Beam2rf = Mathf.Max(0, Mathf.Round(rcn.Item2 * 10) / 10);
        rcn = GetReactionForce(Beam2rf, Beam3rf, Mathf.Abs(refP2.position.x - refP1.position.x), Mathf.Abs(refP3.position.x - refP1.position.x), Mathf.Abs(liveloadS.start.x - refP1.position.x), Mathf.Abs(liveloadS.end.x - refP1.position.x), liveloadS.liveload);
        Beam2rf = Mathf.Max(0, Mathf.Round(rcn.Item1 * 10) / 10);
        Beam3rf = Mathf.Max(0, Mathf.Round(rcn.Item2 * 10) / 10);
        rcn = GetReactionForce(Beam3rf, Beam4rf, Mathf.Abs(refP3.position.x - refP1.position.x), Mathf.Abs(refP4.position.x - refP1.position.x), Mathf.Abs(liveloadS.start.x - refP1.position.x), Mathf.Abs(liveloadS.end.x - refP1.position.x), liveloadS.liveload);
        Beam3rf = Mathf.Max(0, Mathf.Round(rcn.Item1 * 10) / 10);
        Beam4rf = Mathf.Max(0, Mathf.Round(rcn.Item2 * 10) / 10); 
    }

    private (float, float) GetReactionForce(float rcnL, float rcnR, float beamStart, float beamEnd, float loadStart, float loadEnd, float totalload) {

        float L = (beamEnd - beamStart)*ratrio;
        float a = Mathf.Max(0, (loadStart-beamStart)* ratrio);
        float b = (Mathf.Min(loadEnd, beamEnd)-Mathf.Max(loadStart,beamStart))*ratrio;
        float w = totalload;
        float L3 = L * L * L;
        if (b <= 0)
        {

            rcnL += 0.6f * L;
            rcnR += 0.6f * L;
        }
        else {
            rcnL += 0.6f * L + w * b * (2 * L - 2 * a - b) / (2 * L);
            rcnR += 0.6f * L + w * b * (2 * a + b) / (2 * L);
        }
        return (rcnL,rcnR);
    }


    // Update is called once per frame
    void Update()
    {
        refP1.transform.Find("SupportArrow").transform.Find("Base").gameObject.transform.localScale = new Vector3(10, Beam1rf * (-1) / 5, 10);
        refP2.transform.Find("SupportArrow").transform.Find("Base").gameObject.transform.localScale = new Vector3(10, Beam2rf * (-1) / 5, 10);
        refP3.transform.Find("SupportArrow").transform.Find("Base").gameObject.transform.localScale = new Vector3(10, Beam3rf * (-1) / 5, 10);
        refP4.transform.Find("SupportArrow").transform.Find("Base").gameObject.transform.localScale = new Vector3(10, Beam4rf * (-1) / 5, 10);
        refP1.transform.Find("ReactionForceLabel").gameObject.GetComponent<TextMesh>().text = (Beam1rf).ToString() + "k/ft";
        refP2.transform.Find("ReactionForceLabel").gameObject.GetComponent<TextMesh>().text = (Beam2rf).ToString() + "k/ft";
        refP3.transform.Find("ReactionForceLabel").gameObject.GetComponent<TextMesh>().text = (Beam3rf).ToString() + "k/ft";
        refP4.transform.Find("ReactionForceLabel").gameObject.GetComponent<TextMesh>().text = (Beam4rf).ToString() + "k/ft";
    }
}
