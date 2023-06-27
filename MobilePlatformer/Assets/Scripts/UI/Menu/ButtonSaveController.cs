using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSaveController : MonoBehaviour
{
    public void SaveData()
    {
        DataController.Instance.SaveData();
    }
}
