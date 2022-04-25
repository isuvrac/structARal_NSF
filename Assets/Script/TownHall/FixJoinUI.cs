using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixJoinUI : MonoBehaviour
{
    public GameObject myPrefab;
    [SerializeField]
    float BF1, BF2, BS1, BS2, BM1, BM2, BR, CF1, CF2, CS1, CS2, CM1, CM2, CR;
    [SerializeField]
    Text Bf1, Bf2, Bs1, Bs2, Bm1, Bm2, Cf1, Cf2, Cs1, Cs2, Cm1, Cm2;
    Vector3[] startendBS1, startendBS2, startendBF1, startendBF2, startendBM1, startendBM2, startendCS1, startendCS2, startendCF1, startendCF2, startendCM1,
        startendCM2, startendB1,startendB2, startendC1, startendC2;
    //Vector3[] startendArrowBS1, startendArrowBS2, startendArrowBF1, startendArrowBF2, startendArrowBM1, startendArrowBM2, startendArrowCS1,
      //  startendArrowCS2, startendArrowCF1, startendArrowCF2, startendArrowCM1, startendArrowCM2;
    LineRenderer ShearB1, ShearB2, ForceB1, ForceB2, MomentB1, MomentB2, ShearC1, ShearC2, ForceC1, ForceC2, MomentC1, MomentC2, CornerB1, CornerB2, CornerC1, CornerC2;
    //LineRenderer ArrowBS1, ArrowBS2, ArrowBF1, ArrowBF2, ArrowBM1, ArrowBM2, ArrowCS1, ArrowCS2, ArrowCF1, ArrowCF2, ArrowCM1, ArrowCM2;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        R = 3;
        print(R);
        //set up lines
        startendB1 = new Vector3[2];
        startendB2 = new Vector3[2];
        startendC1 = new Vector3[2];
        startendC2 = new Vector3[2];
        startendBS1 = new Vector3[2]; //startendArrowBS1 = new Vector3[2];
        startendBS2 = new Vector3[2]; //startendArrowBS2 = new Vector3[2];
        startendBF1 = new Vector3[2]; //startendArrowBF1 = new Vector3[2];
        startendBF2 = new Vector3[2]; //startendArrowBF2 = new Vector3[2];
        startendBM1 = new Vector3[2]; //startendArrowBM1 = new Vector3[2];
        startendBM2 = new Vector3[2]; //startendArrowBM2 = new Vector3[2];
        startendCS1 = new Vector3[2]; //startendArrowCS1 = new Vector3[2];
        startendCS2 = new Vector3[2]; //startendArrowCS2 = new Vector3[2];
        startendCF1 = new Vector3[2];// startendArrowCF1 = new Vector3[2];
        startendCF2 = new Vector3[2]; //startendArrowCF2 = new Vector3[2];
        startendCM1 = new Vector3[2]; //startendArrowCM1 = new Vector3[2];
        startendCM2 = new Vector3[2]; //startendArrowCM2 = new Vector3[2];
        CornerB1 = new GameObject().AddComponent<LineRenderer>();
        CornerB2 = new GameObject().AddComponent<LineRenderer>();
        CornerC1 = new GameObject().AddComponent<LineRenderer>();
        CornerC2 = new GameObject().AddComponent<LineRenderer>();
        ShearB1 = new GameObject().AddComponent<LineRenderer>(); //ArrowBS1 = new GameObject().AddComponent<LineRenderer>();
        ShearB2 = new GameObject().AddComponent<LineRenderer>(); //ArrowBS2 = new GameObject().AddComponent<LineRenderer>();
        ForceB1 = new GameObject().AddComponent<LineRenderer>(); //ArrowBF1 = new GameObject().AddComponent<LineRenderer>();
        ForceB2 = new GameObject().AddComponent<LineRenderer>(); //ArrowBF2 = new GameObject().AddComponent<LineRenderer>();
        MomentB1 = new GameObject().AddComponent<LineRenderer>();// ArrowBM1 = new GameObject().AddComponent<LineRenderer>();
        MomentB2 = new GameObject().AddComponent<LineRenderer>();// ArrowBM2 = new GameObject().AddComponent<LineRenderer>();
        ShearC1 = new GameObject().AddComponent<LineRenderer>(); //ArrowCS1 = new GameObject().AddComponent<LineRenderer>();
        ShearC2 = new GameObject().AddComponent<LineRenderer>(); //ArrowCS2 = new GameObject().AddComponent<LineRenderer>();
        ForceC1 = new GameObject().AddComponent<LineRenderer>(); //ArrowCF1 = new GameObject().AddComponent<LineRenderer>();
        ForceC2 = new GameObject().AddComponent<LineRenderer>();// ArrowCF2 = new GameObject().AddComponent<LineRenderer>();
        MomentC1 = new GameObject().AddComponent<LineRenderer>(); //ArrowCM1 = new GameObject().AddComponent<LineRenderer>();
        MomentC2 = new GameObject().AddComponent<LineRenderer>(); //ArrowCM2 = new GameObject().AddComponent<LineRenderer>();
        initilizeLines(MomentB1); //initilizeArrow(MomentB1.gameObject, ArrowBM1);
        initilizeLines(MomentB2); //initilizeArrow(MomentB2.gameObject, ArrowBM2);
        initilizeLines(MomentC1); //initilizeArrow(MomentC1.gameObject, ArrowCM1);
        initilizeLines(MomentC2); //initilizeArrow(MomentC2.gameObject, ArrowCM2);
        initilizeLines(ShearC1); //initilizeArrow(ShearC1.gameObject, ArrowCS1);
        initilizeLines(ShearC2); //initilizeArrow(ShearC2.gameObject, ArrowCS2);
        initilizeLines(ShearB2); //initilizeArrow(ShearB2.gameObject, ArrowBS1);
        initilizeLines(ShearB1); //initilizeArrow(ShearB1.gameObject, ArrowBS2);
        initilizeLines(ForceC1); //initilizeArrow(ForceC1.gameObject, ArrowCF1);
        initilizeLines(ForceC2); //initilizeArrow(ForceC2.gameObject, ArrowCF2);
        initilizeLines(ForceB1); //initilizeArrow(ForceB1.gameObject, ArrowBF1);
        initilizeLines(ForceB2); //initilizeArrow(ForceB2.gameObject, ArrowBF2);
        initilizeLines(CornerB1);
        initilizeLines(CornerB2); 
        initilizeLines(CornerC1); 
        initilizeLines(CornerC2); 
        DrawCorner(startendB1, cornerB, BR, Mathf.PI, 10, CornerB1, cornerB1);
        DrawCorner(startendB2, cornerB, BR, Mathf.PI/2, 10, CornerB2, cornerB2);
        DrawCorner(startendC1, cornerC, CR, Mathf.PI, 10, CornerC1, cornerC1);
        DrawCorner(startendC2, cornerC, CR, Mathf.PI*3 / 2, 10, CornerC2, cornerC2);
        DrawForce(startendBF1, cornerB1, BR, Mathf.PI * 3 / 2,5, ForceB1);
        DrawForce(startendBF2, cornerB2, BR, Mathf.PI * 3 / 2, -5, ForceB2);
        DrawForce(startendCF1, cornerC1, CR, Mathf.PI/ 2, 5, ForceC1);
        DrawForce(startendCF2, cornerC2, CR, Mathf.PI/ 2, -5, ForceC2);
        DrawForce(startendBS1, cornerB1, BR, Mathf.PI, 5, ShearB1);
        DrawForce(startendBS2, cornerB2, BR, Mathf.PI, 5, ShearB2);
        DrawForce(startendCS1, cornerC1, CR, Mathf.PI, 5, ShearC1);
        DrawForce(startendCS2, cornerC2, CR, Mathf.PI, 5, ShearC2);
        DrawMoment(startendBM1, cornerB1, 7, BR,5, MomentB1);
        DrawMoment(startendBM2, cornerB2, 7, BR, -5, MomentB2);
        DrawMoment(startendCM1, cornerC1, 7, CR, 5, MomentC1);
        DrawMoment(startendCM2, cornerC2, 7, CR, 5, MomentC2);
    }
    void initilizeLines(LineRenderer Moment)
    {
        //setup line
        Moment.transform.SetParent(this.transform);
        Moment.startWidth = R / 4;
        Moment.endWidth = R / 4;
        Moment.positionCount = pointsN + 1;
    }
    void initilizeArrow(GameObject parent, LineRenderer Moment)
    {
        //setup line
        Moment.transform.SetParent(parent.transform);
        Moment.name="Tip";
        Moment.startWidth = R / 2;
        Moment.endWidth = R / 2;
        Moment.positionCount = 5;
    }
   /* private void DrawArrow(Vector3[] startend, float Ang, float force , LineRenderer Arrow) {
        float momentAng = force;
        startend[0] = new Vector3(Arrow.gameObject.transform.position.x , Arrow.gameObject.transform.position.y , Arrow.gameObject.transform.position.z);
        startend[1] = new Vector3(Arrow.gameObject.transform.position.x + Mathf.Sign(force) * 3 * Mathf.Sin(-Mathf.PI / 2 - momentAng - Ang), Arrow.gameObject.transform.position.y + Mathf.Sign(force) * 3 * Mathf.Cos(-Mathf.PI / 2 + -momentAng - Ang), Arrow.gameObject.transform.position.z);
        Vector3[] pointsP = new Vector3[pointsN];
        for (int i = 0; i < 25; i++)
        {
            pointsP[i] = new Vector3(corner.position.x + Mathf.Sign(force) * 3 * Mathf.Sin(-Mathf.PI / 2 + i * -momentAng / 25 - Ang), corner.position.y + Mathf.Sign(force) * radius * Mathf.Cos(-Mathf.PI / 2 + i * -momentAng / 25 - Ang), corner.position.z);
        }
    }*/
    private void DrawCorner(Vector3[] startend, Transform corner, float Ang, float initAng, float radius, LineRenderer Moment, Transform cornerend)
    {
        float momentAng = initAng + Ang;
        startend[0] = new Vector3(corner.position.x, corner.position.y, corner.position.z);
        startend[1] = new Vector3(corner.position.x + radius * Mathf.Sin(-Mathf.PI / 2 - momentAng), corner.position.y + radius * Mathf.Cos(-Mathf.PI / 2 + -momentAng), corner.position.z);
        //initiate all points on curve
        Vector3[] pointsP = new Vector3[pointsN];
        for (int i = 0; i < 25; i++)
        {
            pointsP[i] = new Vector3(corner.position.x + i*(radius/25) * Mathf.Sin(-Mathf.PI / 2 -momentAng ), corner.position.y + i * (radius / 25) * Mathf.Cos(-Mathf.PI / 2 - momentAng), corner.position.z);
        }
        Moment.SetPositions(pointsP);
        Moment.SetPosition(pointsN, startend[1]);Moment.material.color = new Color(0f, 0f, 0f, 1f);
        cornerend.position = startend[1];
    }
    private void DrawForce(Vector3[] startend, Transform corner, float Ang, float initAng, float force, LineRenderer Moment)
    {
        float momentAng = initAng + Ang;
        startend[0] = new Vector3(corner.position.x, corner.position.y, corner.position.z);
        startend[1] = new Vector3(corner.position.x + force * Mathf.Sin(-Mathf.PI / 2 - momentAng), corner.position.y + force * Mathf.Cos(-Mathf.PI / 2 + -momentAng), corner.position.z);
        //initiate all points on curve
        Vector3[] pointsP = new Vector3[pointsN];
        for (int i = 0; i < 25; i++)
        {
            pointsP[i] = new Vector3(corner.position.x + i * (force / 25) * Mathf.Sin(-Mathf.PI / 2 - momentAng), corner.position.y + i * (force / 25) * Mathf.Cos(-Mathf.PI / 2 - momentAng), corner.position.z);
        }
        Moment.SetPositions(pointsP);
        Moment.SetPosition(pointsN, startend[1]); Moment.material.color = new Color(0f, 1f, 0f, 1f);
        Vector3 newVector = new Vector3(0, 0, 0);
        if (Mathf.Sign(force) > 0) { newVector = startend[1] - startend[0]; }
        else { newVector = startend[0] - startend[1]; }
        print(newVector);
        GameObject arrow = Instantiate(myPrefab, startend[0], Quaternion.LookRotation(newVector, Vector3.forward));
        arrow.transform.parent = this.transform; 
    }
    private void DrawMoment(Vector3[] startend, Transform corner,float radius, float Ang, float force, LineRenderer Moment) {
        float momentAng = force;
        startend[0] = new Vector3(corner.position.x + Mathf.Sign(force) * radius* Mathf.Sin(-Mathf.PI / 2 - Ang), corner.position.y+ Mathf.Sign(force) * radius * Mathf.Cos(-Mathf.PI / 2 - Ang), corner.position.z);
        startend[1] = new Vector3(corner.position.x + Mathf.Sign(force) * radius * Mathf.Sin(-Mathf.PI / 2 - momentAng - Ang), corner.position.y + Mathf.Sign(force) * radius * Mathf.Cos(-Mathf.PI / 2 + -momentAng - Ang), corner.position.z);
        //initiate all points on curve
        Vector3[] pointsP = new Vector3[pointsN];
        for (int i = 0; i < 25; i++)
        {
            pointsP[i] = new Vector3(corner.position.x + Mathf.Sign(force) * radius * Mathf.Sin(-Mathf.PI / 2 + i * -momentAng / 25 - Ang), corner.position.y + Mathf.Sign(force) * radius * Mathf.Cos(-Mathf.PI / 2 + i * -momentAng / 25 - Ang), corner.position.z);
        }
        Moment.SetPositions(pointsP);
        Moment.SetPosition(pointsN, startend[1]); Moment.material.color = new Color(0f, 1f, 0f, 1f);
        Vector3 newVector = new Vector3(0, 0, 0);newVector = corner.position - startend[1];
        //if (Mathf.Sign(force) > 0) {  }
        //else { newVector = startend[0] - startend[1]; }
        //print(newVector);
        GameObject arrow = Instantiate(myPrefab, startend[1], Quaternion.LookRotation(newVector, -Mathf.Sign(momentAng) * corner.forward));
        arrow.transform.parent = this.transform;
        //GameObject arrow = Instantiate(myPrefab, startend[1], Quaternion.identity);
        //arrow.transform.LookAt(corner, -Mathf.Sign(momentAng) * corner.up);
        //arrow.transform.parent = this.transform;
        //corner.transform.Find("Tip").transform.position = startend[1];
        //print(momentAng);
        //if (momentAng > -1.6f && momentAng < 0f) { corner.transform.Find("Tip").transform.LookAt(corner, -Mathf.Sign(momentAng) * corner.up); }
        //else { corner.transform.Find("Tip").transform.LookAt(corner, Mathf.Sign(momentAng) * corner.up); }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
