using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DropOpenGate : MonoBehaviour
{
    public GameObject gate;
    private bool leverOn;
    private Vector3 startingPos;
    private Vector3 targetPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPos = gate.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        leverOn = GetComponent<Lever>().on;
        if (leverOn)
        {
            targetPos = startingPos - new Vector3(0f, 4.2f, 0f);
        }
        else
        {
            targetPos = startingPos;
        }
        gate.transform.position = Vector3.MoveTowards(gate.transform.position, targetPos, 4f * Time.deltaTime);
    }
}
