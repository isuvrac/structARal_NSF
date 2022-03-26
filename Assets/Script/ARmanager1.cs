using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARmanager1 : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject imagetarget;
    Transform initialtransform;

    void Start()
    {
        initialtransform = this.transform;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deAlignObject()
    {
        this.transform.position = initialtransform.position;
        this.transform.localScale = initialtransform.localScale;
    }

    public void AlignObject()
    {
        this.transform.position = imagetarget.transform.position;
        //this.transform.localScale = new Vector3(this.transform.localScale.x / 2, this.transform.localScale.y / 2, this.transform.localScale.z / 2);
        //this.transform.rotation = imagetarget.transform.rotation;
        //this.transform.up = imagetarget.transform.up;
    
    
    }

}
