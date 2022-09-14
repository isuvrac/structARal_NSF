using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownWindForce : MonoBehaviour
{

    [SerializeField]
    public Transform Min, Max, WindLoadBar, arrowbase, Arrows;
    [SerializeField]
    public Vector3 applypoint, mOffset;
    public float WindForce;
    float WindForceScale = 12.5f;
    [SerializeField]
    GameObject Label;
    public float targetdis;
    [SerializeField]
    Town_ReactionForce town_ReactionForce;

    // Start is called before the first frame update
    private void Start()
    {targetdis = Min.position.x; }
    private void OnMouseDown()
    {mOffset = WindLoadBar.transform.position - GetMouseWorldPos();}

    Vector3 GetMouseWorldPos()
    {
        var v3 = Input.mousePosition;
        v3.z = Camera.main.WorldToScreenPoint(WindLoadBar.transform.position).z;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        return v3;
    }

    private void OnMouseDrag()
    {
        Vector3 draggingpoint = GetMouseWorldPos() + mOffset;
        applypoint = new Vector3(Mathf.Clamp(draggingpoint.x, Max.position.x,Min.position.x ), 0, 0);
        targetdis = applypoint.x; Label.GetComponent<TextMesh>().text =(Mathf.Round(10*WindForce)/10).ToString() + " k";
        town_ReactionForce.updatereactionforce();
    }

    // Update is called once per frame
    void Update()
    {
        WindLoadBar.transform.position = new Vector3(targetdis, WindLoadBar.transform.position.y, WindLoadBar.transform.position.z);
        float height = Mathf.Abs(WindLoadBar.transform.localPosition.x - arrowbase.localPosition.x);
        Arrows.Find("Base").gameObject.transform.localScale = new Vector3(10, height, 10);
        WindForce = Mathf.Abs(WindLoadBar.transform.position.x - Min.position.x) * (8.65f/ Mathf.Abs(Max.position.x-Min.position.x));//
    }
}