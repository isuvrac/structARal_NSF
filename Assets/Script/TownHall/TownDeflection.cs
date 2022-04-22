using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDeflection : MonoBehaviour
{
    private LineRenderer DeflineAB, DeflineCD, DeflineEF, DeflineBC, DeflineCE;
    private Vector3[] startendAB, startendCD, startendEF, startendBC, startendCE;
    private int pointsN = 8;
    float resx;
    float resy ;
    [SerializeField]
    Transform refPA, refPB, refPC, refPD, refPE, refPF;

    float[] array1 = new float[5] { 0, 1.15550E-05f, -1.37282E-04f, -1.59049E-04f, +5.37344E-05f };
    float[] array2 = new float[5] { 0, +2.13136E-05f, -2.45540E-04f, -1.20251E-04f, +1.94406E-05f };
    float[] array3 = new float[5] { 0, -6.30189E-06f, +7.84770E-05f, +1.09172E-04f, -4.33148E-05f };
    float[] array4 = new float[5] { 0, -1.15516E-05f, +1.37205E-04f, +1.59369E-04f, -5.70211E-05f };
    float[] array5 = new float[5] { 0, -2.12686E-05f, +2.44974E-04f, +1.20283E-04f, -2.60839E-05f };
    float[] array6 = new float[5] { -0.00000111121f, +0.0000290842f, -0.0000383479f, -0.00230318f, -0.0000111888f };
    float[] array7 = new float[5] { -0.00000107914f, +0.0000437637f, -0.000420113f, -0.000158754f, -0.0000118881f };
    float width = 16.67f;float height = 12;


    // Start is called before the first frame update
    void Start()
    {
        resx = width/ (pointsN-1);
        resy = height/(pointsN - 1);
        startendAB = new Vector3[2];
        startendAB[0] = refPA.position; startendAB[1] = refPB.position;
        startendCD = new Vector3[2];
        startendCD[0] = refPD.position; startendCD[1] = refPC.position;
        startendEF = new Vector3[2];
        startendEF[0] = refPF.position; startendEF[1] = refPE.position;
        startendBC = new Vector3[2];
        startendBC[0] = refPB.position; startendBC[1] = refPC.position;
        startendCE = new Vector3[2];
        startendCE[0] = refPC.position; startendCE[1] = refPE.position;

        DeflineAB = new GameObject().AddComponent<LineRenderer>();
        DeflineCD = new GameObject().AddComponent<LineRenderer>();
        DeflineEF = new GameObject().AddComponent<LineRenderer>();
        DeflineBC = new GameObject().AddComponent<LineRenderer>();
        DeflineCE = new GameObject().AddComponent<LineRenderer>();
        initilizeLines(DeflineAB);
        initilizeLines(DeflineCD);
        initilizeLines(DeflineEF);
        initilizeLines(DeflineBC);
        initilizeLines(DeflineCE);

    }
    void initilizeLines(LineRenderer Defline)
    {
        //setup line
        Defline.transform.SetParent(this.transform);
        Defline.startWidth = Mathf.Abs(refPA.localPosition.x - refPD.localPosition.x) / 20;
        Defline.endWidth = Mathf.Abs(refPA.localPosition.x - refPD.localPosition.x) / 20;
        Defline.positionCount = pointsN + 1;
        Defline.material.color = new Color(0f, 0f, 1f, 1f);
    }


    public void drawLines(float x1, float x2, float L, float F, float delta) {
        DrawVlines(DeflineAB, startendAB, L, delta);
        DrawVlines(DeflineCD, startendCD, L, delta);
        DrawVlines(DeflineEF, startendEF, L, delta);
        DrawHlines(DeflineBC, startendBC,  x1, x2,L, F, delta);
        DrawHlines(DeflineCE, startendCE,  x1, x2,L, F, delta);
    }
    void DrawVlines(LineRenderer Defline, Vector3[] startend,float L, float delta)
    {
        float def_scale = 200;
        float[] def = new float[pointsN];
        
        for (int i = 0; i < pointsN; i++)
        {
            def[i] = resy * i;
        }
        //def= updateDef(def, x1,  x2,  L, F, delta, 200);
        float[] zero = new float[5] { 0, 0, 0, 0, 0 };
        float[] Def = new float[pointsN];
        //beam AB
        if (Defline == DeflineAB)
        {
            print("AB");
            Def = evalDeflection(-def_scale * delta / -0.001785335f,array1, -def_scale * (L + 3) / 5, array2, def);
        }
        //DC
        else if (Defline == DeflineCD)
        {
            print("CD");
            Def = evalDeflection(-def_scale * delta / 0.001677771f,array3, 0, zero,   def);
        }
        //FE
        else if(Defline == DeflineEF)
        {
            print("EF");
            Def = evalDeflection(-def_scale * delta / 0.001651762f, array4, -def_scale * (L + 3) / 5, array5, def);
        }


        Vector3[] PointP = new Vector3[pointsN];
        for (int i = 0; i < pointsN; i++)
        {
            print(Def[i]);
            PointP[i] =new Vector3(startend[0].x+Def[i], startend[0].y+ i*((startend[1].y-startend[0].y)/(pointsN-1)), startend[0].z); ;
        }
        Defline.SetPositions(PointP);
        Defline.SetPosition(pointsN, startend[1]);
    }
    void DrawHlines(LineRenderer Defline, Vector3[] startend, float x1, float x2, float L, float F, float delta)
    {
        float[] def = new float[pointsN];
        
        for (int i = 0; i < pointsN; i++)
        {
            def[i] = resx * i;
        }
        float[] Def = new float[pointsN];
        float[] zero = new float[5] { 0, 0, 0, 0, 0 };
        float A, B, C, D;
        if (x1 <= width)
        {
            if (x2 > width)
            {
                // Case 1
                A = (width - x1) / width;
                B = (x2 - width) / width;
                C = (x2 - width) / width;
                D = (width - x1) / width;
            }
            else
            { // x2 <= width
              // Case 2
                A = (x2 - x1) / width;
                B = 0;
                C = 0;
                D = (x2 - x1) / width;
            }
        }
        else
        { // x1 > width && x2 > width
            A = 0;
            B = (x2 - x1) / width;
            C = (x2 - x1) / width;
            D = 0;
        }

        // Beam BC
        float beam1_factor = 1 + 0.5f * L * (A - B) / 3 + 0.2f * F / 5;
        beam1_factor *= 200;
        Def = evalDeflection(0, zero, beam1_factor, array6, def);

        // Beam CE
        float beam2_factor = 1 + 0.5f * L * (C - D) / 3 + 0.18f * F / 5;
        beam2_factor *= 200;
        Def = evalDeflection(0, zero, beam2_factor, array7, def);

        Vector3[] PointP = new Vector3[pointsN];
        for (int i = 0; i < pointsN; i++)
        {
            PointP[i] = new Vector3(startend[0].x + i * ((startend[1].x - startend[0].x) / (pointsN - 1)), startend[0].y+Def[i] ,startend[0].z); ;
        }
        Defline.SetPositions(PointP);
        Defline.SetPosition(pointsN, startend[1]);
    }

    private float[] evalDeflection(float coe1, float[] a1, float coe2, float[] a2, float[] def) {
        float[] Def = new float[pointsN];
        for (int i = 0; i < def.Length; ++i)
        {
            float x = def[i];
            float x2 = x * x;
            float x3 = x2 * x;
            float x4 = x2 * x2;
            float value = 0;

            float poly_evaluated = a1[0] * x4 +
                                    a1[1] * x3 +
                                    a1[2] * x2 +
                                    a1[3] * x +
                                    a1[4];
            value += poly_evaluated * coe1;
            poly_evaluated = a2[0] * x4 +
                        a2[1] * x3 +
                        a2[2] * x2 +
                        a2[3] * x +
                        a2[4];
            value += poly_evaluated * coe2;
            Def[i] = value;
        }
        return Def;
    }

    private float[] updateDef(float[] def, float x1, float x2, float L, float F, float delta, float def_scale) {
        float[] Def = new float[pointsN];
        float[] zero = new float[5] { 0, 0, 0, 0, 0 };       
        float A, B, C, D;
        if (x1 <= width)
        {
            if (x2 > width)
            {
                // Case 1
                A = (width - x1) / width;
                B = (x2 - width) / width;
                C = (x2 - width) / width;
                D = (width - x1) / width;
            }
            else
            { // x2 <= width
              // Case 2
                A = (x2 - x1) / width;
                B = 0;
                C = 0;
                D = (x2 - x1) / width;
            }
        }
        else
        { // x1 > width && x2 > width
            A = 0;
            B = (x2 - x1) / width;
            C = (x2 - x1) / width;
            D = 0;
        }

        // Beam BC
        float beam1_factor = 1+ 0.5f * L * (A - B) / 3+ 0.2f * F / 5;
        beam1_factor *= def_scale;
        Def = evalDeflection(0,zero,beam1_factor, array6, def);

        // Beam CE
        float beam2_factor = 1+ 0.5f * L * (C - D) / 3+ 0.18f * F / 5;
        beam2_factor *= def_scale;
        Def = evalDeflection(0, zero, beam2_factor, array7,def);
        return Def;

    }
}
    
