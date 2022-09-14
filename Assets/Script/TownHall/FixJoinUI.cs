using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixJoinUI : MonoBehaviour
{
    public GameObject Tips, Tip;
    [SerializeField]
    float BF1, BF2, BS1, BS2, BM1, BM2, BR, CF1, CF2, CS1, CS2, CM1, CM2, CR;
    [SerializeField]
    Text Bf1, Bf2, Bs1, Bs2, Bm1, Bm2, Cf1, Cf2, Cs1, Cs2, Cm1, Cm2;
    public float scale;
    public Material transMat;
    Vector3[] startendBS1, startendBS2, startendBF1, startendBF2, startendBM1, startendBM2, startendCS1, startendCS2, startendCF1, startendCF2, startendCM1,
        startendCM2, startendB1,startendB2, startendC1, startendC2;
    LineRenderer ShearB1, ShearB2, ForceB1, ForceB2, MomentB1, MomentB2, ShearC1, ShearC2, ForceC1, ForceC2, MomentC1, MomentC2, CornerB1, CornerB2, CornerC1, CornerC2;
    float R;
    int pointsN=25;
    [SerializeField]
    Transform cornerB,cornerB1, cornerB2, cornerC, cornerC1, cornerC2;
    public void assignValue(float bf1, float bf2, float bs1, float bs2, float bm1, float bm2, float br, float cf1, float cf2, float cs1, float cs2, float cm1, float cm2, float cr) {
         BF1=bf1;  BF2=bf2; BS1 =bs1;  BS2=bs2 ;BM1=bm1  ;  BM2=bm2;  BR =br; CF1=cf1 ;   CF2=cf2;   CS1= cs1;  CS2 = cs2;   CM1= cm1;   CM2=cm2; CR= cr ;
        
        Bf1.text=Mathf.Abs(Mathf.Round(BF1*10)/10).ToString()+" k"; Bf2.text = Mathf.Abs(Mathf.Round(BF2 * 10) / 10).ToString()+" k"; Bs1.text = Mathf.Abs(Mathf.Round(BS1 * 10) / 10).ToString()+" k";
        Bs2.text = Mathf.Abs(Mathf.Round(BS2 * 10) / 10).ToString() + " k"; Bm1.text = Mathf.Abs(Mathf.Round(BM1 * 10) / 10).ToString()+"k-ft"; Bm2.text = Mathf.Abs(Mathf.Round(BM2 * 10) / 10).ToString()+" k-ft"; 
        Cf1.text = Mathf.Abs(Mathf.Round(CF1 * 10) / 10).ToString()+" k"; Cf2.text = Mathf.Abs(Mathf.Round(CF2 * 10) / 10).ToString()+" k";Cs1.text = Mathf.Abs(Mathf.Round(CS1 * 10) / 10).ToString()+ " k"; 
        Cs2.text = Mathf.Abs(Mathf.Round(CS2 * 10) / 10).ToString()+ " k"; Cm1.text = Mathf.Abs(Mathf.Round(CM1 * 10) / 10).ToString()+ " k-ft"; Cm2.text = Mathf.Abs(Mathf.Round(CM2 * 10) / 10).ToString()+ "k-ft";
        updateFixedJoin();
    }

    
    public void startup()
    {
        //set up lines
        startendB1 = new Vector3[2];
        startendB2 = new Vector3[2];
        startendC1 = new Vector3[2];
        startendC2 = new Vector3[2];
        startendBS1 = new Vector3[2]; 
        startendBS2 = new Vector3[2];
        startendBF1 = new Vector3[2]; 
        startendBF2 = new Vector3[2]; 
        startendBM1 = new Vector3[2];
        startendBM2 = new Vector3[2]; 
        startendCS1 = new Vector3[2];
        startendCS2 = new Vector3[2]; 
        startendCF1 = new Vector3[2];
        startendCF2 = new Vector3[2];
        startendCM1 = new Vector3[2]; 
        startendCM2 = new Vector3[2]; 
    }

    public void DestroyLines() {
        Destroy(CornerB1.gameObject);
        Destroy(CornerB2.gameObject);
        Destroy(CornerC1.gameObject); 
        Destroy(CornerC2.gameObject); 
        Destroy(ShearB1.gameObject); 
        Destroy(ShearB2.gameObject); 
        Destroy(ForceB1.gameObject); 
        Destroy(ForceB2.gameObject);
        Destroy(MomentB1.gameObject); 
        Destroy(MomentB2.gameObject); 
        Destroy(ShearC1.gameObject); 
        Destroy(ShearC2.gameObject); 
        Destroy(ForceC1.gameObject); 
        Destroy(ForceC2.gameObject); 
        Destroy(MomentC1.gameObject); 
        Destroy(MomentC2.gameObject);
        if (MomentC2.transform.childCount > 0){
            Destroy(MomentC1.transform.Find("Tip(Clone)").gameObject);
            Destroy(MomentC2.transform.Find("Tip(Clone)").gameObject);
            Destroy(ShearB1.transform.Find("Tips(Clone)").gameObject);
            Destroy(ShearB2.transform.Find("Tips(Clone)").gameObject);
            Destroy(ForceB1.transform.Find("Tips(Clone)").gameObject);
            Destroy(ForceB2.transform.Find("Tips(Clone)").gameObject);
            Destroy(MomentB1.transform.Find("Tip(Clone)").gameObject);
            Destroy(MomentB2.transform.Find("Tip(Clone)").gameObject);
            Destroy(ShearC1.transform.Find("Tips(Clone)").gameObject);
            Destroy(ShearC2.transform.Find("Tips(Clone)").gameObject);
            Destroy(ForceC1.transform.Find("Tips(Clone)").gameObject);
            Destroy(ForceC2.transform.Find("Tips(Clone)").gameObject);
        }
        
    }

    public void setupFixed() {
        R = scale * 3;
        CornerB1 = new GameObject().AddComponent<LineRenderer>();
        CornerB2 = new GameObject().AddComponent<LineRenderer>();
        CornerC1 = new GameObject().AddComponent<LineRenderer>();
        CornerC2 = new GameObject().AddComponent<LineRenderer>();
        ShearB1 = new GameObject().AddComponent<LineRenderer>(); 
        ShearB2 = new GameObject().AddComponent<LineRenderer>(); 
        ForceB1 = new GameObject().AddComponent<LineRenderer>(); 
        ForceB2 = new GameObject().AddComponent<LineRenderer>(); 
        MomentB1 = new GameObject().AddComponent<LineRenderer>();
        MomentB2 = new GameObject().AddComponent<LineRenderer>();
        ShearC1 = new GameObject().AddComponent<LineRenderer>(); 
        ShearC2 = new GameObject().AddComponent<LineRenderer>();
        ForceC1 = new GameObject().AddComponent<LineRenderer>();
        ForceC2 = new GameObject().AddComponent<LineRenderer>();
        MomentC1 = new GameObject().AddComponent<LineRenderer>(); 
        MomentC2 = new GameObject().AddComponent<LineRenderer>(); 
        initilizeCorner(CornerB1);
        initilizeCorner(CornerB2);
        initilizeCorner(CornerC1);
        initilizeCorner(CornerC2);
        initilizeCurve(MomentB1.gameObject,MomentB1); 
        initilizeCurve(MomentB2.gameObject,MomentB2); 
        initilizeCurve(MomentC1.gameObject, MomentC1); 
        initilizeCurve(MomentC2.gameObject, MomentC2); 
        initilizeLines(ShearC1.gameObject, ShearC1);
        initilizeLines(ShearC2.gameObject, ShearC2); 
        initilizeLines(ShearB2.gameObject, ShearB2); 
        initilizeLines(ShearB1.gameObject, ShearB1);
        initilizeLines(ForceC1.gameObject, ForceC1); 
        initilizeLines(ForceC2.gameObject, ForceC2);
        initilizeLines(ForceB1.gameObject, ForceB1); 
        initilizeLines(ForceB2.gameObject, ForceB2); 
                                                     
    }

    void updateFixedJoin()
    {
        print(scale);
        DrawCorner(startendB1, cornerB, BR,  Mathf.PI * 3 / 2, scale * 10, CornerB1, cornerB1);
        DrawCorner(startendB2, cornerB, BR,          Mathf.PI, scale * 10, CornerB2, cornerB2);
        DrawForce(startendBF1, cornerB1, BR,                0, scale * BF1 / 5, ForceB1);
        DrawForce(startendBS1, cornerB1, BR,     Mathf.PI / 2, scale * BS1 * 2, ShearB1);
        DrawForce(startendBF2, cornerB2, BR,     Mathf.PI / 2, scale * BF2 * 2, ForceB2);
        DrawForce(startendBS2, cornerB2, BR,         Mathf.PI, scale * 5, ShearB2);
        DrawCorner(startendC1, cornerC, CR,  Mathf.PI * 3 / 2, scale * 10, CornerC1, cornerC1);
        DrawCorner(startendC2, cornerC, CR,          Mathf.PI, scale * 10, CornerC2, cornerC2);
        DrawForce(startendCF1, cornerC1, CR,                0, scale * CF1 * 2, ForceC1);
        DrawForce(startendCS1, cornerC1, CR, Mathf.PI / 2, scale * CS1/5, ShearC1);
        DrawForce(startendCF2, cornerC2, CR,     Mathf.PI / 2, scale * CF2 / 5, ForceC2);
        DrawForce(startendCS2, cornerC2, CR,                0, scale * CS2 * 2, ShearC2);
        DrawMoment(startendBM1, cornerB1, BR,Mathf.PI * 3 / 2,  BM1 / 5, MomentB1, scale * 4);
        DrawMoment(startendBM2, cornerB2, BR,        Mathf.PI,  BM2 / 5, MomentB2, scale * 4);
        DrawMoment(startendCM1, cornerC1, CR,    Mathf.PI / 2,  CM1 / 5, MomentC1, scale * 4);
        DrawMoment(startendCM2, cornerC2, CR,               0,  CM2 / 5, MomentC2, scale * 4);

    }

    void initilizeCorner(LineRenderer Moment)
    {
        //setup line
        Moment.name = "corner";
        Moment.transform.SetParent(this.transform);
        Moment.startWidth = R / 4;
        Moment.endWidth = R / 4;
        Moment.positionCount = pointsN + 1;
    }
    void initilizeLines(GameObject parent, LineRenderer Moment)
    {
        //setup line
        Moment.name = "Lines";
        Moment.transform.SetParent(this.transform);
        Moment.startWidth = R / 3;
        Moment.endWidth = R / 3;
        Moment.positionCount = pointsN + 1;
        GameObject arrow = Instantiate(Tips, new Vector3(0, 0, 0), Quaternion.identity);
        arrow.transform.parent = parent.transform;
    }
    void initilizeCurve(GameObject parent, LineRenderer Moment)
    {
        //setup line
        Moment.name = "Curve";
        Moment.transform.SetParent(this.transform);
        Moment.startWidth = R / 3;
        Moment.endWidth = R /3;
        Moment.positionCount = pointsN + 1;
        GameObject arrow = Instantiate(Tip, new Vector3(0, 0, 0), Quaternion.identity);
        arrow.transform.parent = parent.transform;
    }
  

    private void DrawCorner(Vector3[] startend, Transform corner, float Ang, float initAng, float radius, LineRenderer Moment, Transform cornerend)
    {
        float momentAng = initAng + Ang;
        startend[0] = new Vector3(corner.position.x, corner.position.y, corner.position.z);
        startend[1] = new Vector3(corner.position.x + radius * Mathf.Sin(-Mathf.PI / 2 - momentAng), corner.position.y + radius * Mathf.Cos(-Mathf.PI / 2 + -momentAng), corner.position.z);
        Vector3 endpoint = new Vector3(corner.position.x + (radius + 1) * Mathf.Sin(-Mathf.PI / 2 - momentAng), corner.position.y + (radius + 1) * Mathf.Cos(-Mathf.PI / 2 + -momentAng), corner.position.z);
        //initiate all points on curve
        Vector3[] pointsP = new Vector3[pointsN];
        for (int i = 0; i < 25; i++)
        {
            pointsP[i] = new Vector3(corner.position.x + i*(radius/25) * Mathf.Sin(-Mathf.PI / 2 -momentAng ), corner.position.y + i * (radius / 25) * Mathf.Cos(-Mathf.PI / 2 - momentAng), corner.position.z);
        }
        Moment.SetPositions(pointsP);
        Moment.SetPosition(25, startend[1]); Moment.material = transMat; Moment.material.color = new Color(0f, 0f, 0f, 1f);


        cornerend.position = startend[1];
    }
    private void DrawForce(Vector3[] startend, Transform corner, float Ang, float initAng, float force, LineRenderer Moment)
    {
        float momentAng = initAng + Ang;
        float gap = 2 * scale;
        startend[0] = new Vector3(corner.position.x + Mathf.Sign(force)* gap * Mathf.Sin(-Mathf.PI / 2 - momentAng), corner.position.y + Mathf.Sign(force) * gap * Mathf.Cos(-Mathf.PI / 2 + -momentAng), corner.position.z);
        startend[1] = new Vector3(startend[0].x + force * Mathf.Sin(-Mathf.PI / 2 - momentAng), startend[0].y+ force * Mathf.Cos(-Mathf.PI / 2 + -momentAng), startend[0].z);
        //initiate all points on curve
        Vector3[] pointsP = new Vector3[pointsN];
        for (int i = 0; i < 25; i++)
        {
            pointsP[i] = new Vector3(startend[0].x  + i * (force / 25) * Mathf.Sin(-Mathf.PI / 2 - momentAng), startend[0].y  + i * (force / 25) * Mathf.Cos(-Mathf.PI / 2 - momentAng), startend[0].z);
        }
        Moment.SetPositions(pointsP);
        Moment.SetPosition(pointsN, startend[1]);Moment.material = transMat; Moment.material.color = new Color(0f, 1f, 0f, 1f); 
        Vector3 newVector = new Vector3(0, 0, 0);
        newVector = startend[0] - startend[1];
        GameObject arrow = Moment.transform.Find("Tips(Clone)").gameObject;
        arrow.transform.localScale = new Vector3(scale, scale, scale);
        arrow.transform.Find("Tip").gameObject.GetComponent<LineRenderer>().SetWidth(3 * scale, 0 * scale);
        arrow.transform.SetPositionAndRotation(startend[0], Quaternion.LookRotation(-newVector, Vector3.forward));
    }
    private void DrawMoment(Vector3[] startend, Transform corner, float initAng, float Ang, float force, LineRenderer Moment,float radius) {
        float momentAng = -force;
        startend[0] = new Vector3(corner.position.x +radius* Mathf.Sin((Ang+initAng)), corner.position.y+radius * Mathf.Cos( (Ang + initAng)), corner.position.z);
        startend[1] = new Vector3(corner.position.x +radius * Mathf.Sin((Ang + initAng)-momentAng), corner.position.y +radius * Mathf.Cos((Ang + initAng)- momentAng  ), corner.position.z);
        //initiate all points on curve
        Vector3[] pointsP = new Vector3[pointsN];
        for (int i = 0; i < 25; i++)
        {
            pointsP[i] = new Vector3(corner.position.x + radius * Mathf.Sin( (Ang + initAng)- i * momentAng / 25), corner.position.y + radius * Mathf.Cos( (Ang + initAng)- i *momentAng / 25), corner.position.z);
        }
        Moment.SetPositions(pointsP);
        Moment.SetPosition(pointsN, startend[1]); Moment.material = transMat; Moment.material.color = new Color(0f, 1f, 0f, 1f);
        Vector3 newVector = new Vector3(0, 0, 0);newVector = corner.position - startend[1];
        GameObject arrow = Moment.transform.Find("Tip(Clone)").gameObject;
        arrow.transform.localScale = new Vector3(scale, scale ,scale);
        arrow.transform.Find("Tip").gameObject.GetComponent<LineRenderer>().SetWidth(3 * scale, 0 * scale);
        arrow.transform.SetPositionAndRotation(startend[1], Quaternion.LookRotation(-newVector, -Mathf.Sign(momentAng)*corner.forward));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
