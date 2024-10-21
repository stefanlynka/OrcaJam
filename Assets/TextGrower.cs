using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGrower : MonoBehaviour
{
    public Text Text;
    public int Speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Text != null && Text.fontSize < 162) Text.fontSize += Speed;
    }
}
