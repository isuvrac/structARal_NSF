using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Pressures
{
    public float windward_base;
    public float windward_side_top;
    public  float leeward_side;
    public float windward_roof;
    public float leeward_roof;
}

public class ForceUpdate : MonoBehaviour
{
    Pressures[] pressures_s;
    Pressures pressures;
    float base_height = 89 + 2f / 12;
    float roof_height = 20 + 4.5f / 12;
    float MOD_ELASTICITY = 2.016e8f;
    float MOM_OF_INERTIA = 2334;
    private LineRenderer towerL, towerR, Moment;
    private Vector3[] pointsPL, pointsPR;
    private int pointsN = 25;
    private Vector3[] startendL, startendR;
    [SerializeField]
    Transform refL1, refL2, refR3, refR4, refM;
    float f1_h = 17.75f;
    float f2_h = 57.5f;
    float f3_h = 71.5f;
    float f4_h = 89 + 2f / 12;
    public bool WindMode=true;

    [SerializeField]
    public float seismicSs, seismicS1, shear, moment, DefLabel, Max_P, Min_P;
    public float[] WindSeismic = new float[4];
    private float[] def;
    static float t = 0.0f;
    float R;
    private Vector3[] pointsM;
    private Vector3[] startendM;
    public float width = 1; 

    // Start is called before the first frame update
    void Start()
    {
        pressures_s = new Pressures[5];
        def = new float[pointsN];
        // Drawlines(WindVelocity);
    }
    public void StartUp()
    {
        pointsPL = new Vector3[pointsN];
        pointsPR = new Vector3[pointsN];
        startendL = new Vector3[2];
        startendL[0] = refL1.position;
        startendL[1] = refL2.position;
        startendR = new Vector3[2];
        startendR[0] = refR3.position;
        startendR[1] = refR4.position;

        //setup line
        towerL = new GameObject().AddComponent<LineRenderer>();
        towerL.transform.SetParent(this.transform);
        towerL.startWidth = 1f* width;
        towerL.endWidth = 1f * width;
        towerL.positionCount = pointsN + 1;
        towerL.material.color = new Color(0f, 0f, 1f);


        //setup line
        towerR = new GameObject().AddComponent<LineRenderer>();
        towerR.transform.SetParent(this.transform);
        towerR.startWidth = 1f * width;
        towerR.endWidth = 1f * width;
        towerR.positionCount = pointsN + 1;
        towerR.material.color = new Color(0f, 0f, 1f);

        R = Mathf.Abs(refL2.transform.localPosition.x - refR4.transform.localPosition.x);
        print(R);
        pointsM = new Vector3[pointsN];
        startendM = new Vector3[2];
        Moment = new GameObject().AddComponent<LineRenderer>();
        Moment.transform.SetParent(this.transform);
        Moment.startWidth = R /8 * width;
        Moment.endWidth = R /8 * width;
        Moment.positionCount = pointsN + 1;
        Moment.material.color = new Color(0f, 1f, 0f, 1f);
    }

    public void DestryLines() {
        Destroy(towerL);
        Destroy(towerR);
        Destroy(Moment);
    }

    public void Drawlines(float input, float ratio)
    {
        //initiate all points on curve
        float resy = (startendL[1].y - startendL[0].y) / 24;
        if (WindMode) { def = calculateWindForce(input); }
        else { def = calculateSeismic(input); }
        for (int i = 0; i < pointsN; i++)
        {
            pointsPL[i] = new Vector3(startendL[0].x - def[pointsN - i - 1], startendL[0].y + resy * i, startendL[0].z);
        }
        towerL.SetPositions(pointsPL);
        towerL.SetPosition(pointsN, startendL[1]);

        resy = (startendR[1].y - startendR[0].y) / 24;
        for (int i = 0; i < pointsN; i++)
        {
            pointsPR[i] = new Vector3(startendR[0].x - def[pointsN - i - 1], startendR[0].y + resy * i, startendR[0].z);
        }
        towerR.SetPositions(pointsPR);
        towerR.SetPosition(pointsN, startendR[1]);
        DefLabel = Mathf.Round((def[pointsN - 1] * 12 / 10) * 100) / 1000;
        
        DrawMomentlines(startendM, refM, moment, R * width, Moment,ratio);
    }

    void DrawMomentlines(Vector3[] startend, Transform reactionM, float moment, float radius, LineRenderer Moment, float ratio)
    {
        float momentAng = moment / ratio;
        startend[0] = new Vector3(reactionM.position.x +Mathf.Sign(ratio)*radius, reactionM.position.y, reactionM.position.z);
        startend[1] = new Vector3(reactionM.position.x + Mathf.Sign(ratio) * radius * Mathf.Sin(-Mathf.PI / 2 - momentAng), reactionM.position.y+Mathf.Sign(ratio) * radius * Mathf.Cos(-Mathf.PI / 2 + -momentAng), reactionM.position.z);
        //initiate all points on curve
        Vector3[] pointsP = new Vector3[pointsN];
        for (int i = 0; i < 25; i++)
        {
            pointsP[i] = new Vector3(reactionM.position.x + Mathf.Sign(ratio) * radius * Mathf.Sin(-Mathf.PI / 2 + i * -momentAng / 25), reactionM.position.y + Mathf.Sign(ratio) * radius * Mathf.Cos(-Mathf.PI / 2 + i * -momentAng / 25), reactionM.position.z);
        }
        Moment.SetPositions(pointsP);
        Moment.SetPosition(pointsN, startend[1]);
        reactionM.transform.Find("Tip").transform.position = startend[1];
        print(momentAng);
        if (momentAng > -1.6f && momentAng < 0f) { reactionM.transform.Find("Tip").transform.LookAt(reactionM, -Mathf.Sign(momentAng) * reactionM.up); }
        else { reactionM.transform.Find("Tip").transform.LookAt(reactionM, Mathf.Sign(momentAng) * reactionM.up); }
    }


    public void DrawAnimatedLine()
    {
        float resy = (startendL[1].y - startendL[0].y) / 24;
        float[] lerpdef = new float[pointsN];
        float[] range = def;
        for (int i = 0; i < pointsN; i++)
        {
            lerpdef[i] = Mathf.Lerp(-range[i], range[i], t);
        }

        for (int i = 0; i < pointsN; i++)
        {
            pointsPL[i] = new Vector3(startendL[0].x - lerpdef[pointsN - i - 1], startendL[0].y + resy * i, startendL[0].z);
        }
        towerL.SetPositions(pointsPL);
        towerL.SetPosition(pointsN, startendL[1]);

        resy = (startendR[1].y - startendR[0].y) / 24;
        for (int i = 0; i < pointsN; i++)
        {
            pointsPR[i] = new Vector3(startendR[0].x - lerpdef[pointsN - i - 1], startendR[0].y + resy * i, startendR[0].z);
        }
        towerR.SetPositions(pointsPR);
        towerR.SetPosition(pointsN, startendR[1]);
        DefLabel = Mathf.Round((lerpdef[pointsN - 1] * 12 / 10) * 1000) / 1000;

        t += 3 * Time.deltaTime;
        if (t > 1.0f)
        {
            for (int i = 0; i < pointsN; i++)
            {
                range[i] = -def[i];
            }t = 0.0f;
        }
        
    }
    float[] calculateSeismic(float input)
    {

        int Index = (int)input;
        float[] def = new float[pointsN];
        for (int i = 0; i < pointsN; i++)
        {
            def[i] = (base_height / (pointsN - 1)) * i;
        }

        float[] Ss_vals = new float[] { 0.05f, 0.25f, 0.5f, 0.75f, 1, 1.25f, 2, 3 };
        float[] S1_vals = new float[] { 0.05f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.8f, 1.2f };
        float[] V_vals = new float[] { 44.76f, 223.8f, 373f, 503.55f, 596.8f, 699.37f, 1118.99f, 1678.49f };
        float[] F1_vals = new float[] { 0.74f, 3.72f, 6.19f, 8.36f, 9.91f, 11.61f, 18.58f, 27.87f };
        float[] F2_vals = new float[] { 8.69f, 43.43f, 72.39f, 97.73f, 115.82f, 135.73f, 217.17f, 325.76f };
        float[] F3_vals = new float[] { 13.66f, 68.29f, 113.81f, 153.64f, 182.1f, 213.39f, 341.43f, 512.14f };
        float[] F4_vals = new float[] { 21.67f, 108.36f, 180.6f, 243.81f, 288.96f, 338.63f, 541.81f, 812.71f };

        float F1 = F1_vals[Index];
        float F2 = F2_vals[Index];
        float F3 = F3_vals[Index];
        float F4 = F4_vals[Index];
        float V = V_vals[Index];
        seismicSs = Ss_vals[Index];
        seismicS1 = S1_vals[Index];

        shear = Mathf.Round(V);
        WindSeismic[0] = Mathf.Round(F1 * 100) / 100;
        WindSeismic[1] = Mathf.Round(F2 * 100) / 100;
        WindSeismic[2] = Mathf.Round(F3 * 100) / 100;
        WindSeismic[3] = Mathf.Round(F4 * 100) / 100;

        float Tmoment = f1_h * F1 + f2_h * F2 + f3_h * F3 + f4_h * F4;

        moment = Mathf.Round(Tmoment * 10) / 10;


        int resolution = def.Length;

        for (int i = 0; i < resolution; ++i)
        {
            float x = def[i] - def[0];
            float x2 = x * x;
            float defl_sum = 0;
            if (x < f1_h)
            {
                defl_sum += (53.25f - x) * F1 * x2 / 6;
            }
            else
            {
                defl_sum += (3 * x - f1_h) * 52.5f * F1;
            }
            if (x < f2_h)
            {
                defl_sum += (172.5f - x) * F2 * x2 / 6;
            }
            else
            {
                defl_sum += (3 * x - f2_h) * 551.04f * F2;
            }
            if (x < f3_h)
            {
                defl_sum += (214.5f - x) * F3 * x2 / 6;
            }
            else
            {
                defl_sum += (3 * x - f3_h) * 852.04f * F3;
            }
            if (x < f4_h)
            {
                defl_sum += (267.6f - x) * F4 * x2 / 6;
            }
            else
            {
                defl_sum += (3 * x - f4_h) * 1326.11f * F4;
            }
            //        if (x < 109.54) {
            //            defl_sum += (328.62 - x) * F5 * x2 / 6;
            //        }
            //        else {
            //            defl_sum += (3*x - 109.54) * 1999.84 * F5;
            //        }
            float defl_ft = 1000 * defl_sum / (MOD_ELASTICITY * MOM_OF_INERTIA);
            def[i] = defl_ft * 10;
        }

        return def;


    }

    float[] calculateWindForce(float velocity)
    {

        calculatePressure(velocity);

        float[] def = new float[pointsN];
        for (int i = 0; i < pointsN; i++)
        {
            def[i] = (base_height / (pointsN - 1)) * i;
        }

        // convenience
        float h1 = base_height;
        float h2 = roof_height;
        float ww1 = pressures.windward_base;
        float ww2 = pressures.windward_side_top;
        float wl = -pressures.leeward_side;

        float h1_2 = h1 * h1;
        float h1_3 = h1_2 * h1;

        // Calculate shear, axial, and moment
        shear = Mathf.Round(16 * (h1 * (ww1 / 2 + ww2 / 2 + wl)) / 1000);
        moment = Mathf.Round(10 * 16 * (h1_2 / 2 * (ww1 + wl) + h1_2 / 3 * (ww2 - ww1)) / 1000) / 10;


        // Calculate deflection
        int resolution = def.Length;
        for (int i = 0; i < resolution; ++i)
        {
            float x = def[i] - def[0];
            float x2 = x * x;
            float x3 = x2 * x;

            float defl1 = 0, defl2 = 0, defl3 = 0;
            if (x <= h1)
            {
                float defl13_common = 4 * h1 * x - x2 - 6 * h1_2;
                defl1 = (2 * ww1 * x2 / 3) * defl13_common;
                defl3 = (2 * wl * x2 / 3) * defl13_common;

                defl2 = (2 * (ww2 - ww1) * x2 / 15) * (10 * h1 * x - x3 / h1 - 20 * h1_2);
            }
            else if (x <= (h1 + h2))
            {
                defl1 = 2 * ww1 * h1_3 * (-4 * x + h1) / 3;
                defl2 = 8 * h1_3 * (ww2 - ww1) * (h1 / 15 - x / 4);
                defl3 = 2 * wl * h1_3 * (-4 * x + h1) / 3;
            }
            float sum_defl = defl1 + defl2 + defl3;
            float defl_ft = sum_defl / (MOD_ELASTICITY * MOM_OF_INERTIA);
            def[i] = defl_ft * 100;

        }

        return def;
    }

    void calculatePressure(float velocity)
    {

        float v2 = velocity * velocity;
        pressures.windward_base = 0.000843f * v2;
        pressures.windward_side_top = 0.0014f * v2;
        pressures.leeward_side = -0.00093f * v2;
        pressures.windward_roof = 0.00128f * v2;
        pressures.leeward_roof = -0.00112f * v2;
        Max_P = (Mathf.Round((pressures.windward_side_top - pressures.leeward_side) * 10) / 10);
        Min_P = (Mathf.Round((pressures.windward_base - pressures.leeward_side) * 10) / 10);
    }
}



