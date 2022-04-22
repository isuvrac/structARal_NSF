using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixJoinUI : MonoBehaviour
{
    float BF1, BF2, BS1, BS2, BM1, BM2, BR, CF1, CF2, CS1, CS2, CM1, CM2, CR;
    [SerializeField]
    Text Bf1, Bf2, Bs1, Bs2, Bm1, Bm2, Cf1, Cf2, Cs1, Cs2, Cm1, Cm2;

    public void assignValue(float bf1, float bf2, float bs1, float bs2, float bm1, float bm2, float br, float cf1, float cf2, float cs1, float cs2, float cm1, float cm2, float cr) {
         BF1=bf1;  BF2=bf2; BS1 =bs1;  BS2=bs2 ;BM1=bm1  ;  BM2=bm2;  BR =br; CF1=cf1 ;   CF2=cf2;   CS1= cs1;  CS2 = cs2;   CM1= cm1;   CM2=cm2; CR= cr ;
        
        Bf1.text=Mathf.Abs(Mathf.Round(BF1*10)/10).ToString()+" k"; Bf2.text = Mathf.Abs(Mathf.Round(BF2 * 10) / 10).ToString()+" k"; Bs1.text = Mathf.Abs(Mathf.Round(BS1 * 10) / 10).ToString()+" k";
        Bs2.text = Mathf.Abs(Mathf.Round(BS2 * 10) / 10).ToString() + " k"; Bm1.text = Mathf.Abs(Mathf.Round(BM1 * 10) / 10).ToString()+"k-ft"; Bm2.text = Mathf.Abs(Mathf.Round(BM2 * 10) / 10).ToString()+" k-ft"; 
        Cf1.text = Mathf.Abs(Mathf.Round(CF1 * 10) / 10).ToString()+" k"; Cf2.text = Mathf.Abs(Mathf.Round(CF2 * 10) / 10).ToString()+" k";Cs1.text = Mathf.Abs(Mathf.Round(CS1 * 10) / 10).ToString()+ " k"; 
        Cs2.text = Mathf.Abs(Mathf.Round(CS2 * 10) / 10).ToString()+ " k"; Cm1.text = Mathf.Abs(Mathf.Round(CM1 * 10) / 10).ToString()+ " k-ft"; Cm2.text = Mathf.Abs(Mathf.Round(CM2 * 10) / 10).ToString()+ "k-ft";
    }


    void updateFixJoin() {
    
    
    
    
    }

    // Start is called before the first frame update
    void Start()
    {
       // assignValue(1,2,3,4,5,6,7,8,9,10,11,12,13,14);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
