using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontTesting : MonoBehaviour
{

    public string[] fontOptionsCAPSTESTING;
    [SerializeField] int testIndex;

    public Text textElement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (testIndex != fontOptionsCAPSTESTING.Length - 1)
            {
                testIndex++;
            }
            else
            {
                testIndex = 0;
            }
                textElement.text = fontOptionsCAPSTESTING[testIndex];
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            textElement.fontSize++;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            textElement.fontSize--;
        }
    }
}
