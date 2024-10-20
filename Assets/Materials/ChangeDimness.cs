using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDimness : MonoBehaviour
{

    public Material material;

    public float initDimFactor = 0;
    public float finalDimFactor = 0;
    public float lerpSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        material.SetFloat("_DimFactor", initDimFactor);
    }

    private bool stop = false;
    // Update is called once per frame
    void Update()
    {
        // print(material.GetFloat("_DimFactor"));
        // material.SetFloat("_DimFactor", 0.05f);
        if (!stop) {
            material.SetFloat("_DimFactor", Mathf.Lerp(material.GetFloat("_DimFactor"), finalDimFactor, lerpSpeed));
            if (material.GetFloat("_DimFactor") <= finalDimFactor) stop=true;
            print(material.GetFloat("_DimFactor"));
        }
    }
}
