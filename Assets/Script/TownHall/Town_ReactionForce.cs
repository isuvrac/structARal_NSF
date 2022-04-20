using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_ReactionForce : MonoBehaviour
{
    struct ReactionForce
    {
        public float theta_B;
        public float theta_C;
        public float theta_E;
        public float delta;

        public float M_AB;
        public float M_BA;
        public float M_DC;
        public float M_CD;
        public float M_FE;
        public float M_EF;
        public float M_BC;
        public float M_CB;
        public float M_CE;
        public float M_EC;

        public float F_AB;
        public float F_BA;
        public float F_DC;
        public float F_CD;
        public float F_FE;
        public float F_EF;
        public float F_BC;
        public float F_CB;
        public float F_CE;
        public float F_EC;

        public float V_AB;
        public float V_BA;
        public float V_DC;
        public float V_CD;
        public float V_FE;
        public float V_EF;
        public float V_BC;
        public float V_CB;
        public float V_CE;
        public float V_EC;
    }
    ReactionForce[] reactionForces_s;
    ReactionForce reactionForces;
    string _statusMsg;

    //input
    float k_1 = 11393.51f;
    float k_2 = 11393.51f;
    float k_3 = 11393.51f;
    float k_4 = 138404.186f;
    float k_5 = 138404.186f;
    float width = 16.67f;
    float height = 12;
    float inputD;
    float inputF;
    float inputL;
    float inputx1;
    float inputx2;
    [SerializeField]
    TownWindForce townWindForce;
    [SerializeField]
    TownLiveLoad townLiveLoad;


    private void OnGUI()
    {
        GUI.color = Color.white;
        GUI.skin.label.fontSize = 30;
        GUI.Label(new Rect(200, 10, 800, 400), _statusMsg);
    }
    // Start is called before the first frame update
    void Start()
    {
        reactionForces_s = new ReactionForce[34];
        calculateForces(0,3,0,0, 30);
    }

    // Update is called once per frame
    void Update()
    {
        _statusMsg = "F_AB" + reactionForces.F_AB + " F_DC" + reactionForces.F_DC + " F_FE" + reactionForces.F_FE + " V_AB" + reactionForces.V_AB + " V_DC" + reactionForces.V_DC + " V_FE" + reactionForces.V_FE
            + " M_AB" + reactionForces.M_AB + " M_DC" + reactionForces.M_DC + " M_FE" + reactionForces.M_FE;
        inputD=3;
        inputF=townWindForce.WindForce;
        inputL=townLiveLoad.liveload;
        inputx1= townLiveLoad.start.x;
        inputx2 = townLiveLoad.end.x;
    }


    private void calculateForces(float L, float D, float F, float x1, float x2)
    { // fixed end moments
        float MF_BC = 0, MF_CB = 0, MF_CE = 0, MF_EC = 0;
        if (x1 <= width && x2 >= width)
        {
            MF_BC = L * Mathf.Pow((width - x1),3) * (width + 3 * x1) / 3334.67f;
            MF_CB = -L * Mathf.Pow( (width - x1),2) * (1667.33f - (133.36f * (width - x1)) + 3 * Mathf.Pow( (width - x1),2)) / 3334.67f;
            MF_CE = L * Mathf.Pow( (x2 - width),2) * (3 * Mathf.Pow((x2 - width),2) + 3890.44f - 133.36f * x2) / 3334.67f;
            MF_EC = -L * Mathf.Pow((x2 - width),3) * (116.69f - 3 * x2) / 3334.67f;
        }
        else if (x1 <= width && x2 <= width)
        {
            MF_BC = L * Mathf.Pow((x2),2) * (3 * Mathf.Pow((x2),2) - 133.36f * x2 + 1667.33f) / 3334.67f
                    + L * Mathf.Pow( (width - x1),3) * (66.68f - (50.01f - 3 * x1)) / 3334.67f
                    - 23.157f * L;
            MF_CB = -L * Mathf.Pow((x2),3) * (66.68f - 3 * x2) / 3334.67f
                    - L * Mathf.Pow((width - x1),2) * (1667.33f - 133.36f * (width - x1) + 3 * Mathf.Pow(  (width - x1),2)) / 3334.67f
                    + 23.157f * L;
            MF_CE = 0;
            MF_EC = 0;
        }
        else if (x1 >= width && x2 >= width)
        {
            MF_BC = 0;
            MF_CB = 0;
            MF_CE = L * Mathf.Pow((x2 - width),2) * (3 * Mathf.Pow((x2 - width),2) - 133.36f * (x2 - width) + 1667.33f) / 3334.67f
                    + L * Mathf.Pow((33.34f - x1),3) * (66.68f - 3 * (33.34f - x1)) / 3334.67f
                    - 23.157f * L;
            MF_EC = -L * Mathf.Pow( (x2 - width),3) * (66.68f - 3 * (x2 - width)) / 3334.67f
                    - L * Mathf.Pow((33.34f - x1),2) * (1667.33f - 133.36f * (33.34f - x1) + 3 * Mathf.Pow( (33.34f - x1),2)) / 3334.67f
                    + 23.157f * L;
        }
        MF_BC += 23.157f * D;
        MF_CB -= 23.157f * D;
        MF_CE += 23.157f * D;
        MF_EC -= 23.157f * D;

        float VF_BC = 0, VF_CB = 0, VF_CE = 0, VF_EC = 0;
        if (x1 <= width && x2 >= width)
        {
            VF_BC = (L * (Mathf.Pow (width - x1,2) / 2) + MF_CB + MF_BC) / width;
            VF_CB = (L * (width - x1) * (x1 + ((width - x1) / 2)) - MF_BC - MF_CB) / width;
            VF_CE = (L * (x2 - width) * (33.34f - x2 + ((x2 - width) / 2)) + MF_EC + MF_CE) / width;
            VF_EC = ((L * Mathf.Pow(x2 - width,2) / 2) - MF_CE - MF_EC) / width;
        }
        else if (x1 <= width && x2 <= width)
        {
            VF_BC = (L * (x2 - x1) * (width - x1 - ((x2 - x1) / 2)) + MF_BC + MF_CB) / width;
            VF_CB = (L * (x2 - x1) * (x2 - ((x2 - x1) / 2)) - MF_BC - MF_CB) / width;
            VF_CE = 0;
            VF_EC = 0;
        }
        else if (x1 >= width && x2 >= width)
        {
            VF_BC = 0;
            VF_CB = 0;
            VF_CE = (L * (x2 - x1) * (33.34f - x1 - ((x2 - x1) / 2)) + MF_CE + MF_EC) / width;
            VF_EC = (L * (x2 - x1) * ((x1 - width) + ((x2 - x1) / 2)) - MF_CE - MF_EC) / width;
        }

        float[] A_data= new float [16]{
        4*k_1 + 4*k_4, 2*k_4,             0,           k_1/2,
        2*k_4,         4*k_4+4*k_2+4*k_5, 2*k_5,       k_2/2,
        0,             2*k_5,             4*k_5+4*k_3, k_3/2,
        k_1/2,         k_2/2,             k_2/2,       (k_1+k_2+k_3)/12
    };
        float[] b_data=new float [4] { -MF_BC, -MF_CB - MF_CE, -MF_EC, F };

        Matrix4x4 A = Matrix4x4.identity;
        Matrix4x4 b = Matrix4x4.zero;
        for (int i = 0; i < A_data.Length; i++) { int k = i / 4; int l = i % 4; A[k, l] = A_data[i];}
        //Vector4 b = new Vector4(-MF_BC, -MF_CB - MF_CE, -MF_EC, F);
        b[0, 0] = -MF_BC;
        b[1, 0] = -MF_CB - MF_CE;
        b[2, 0] = -MF_EC;
        b[3, 0] = F;
        print(A_data.Length);
        print(b);
        print(A);
        //float[,] b = new float[4, 1];
        //for (int i = 0; i < b_data.Length; i++) { int k = i / 4; int l = i % 4; b[k, l] = b_data[i]; }
        Matrix4x4 x = A.inverse * b;
        print(x);
         //bool has_solution = cv::solve(A, b, x);
         //assert(has_solution);
         float theta_B = x[0,0];
         float theta_C = x[1,0];
         float theta_E = x[2,0];
         float delta = x[3,0];

         // calculate final values
         reactionForces.theta_B = theta_B;
         reactionForces.theta_C = theta_C;
         reactionForces.theta_E = theta_E;
         reactionForces.delta = delta;

        // moments
        reactionForces.M_AB = 2 * k_1 * (theta_B + delta / 4);
        reactionForces.M_BA = 2 * k_1 * (2 * theta_B + delta / 4);
        reactionForces.M_BC = 2 * k_4 * (2 * theta_B + theta_C) + MF_BC;
        reactionForces.M_CB = 2 * k_4 * (theta_B + 2 * theta_C) + MF_CB;
        reactionForces.M_CD = 2 * k_2 * (2 * theta_C + delta / 4);
        reactionForces.M_DC = 2 * k_2 * (theta_C + delta / 4);
        reactionForces.M_CE = 2 * k_5 * (2 * theta_C + theta_E) + MF_CE;
        reactionForces.M_EC = 2 * k_5 * (theta_C + 2 * theta_E) + MF_EC;
        reactionForces.M_EF = 2 * k_3 * (2 * theta_E + delta / 4);
        reactionForces.M_FE = 2 * k_3 * (theta_E + delta / 4);

        // shear forces
        reactionForces.V_AB = (k_1 / 2) * (theta_B + delta / 6);
        reactionForces.V_BA = -reactionForces.V_AB;
        reactionForces.V_BC = 0.36f * k_4 * (theta_B + theta_C) + VF_BC;
        reactionForces.V_CB = -0.36f * k_4 * (theta_B + theta_C) + VF_CB;
        reactionForces.V_CD = (k_2 / 2) * (theta_C + delta / 6);
        reactionForces.V_DC = -reactionForces.V_CD;
        reactionForces.V_CE = 0.36f * k_5 * (theta_C + theta_E) + VF_CE;
        reactionForces.V_EC = -0.36f * k_5 * (theta_C + theta_E) + VF_EC;
        reactionForces.V_EF = (k_2 / 2) * (theta_E + delta / 6);
        reactionForces.V_FE = -reactionForces.V_EF;

        // axial forces
        reactionForces.F_AB = reactionForces.V_BC;
        reactionForces.F_BA = -reactionForces.V_BC;
        reactionForces.F_BC = -reactionForces.V_BA;
        reactionForces.F_CB = reactionForces.V_BA;
        reactionForces.F_CD = -reactionForces.V_CB - reactionForces.V_CE;
        reactionForces.F_DC = reactionForces.V_CB + reactionForces.V_CE;
        reactionForces.F_CE = reactionForces.V_EF;
        reactionForces.F_EC = -reactionForces.V_EF;
        reactionForces.F_EF = -reactionForces.V_EC;
        reactionForces.F_FE = reactionForces.V_EC;

    }

}
