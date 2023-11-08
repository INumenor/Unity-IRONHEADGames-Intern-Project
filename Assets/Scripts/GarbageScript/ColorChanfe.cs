using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanfe : MonoBehaviour
{

    [SerializeField] public GameObject Body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ColorChange (Material Color)
    {
        Body.GetComponent<MeshRenderer>().material = Color;
    }
}
