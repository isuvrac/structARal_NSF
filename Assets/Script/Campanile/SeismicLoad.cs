using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeismicLoad : MonoBehaviour
{
    [SerializeField]
    public Transform ShearArrow, MomentArrow, sideload;//,cube
    [SerializeField]
    private ForceUpdate forceUpdate;
    private List<Transform> Arrows = new List<Transform>();
    [SerializeField]
    GameObject MomentL, ShearL, DefLable;
    public int valueIndex;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform a in sideload)
        {
            if (a.tag == "WindForceArrow")
            {

                Arrows.Add(a);
            }
        }
    }
   

   
    public void OnIndexChange(int valueIndex)
    {
        forceUpdate.Drawlines(valueIndex);
        //Change label and rotation 
        DefLable.GetComponent<TextMesh>().text = (forceUpdate.DefLabel).ToString() + " in";
        MomentL.GetComponent<TextMesh>().text = forceUpdate.moment.ToString() + " k-ft";
        ShearL.GetComponent<TextMesh>().text = forceUpdate.shear.ToString() + " k";
       
        int i = 0;
         foreach (Transform t in Arrows)
        {
            t.transform.Find("Base").gameObject.transform.localScale = new Vector3(5,forceUpdate.WindSeismic[3-i]/20, 5);
            t.transform.Find("Label").gameObject.GetComponent<TextMesh>().text = forceUpdate.WindSeismic[3 - i].ToString()+" k";i++;
        }
        ShearArrow.transform.Find("Base").gameObject.transform.localScale = new Vector3(1, forceUpdate.shear /200, 1);

        //float rotate = Mathf.Clamp((360 - Mathf.Abs(forceUpdate.moment / 34)), 271, 359);
        //Vector3 to = new Vector3(rotate, MomentArrow.rotation.eulerAngles.y, MomentArrow.rotation.eulerAngles.z);
        //MomentArrow.eulerAngles = to; print(MomentArrow.eulerAngles.x);

    }

   
}
