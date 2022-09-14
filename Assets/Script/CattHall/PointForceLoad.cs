using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointForceLoad : MonoBehaviour
{
    [SerializeField]
    double Snow_depth, Wind_speed, windload, deadload, snow_load, sload1, sload2, sload3, sload4, sload5, wload1, wload2, wload3, Rload1,Rload2,Rload3;
    [SerializeField]
    Text A,B,C,D,E,F,G;
    [SerializeField]
    CattWindLoad cattWindLoad;
    [SerializeField]
    CattLiveLoad cattLiveLoad;
    [SerializeField]
    GameObject Shear, Force1, Force2, PW1,PW2,PW3,PS1,PS2,PS3,PS4,PS5;

    // Start is called before the first frame update
    void Start()
    {
        Snow_depth = 0;
        Wind_speed = 0;
        deadload = 325;
        upadteForce(Snow_depth, Wind_speed);
    }


    public void upadteForce(double snow_depth, double wind_speed) {
        double snow_load = snow_depth *200/12;
        double load_p1s = 12.375 * snow_load;
        double load_p2s = 9.167 * snow_load;
        double load_p3s = 12.375 * snow_load;
        double load_p01s = 7.792 * snow_load;
        double load_p02s = 7.792 * snow_load;
        // dead loads
        double load_p1d = 12.375 * deadload;
        double load_p2d = 9.167 * deadload;
        double load_p3d = 12.375 * deadload;
        double load_p01d = 7.792 * deadload;
        double load_p02d = 7.792 * deadload;
        // vertical laods
        double load_p1 = (load_p1s + load_p1d) / 1000;
        double load_p2 = (load_p2s + load_p2d) / 1000;
        double load_p3 = (load_p3s + load_p3d) / 1000;
        double load_p01 = (load_p01s + load_p01d) / 1000;
        double load_p02 = (load_p02s + load_p02d) / 1000;
        // wind loads
        double qz = 0.00256 * 0.915 * 1.0 * 0.85 * 10 * wind_speed * wind_speed;
        
        double load_wind1 = qz * 0.85 * 0.4 *Mathf.Cos( Mathf.PI / 4);
        double load_wind2 = qz * 0.85 * 0.6 * Mathf.Cos(Mathf.PI / 4);
        double load_p4 = (load_wind1 + load_wind2) * 17.5 / 1000;
        double load_p5 = (load_wind1 + load_wind2) * 6.47 / 1000;
        double load_p06 = (load_wind1 + load_wind2) * 11.03 / 1000;
        windload = (load_wind1 + load_wind2) / 1000;

        double[] A_data = new double[100]{ 
            //           F1      F2      F3      F4      F5      F6      F7      R1      R2      R3
            /* F1 */
            0.707,       0,      0,      0,      0.905,  0,      0,      0,     -1,      0,
    /* F2 */     0.707,  0,      0,      0,      0.417,  0,      0,      1,      0,      0,
    /* F3 */    -0.707,  0.707,  0,      0,      0,      0.908,  1,      0,      0,      0,
    /* F4 */    -0.707,  0.707,  0,      0,      0,     -0.417,  0,      0,      0,      0,
    /* F5 */     0,     -0.707,  0.707,  0,      0,      0,      0,      0,      0,      0,
    /* F6 */     0,     -0.707, -0.707,  0,      0,      0,      0,      0,      0,      0,
    /* F7 */     0,      0,     -0.707,  0.707, -0.908,  0,     -1,      0,      0,      0,
    /* R1 */     0,      0,      0.707, -0.707, -0.417,  0,      0,      0,      0,      0,
    /* R2 */     0,      0,      0,     -0.707,  0,     -0.908,  0,      0,      0,      0,
    /* R3 */     0,      0,      0,      0.707,  0,      0.417,  0,      0,      0,      1
        };


        double[] c_data = new double[10] { -load_p06, load_p01, -load_p4, load_p1, -load_p5, load_p2, 0, load_p3, 0, load_p02 };

        double[,] a = new double[10, 10];
        for (int i = 0; i < A_data.Length; i++) { int k = i / 10; int l = i % 10; a[k, l] = A_data[i]; }
        double[,] b = new double[10, 1];
        for (int i = 0; i < c_data.Length; i++) { b[i, 0] = c_data[i]; }
        double[,] c = new double[10, 1];
        double[,] AT = MatrixTranspose(a);
        double[,] AI = new double [10,10]
{{7.341324302,    1.000302091, 0.8495437312 ,   6.587634856, -7.754510515,    -6.452326739,    10.71987068,-1.956685396 ,   -1.827515735 ,   -1.966837593, },
{ 1.000302091, 1.000302091 ,1.838392485e-15, -4.727294961e-15  ,  -2.101019983e-14  ,  -5.252549957e-16  ,  -2.101019983e-15  ,  -0.7072135785 ,  0.7072135785  ,  -1.969706234e-15 },
{ 0.8495437312,    1.313137489e-15, 1.000302091 ,1.849845822, -1.44035352, -1.44035352 ,1.908468414 ,-3.939412468e-16,-0.7028925179 ,  -0.7072135785 },
{ 6.587634856 ,-3.676784970e-15 ,   1.849845822 ,10.10987512 ,-9.705895106  ,  -9.194864036,    13.82959393, -0.6100995839  , -4.126377228  ,  -3.313423406 },
{ -7.754510515  ,  -3.676784970e-15 ,   -1.44035352, -9.705895106 ,   12.47665816, 9.800360923, -15.77685508 ,   0.2796724813 ,   5.808936701 ,2.775317335 },
{ -6.452326739   , -8.929334926e-15 ,  -1.44035352, -9.194864036,   9.800360923, 9.402454572, -13.7401952, 0.4750444998,    4.307531631, 2.579945317 },
{ 10.71987068, 6.303059948e-15, 1.908468414, 13.82959393 ,-15.77685508,    -13.7401952, 21.90433298, -1  ,-6.69910528, -4.047861507 },
{ -1.956685396  ,  -0.7072135785 ,  2.757588727e-15, -0.6100995839 ,  0.2796724813  ,  0.4750444998 ,   -1 , 2.266753151, -1.13027298, 0.2332468494 },
{ -1.827515735 ,   0.7072135785,    -0.7028925179 ,  -4.126377228 ,   5.808936701, 4.307531631, -6.69910528, -1.13027298, 4.96503409,  1.12110801 },
{ -1.966837593 ,   4.989922459e-15,-0.7072135785 ,  -3.313423406,   2.775317335, 2.579945317, -4.047861507 ,   0.2332468494  ,  1.12110801 , 2.266753151 },};
    
        double[,] AC = matrxMultiply(AI, AT);
       
        c = matrxMultiply(AC, b);

        float F1 = (float)c[0, 0];
        float F2 = (float)c[1,0];
        float F3 = (float)c[2, 0];
        float F4 = (float)c[3, 0];
        float F5 = (float)c[4, 0];
        float F6 = (float)c[5, 0];
        float F7 = (float)c[6, 0];
        float R1 = (float)c[7, 0];
        float R2 = (float)c[8, 0];
        float R3 = (float)c[9, 0];


        // Update arrows with calculated forces
        sload1 = load_p1;
        sload2 = load_p2;
        sload3 = load_p3;
        sload4 = load_p01;
        sload5 = load_p02;
        wload1 = load_p4;
        wload2 = load_p5 ;
        wload3 = load_p06;

        Rload1 = R1;
        Rload2 = R2;
        Rload3 = R3;
        updatearrow(Shear, (float)Rload2, " K");
        updatearrow(Force1, -(float)Rload1, " K");
        updatearrow(Force2, -(float)Rload3, " K");
        updatearrow(PW1, (float)wload1, " K");
        updatearrow(PW2, (float)wload2, " K");
        updatearrow(PW3, (float)wload3, " K");
        updatearrow(PS1, (float)sload1, " K");
        updatearrow(PS2, (float)sload2, " K");
        updatearrow(PS3, (float)sload3, " K");
        updatearrow(PS4, (float)sload4, " K");
        updatearrow(PS5, (float)sload5, " K");
        
        A.text = (Mathf.Round(F1 * 10) / 10).ToString()+" kip ";
        B.text = (Mathf.Round(F2 * 10) / 10).ToString() + " kip ";
        C.text = (Mathf.Round(F3 * 10) / 10).ToString() + " kip ";
        D.text = (Mathf.Round(F4 * 10) / 10).ToString() + " kip ";
        E.text = (Mathf.Round(F5 * 10) / 10).ToString() + " kip ";
        F.text = (Mathf.Round(F6 * 10) / 10).ToString() + " kip ";
        G.text = (Mathf.Round(F7 * 10) / 10).ToString() + " kip ";
    }

    private void updatearrow( GameObject arrow, float input, string unit) {
        arrow.transform.Find("Arrow").transform.Find("Base").gameObject.transform.localScale = new Vector3(10, input, 10);
        arrow.transform.Find("ForceLabel").gameObject.GetComponent<TextMesh>().text = (Mathf.Round(Mathf.Abs(input) *10)/10).ToString() + unit;
    }

    private double[,] matrxMultiply(double[,] matA, double[,] matB) {
        double[,] res;
        int rowA = matA.GetLength(0);
        int columnsA = matA.GetLength(1);
        int rowB = matB.GetLength(0);
        int columnsB = matB.GetLength(1);

        if (columnsA != rowB) { res = null; }
        else {
            res = new double[rowA, columnsB];

            for (int a = 0; a < columnsB; a++) {
                for (int i = 0; i < rowA; i++)
                {
                    double sum = 0;
                    for (int j=0; j < columnsA; j++) { sum += matA[i, j] * matB[j, a]; }
                    res[i, a] = sum;

                }
            }
        }
        return res;
    }

    private double[,] MatrixTranspose(double[,] mat)
    {

        int rows = mat.GetLength(0);
        int columns = mat.GetLength(1);
        double[,] res = new double[columns, rows];
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                res[j, i] = mat[i, j];
            }
        }
        return res; 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
