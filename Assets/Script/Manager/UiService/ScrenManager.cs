using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrenManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScrenManager instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
