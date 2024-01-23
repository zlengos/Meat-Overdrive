using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstructionChecker : MonoBehaviour
{
    [SerializeField] private GameObject instructionShift, instructionWASD, instructionSpace;


    private bool isDestroyedShift = false;
    private bool isDestroyedWASD = false;
    private bool isDestroyedSpace = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isDestroyedShift)
        {
            Destroy(instructionShift);
            isDestroyedShift = true;
            Destroy(instructionShift);
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && !isDestroyedWASD)
        {
            Destroy(instructionWASD);
            isDestroyedWASD = true;
            Destroy(instructionWASD);

        }
        if (Input.GetKey(KeyCode.Space) && !isDestroyedSpace)
        {
            Destroy(instructionSpace);
            isDestroyedSpace = true;
            Destroy(instructionSpace);

        }

        if (isDestroyedShift && isDestroyedWASD && isDestroyedSpace)
            Destroy(gameObject);
    }
}
