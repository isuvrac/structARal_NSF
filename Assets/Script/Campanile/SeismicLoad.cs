using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeismicLoad : MonoBehaviour
{
    [SerializeField]
    public Transform ShearArrow, sideload;//,cube
    [SerializeField]
    private ForceUpdate forceUpdate;
    private List<Transform> Arrows = new List<Transform>();
    [SerializeField]
    GameObject MomentL, ShearL, DefLable;
    public int valueIndex;
    public bool animate;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform a in sideload)
        {
            if (a.tag == "WindForceArrow")
            {
                Arrows.Add(a);
            }
        }
    }

    private void Update()
    {
        if (animate && this.gameObject.activeSelf)
        {
            forceUpdate.DrawAnimatedLine();
            DefLable.GetComponent<TextMesh>().text = (forceUpdate.DefLabel).ToString() + " in";
        }
    }


    public void OnIndexChange(int valueIndex)
    {
        
        forceUpdate.Drawlines(valueIndex, -30000);
        //Change label and rotation 
        DefLable.GetComponent<TextMesh>().text = (forceUpdate.DefLabel).ToString() + " in";
        MomentL.GetComponent<TextMesh>().text = forceUpdate.moment.ToString() + " k-ft";
        ShearL.GetComponent<TextMesh>().text = forceUpdate.shear.ToString() + " k";
       
        int i = 0;
         foreach (Transform t in Arrows)
        {
            t.transform.Find("Base").gameObject.transform.localScale = new Vector3(5,forceUpdate.WindSeismic[3-i]/40, 5);
            t.transform.Find("Label").gameObject.GetComponent<TextMesh>().text = forceUpdate.WindSeismic[3 - i].ToString()+" k";i++;
        }
        ShearArrow.transform.Find("Base").gameObject.transform.localScale = new Vector3(1, forceUpdate.shear /200, 1);

        

    }

   
}
