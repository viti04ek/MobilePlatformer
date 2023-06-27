using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshData : MonoBehaviour
{
    private void Start()
    {
        DataController.Instance.RefreshData();
    }
}
