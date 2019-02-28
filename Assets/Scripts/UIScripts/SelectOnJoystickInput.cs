using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnJoystickInput : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject selectedObject;
    [SerializeField]
    private string JoystickVerticalAxisName = "Vertical";
    private bool buttonSelected;
    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        if (Input.GetAxisRaw(JoystickVerticalAxisName) != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }
    private void OnDisable()
    {
        buttonSelected = false;
    }
}
