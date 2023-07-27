using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private int checkPointNumber = 0;
    // Start is called before the first frame update

    public int GetCheckPointNumber() { return checkPointNumber; }
    void Start()
    {
        if (checkPointNumber == 0) {
            Debug.LogWarning("CheckPoint has no id", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
    }
}
