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
    private LineRenderer towerL, towerR;
    private Vector3[] pointsPL, pointsPR;
    private int pointsN = 25;
    private Vector3[] startendL, startendR;
    [SerializeField]
    Transform refL1, refL2, refR3, refR4;
    float WindVelocity;
    [SerializeField]
    GameObject DefLable, Min_P, Max_P, MomentL,ShearL;
    public float shear;
    public float moment;




    // Start is called before the first frame update
    void Start()
    {
        WindVelocity = 75;
        pressures_s = new Pressures[5];
        pointsPL= new Vector3[pointsN];
        pointsPR= new Vector3[pointsN];
        startendL = new Vector3[2];
        startendL[0] = refL1.position;
        startendL[1] = refL2.position;
        startendR = new Vector3[2];
        startendR[0] = refR3.position;
        startendR[1] = refR4.position;

        //setup line
        towerL = new GameObject().AddComponent<LineRenderer>();
        towerL.transform.SetParent(this.transform);
        towerL.startWidth = 1f;
        towerL.endWidth = 1f;
        towerL.positionCount = pointsN + 1;
        towerL.material.color = new Color(0f, 0f, 1f);
       

        //setup line
        towerR = new GameObject().AddComponent<LineRenderer>();
        towerR.transform.SetParent(this.transform);
        towerR.startWidth = 1f;
        towerR.endWidth = 1f;
        towerR.positionCount = pointsN + 1;
        towerR.material.color = new Color(0f, 0f, 1f);

        Drawlines(WindVelocity);
    }
    public void Drawlines(float WindVelocity)
    {
        //initiate all points on curve
        //float resx = (startendL[1].x - startendL[0].x) / 24;
        float resy = (startendL[1].y - startendL[0].y) / 24;
        float[] def = calculateWindForce(WindVelocity);

        for (int i = 0; i < pointsN; i++)
        {
            pointsPL[i] = new Vector3(startendL[0].x - def[pointsN-i-1] *100, startendL[0].y + resy * i, startendL[0].z); 
        }
        //pointsPL = calculateWindForce(WindVelocity, pointsPL);
        towerL.SetPositions(pointsPL);
        towerL.SetPosition(pointsN, startendL[1]);

        resy = (startendR[1].y - startendR[0].y) / 24;
        for (int i = 0; i < pointsN; i++)
        {
            pointsPR[i] = new Vector3(startendR[0].x - def[pointsN - i - 1] * 100, startendR[0].y + resy * i, startendR[0].z);
        }
       // pointsPR = calculateWindForce(WindVelocity, pointsPR);
        towerR.SetPositions(pointsPR);
        towerR.SetPosition(pointsN, startendR[1]);

        MomentL.GetComponent<TextMesh>().text = moment.ToString() +" k-ft";
        ShearL.GetComponent<TextMesh>().text = shear.ToString() + " k";
        DefLable.GetComponent<TextMesh>().text = (Mathf.Round(def[pointsN - 1]*12*1000)/1000).ToString() + " in";
    }


    float[] calculateWindForce(float velocity)
    {

        calculatePressure(velocity);

        float[] def = new float[pointsN];
        for (int i = 0; i < pointsN; i++)
        {
            def[i] = (base_height/(pointsN-1))*i;
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
         shear =Mathf.Round( 16  * (h1 * (ww1 / 2 + ww2 / 2 + wl))/ 1000);
         moment =Mathf.Round(10* 16* (h1_2 / 2 * (ww1 + wl) + h1_2 / 3 * (ww2 - ww1))/ 1000)/10;

        
        // Calculate deflection
        int resolution = def.Length;
        for (int i = 0; i < resolution; ++i)
        {
            float x = def[i] - def[0];
            float x2 = x * x;
            float x3 = x2 * x;

            float defl1 = 0, defl2 = 0, defl3=0;
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
            def[i] = defl_ft;
            
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
        Max_P.GetComponent<TextMesh>().text =(Mathf.Round((pressures.windward_side_top - pressures.leeward_side)*10)/10).ToString() + " psf";
        Min_P.GetComponent<TextMesh>().text = (Mathf.Round((pressures.windward_base - pressures.leeward_side)*10)/10).ToString() + " psf";
    }
}



