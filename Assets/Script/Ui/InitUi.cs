using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitUi : MonoBehaviour,BasePanel
{
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
