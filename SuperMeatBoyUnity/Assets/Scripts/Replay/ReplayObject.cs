using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayObject : MonoBehaviour
{
    public void SetDataFromFrame(ReplayData data)
    {
        this.transform.position = data.position;
    }
}
