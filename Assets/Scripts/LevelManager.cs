using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            inputActions = new InputActionControls();
            allObjects = new List<Interactable>();
            levelObjects = new List<Interactable>();
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

    }
    #endregion Singleton
    [SerializeField] public List<Interactable> allObjects;
    [SerializeField] public List<Interactable> levelObjects;
    [SerializeField] private int currentLevel;
    [SerializeField] CameraManager cameraManager;
    Dictionary<int, Vector2> checkPoints;
    EggPhysics playerScript;
    [SerializeField] Interactable selectedObject;
    private InputActionControls inputActions;

    // Start is called before the first frame update

    void Start()
    {
        currentLevel = 0;
        checkPoints = new Dictionary<int, Vector2>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<EggPhysics>();

        //Activate Controls

        inputActions = new InputActionControls();


        //Init Checkpoints
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("CheckPoint")) {
            int checkPointNumber = o.GetComponent<CheckPoint>().GetCheckPointNumber();
            if(checkPoints.ContainsKey(checkPointNumber))
                Debug.LogWarning(String.Format("Checkpoint {0} exists twice", checkPointNumber));
            else
                checkPoints.Add(checkPointNumber, new Vector2(o.transform.position.x, o.transform.position.y));
        }


        SetLevelObjects(1);
        selectedObject = levelObjects[0];
        ObjectChanged(selectedObject.GetId());
        //Set First Checkpoint
        if (checkPoints.Count > 0)
            SetPlayerCheckPoint(1);
    }

    public void MoveCameraToCurrentLevel() {
        cameraManager.MoveToLevel(currentLevel);
    }

    private void OnEnable()
    {
        inputActions.Gameplay.SwapRight.Enable();
        inputActions.Gameplay.SwapLeft.Enable();
        inputActions.Gameplay.SwapRight.performed += SwapRight;
        inputActions.Gameplay.SwapLeft.performed += SwapLeft;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.SwapRight.Disable();
        inputActions.Gameplay.SwapLeft.Disable();
    }


    private void SwapRight(InputAction.CallbackContext context)
    {
        int objectId = levelObjects.IndexOf(selectedObject) - 1;

        if (objectId < 0)
        {
            objectId = levelObjects.Count - 1;
        }
        selectedObject = levelObjects[objectId];
        ObjectChanged(selectedObject.GetId());
    }
    
    private void SwapLeft(InputAction.CallbackContext context) {
        int objectId = levelObjects.IndexOf(selectedObject) + 1;
        if (objectId >= levelObjects.Count)
        {
            objectId = 0;
        }
        selectedObject = levelObjects[objectId];
        ObjectChanged(selectedObject.GetId());
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    //public event Action OnCheckPointReached;
    public void CheckPointReached(int checkPointNumber)
    {
        int recentCheckPointNumber = checkPoints.FirstOrDefault(x => Vector2.Equals(x.Value, new Vector2(playerScript.recentCheckpoint.x, playerScript.recentCheckpoint.y))).Key;
        if (recentCheckPointNumber < checkPointNumber) {
            currentLevel++;
            SetPlayerCheckPoint(checkPointNumber);
            SetLevelObjects(currentLevel);
            MoveCameraToCurrentLevel();
        }
    }

    void SetPlayerCheckPoint(int checkPointNumber)
    {
        Vector2 pos;
        checkPoints.TryGetValue(checkPointNumber, out pos);
        playerScript.SetCheckpoint(pos);
    }

    void SetLevelObjects(int level)
    {
        levelObjects.Clear();

        for (int i = 0; i < allObjects.Count; i++)
        {
            if (allObjects[i].GetLevel() == currentLevel)
                levelObjects.Add(allObjects[i]);
        }

        for (int i = 0; i < levelObjects.Count; i++) 
        {
            if (i+1 == levelObjects.Count)
            {
                return;
            }

            if (levelObjects[i].GetId() > levelObjects[i + 1].GetId())
            {
                Interactable temp = levelObjects[i + 1];
                levelObjects[i + 1] = levelObjects[i];
                levelObjects[i] = temp;
                i = 0;
            }

            selectedObject = levelObjects[0];
            ObjectChanged(selectedObject.GetId());
        }
    }

    public event Action<int> OnObjectChange;
    public void ObjectChanged(int objectId)
    {
        OnObjectChange(objectId);
    }
}
