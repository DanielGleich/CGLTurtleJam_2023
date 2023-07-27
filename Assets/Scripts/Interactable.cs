using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] int level;

    private void Awake()
    {
        LevelManager.Instance.allObjects.Add(this);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        LevelManager.Instance.OnObjectChange += ObjectSelection;
    }

    private void OnDestroy()
    {
        if (LevelManager.Instance.allObjects.Contains(this))
            LevelManager.Instance.allObjects.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetId()
    {
        return id;
    }
    public int GetLevel()
    {
        return level;
    }

    void ObjectSelection(int objectId)
    {
        if (id == objectId)
            EnableSelection();
        else
            DisableSelection();
    }

    void EnableSelection()
    {
        //Debug.Log(string.Format("{0} was selected", id));
        //gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        ControlsMove moveScript = gameObject.GetComponent<ControlsMove>();
        ControlsRotate rotateScript = gameObject.GetComponent<ControlsRotate>();

        if (moveScript != null)
        {
            moveScript.EnableControls();
        }

        if (rotateScript != null)
        {
            rotateScript.EnableControls();
        }
    }

    void DisableSelection()
    {
        //Debug.Log(string.Format("{0} was deselected", id));
        //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        ControlsMove moveScript = gameObject.GetComponent<ControlsMove>();
        ControlsRotate rotateScript = gameObject.GetComponent<ControlsRotate>();

        if (moveScript != null)
        {
            moveScript.DisableControls();
        }

        if (rotateScript != null)
        {
            rotateScript.DisableControls();
        }
    }
}
