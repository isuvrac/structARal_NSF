using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bazier_Curve : MonoBehaviour
{

    private LineRenderer Defline1, Defline2, Defline3;
    private Vector3[] startend1, startend2,startend3;
    private int pointsN = 25;
    private Vector3[] pointsP1,pointsP2,pointsP3;
    [SerializeField]
    Liveload liveloadS;
    [SerializeField]
    Transform refP1, refP2, refP3, refP4;
    float ratio;
    [SerializeField]
    GameObject DeflectionLabel1, DeflectionLabel2, DeflectionLabel3;
    float deflect1, deflect2, deflect3;
    public float weidth =0.2f; 

    // Start is called before the first frame update
    public void SetupBazier()
    {
        ratio = 200 / Mathf.Abs(refP1.position.x - refP4.position.x);

        pointsP1 = new Vector3[pointsN];
        pointsP2 = new Vector3[pointsN];
        pointsP3 = new Vector3[pointsN];

        //initiate start and end points
        startend1 = new Vector3[2];
        startend1[0] = refP1.position;
        startend1[1] = refP2.position;
        startend2 = new Vector3[2];
        startend2[0] = refP2.position;
        startend2[1] = refP3.position;
        startend3 = new Vector3[2];
        startend3[0] = refP3.position;
        startend3[1] = refP4.position;

        //setup line
        Defline1 = new GameObject().AddComponent<LineRenderer>();
        Defline1.transform.SetParent(this.transform);
        Defline1.startWidth = 1f* weidth;
        Defline1.endWidth = 1f * weidth;
        Defline1.positionCount = pointsN + 1;
        Defline1.material.color = new Color(0f, 0f, 1f);

        //setup line
        Defline2 = new GameObject().AddComponent<LineRenderer>();
        Defline2.transform.SetParent(this.transform);
        Defline2.startWidth = 1f * weidth;
        Defline2.endWidth = 1f * weidth;
        Defline2.positionCount = pointsN + 1;
        Defline2.material.color = new Color(0f, 0f, 1f);


        //setup line
        Defline3 = new GameObject().AddComponent<LineRenderer>();
        Defline3.transform.SetParent(this.transform);
        Defline3.startWidth = 1f * weidth;
        Defline3.endWidth = 1f * weidth;
        Defline3.positionCount = pointsN + 1;
        Defline3.material.color = new Color(0f, 0f, 1f);

        Drawlines();
    }

    public void Drawlines() {
        //initiate all points on curve
        float resx = (startend1[1].x - startend1[0].x) / 24;
        float resy = (startend1[1].y - startend1[0].y) / 24;
        for (int i = 0; i < pointsN; i++)
        {
            pointsP1[i] = new Vector3(startend1[0].x + resx * i, startend1[0].y + resy * i, startend1[0].z);
        }
        pointsP1 = GetDeflection(pointsP1,Mathf.Abs(refP1.position.x - refP1.position.x), Mathf.Abs(refP2.position.x - refP1.position.x), Mathf.Abs(liveloadS.start.x - refP1.position.x), 
            Mathf.Abs(liveloadS.end.x - refP1.position.x), liveloadS.liveload, deflect1,DeflectionLabel1);
        Defline1.SetPositions(pointsP1);
        Defline1.SetPosition(pointsN, startend1[1]);
        print(deflect1 );

        resx = (startend2[1].x - startend2[0].x) / 24;
        resy = (startend2[1].y - startend2[0].y) / 24;
        for (int i = 0; i < pointsN; i++)
        {
            pointsP2[i] = new Vector3(startend2[0].x + resx * i, startend2[0].y + resy * i, startend2[0].z);
        }
        pointsP2 = GetDeflection(pointsP2, Mathf.Abs(refP2.position.x - refP1.position.x), Mathf.Abs(refP3.position.x - refP1.position.x), Mathf.Abs(liveloadS.start.x - refP1.position.x),
            Mathf.Abs(liveloadS.end.x - refP1.position.x), liveloadS.liveload, deflect2, DeflectionLabel2);
        Defline2.SetPositions(pointsP2);
        Defline2.SetPosition(pointsN, startend2[1]);

        resx = (startend3[1].x - startend3[0].x) / 24;
        resy = (startend3[1].y - startend3[0].y) / 24;
        for (int i = 0; i < pointsN; i++)
        {
            pointsP3[i] = new Vector3(startend3[0].x + resx * i, startend3[0].y + resy * i, startend3[0].z);
        }
        pointsP3 = GetDeflection(pointsP3, Mathf.Abs(refP3.position.x - refP1.position.x), Mathf.Abs(refP4.position.x - refP1.position.x), Mathf.Abs(liveloadS.start.x - refP1.position.x), 
            Mathf.Abs(liveloadS.end.x - refP1.position.x), liveloadS.liveload, deflect3, DeflectionLabel3);
        Defline3.SetPositions(pointsP3);
        Defline3.SetPosition(pointsN, startend3[1]);
    }

    private Vector3[] GetDeflection(Vector3[] vals, float beamStart, float beamEnd, float loadStart, float loadEnd, float totalLoad, float deflect, GameObject DeflectionLabel) {

        float L = (beamEnd - beamStart) * ratio;
        float a = Mathf.Max(0, (loadStart - beamStart) * ratio);
        float b = (Mathf.Min(loadEnd, beamEnd) - Mathf.Max(loadStart, beamStart)) * ratio;
        float w = totalLoad;
        float L3 = L * L * L;
        for (int i = 0; i < vals.Length; ++i)
        {
            float x = ((vals[i].x - refP1.position.x) - beamStart)*ratio;
            if (i == 0) { print("first value:" + (x.ToString())); }
            float x2 = x * x;
            float x3 = x * x * x;
            float delta = 0;
            if ( b > 0)
            { 
                if (x < a)
                {
                    delta = w * (
                                 (x * b * (2 * L - 2 * a - b) / L) *
                                 (-2 * x2 + 2 * a * (2 * L - a) + b * (2 * L - 2 * a - b))
                                 );
                }
                else if (x <= a + b)
                {
                    delta = w * (
                                 Mathf.Pow(x - a, 4) +
                                 (x * b * (2 * L - 2 * a - b) / L) *
                                 (-2 * x2 + 2 * a * (2 * L - a) + b * (2 * L - 2 * a - b))
                                 );
                }
                else
                {
                    delta = w * (
                                 ((L - x) * b * (b + 2 * a) / L) *
                                 (
                                  -2 * Mathf.Pow((L - x), 2) +
                                  2 * (L - a - b) * (L + a + b) +
                                  b * (b + 2 * a)
                                  )
                                 );
                }
            }

            delta += 1.2f * x * (L3 - 2 * L * x2 + x3);
            delta *= -3.72063E-10f*200/ratio;
            if (delta*ratio /200 < deflect) {deflect = delta * ratio / 200;}
            
            vals[i].y = vals[i].y+delta;

        }
        print(deflect);
            DeflectionLabel.GetComponent<TextMesh>().text = (Mathf.Round(deflect * 1000) / 1000).ToString() + " in";
        return vals;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
