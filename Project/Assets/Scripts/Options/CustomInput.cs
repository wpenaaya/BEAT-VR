using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

[RequireComponent(typeof(EventSystem))]
public class CustomInput : MonoBehaviour
{
    EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = GetComponent<EventSystem>();
    }

    public void Move(MoveDirection direction)
    {
        AxisEventData data = new AxisEventData(EventSystem.current);
        data.moveDir = direction;
        data.selectedObject = EventSystem.current.currentSelectedGameObject;

        ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.moveHandler);
    }

    private void Update()
    {
        if (SteamVR_Actions.drumGame.MenuUp.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Move(MoveDirection.Up);
        }
        if (SteamVR_Actions.drumGame.MenuDown.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Move(MoveDirection.Down);
        }
        if (SteamVR_Actions.drumGame.MenuLeft.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Move(MoveDirection.Left);
        }
        if (SteamVR_Actions.drumGame.MenuRight.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Move(MoveDirection.Right);
        }
        if(SteamVR_Actions.drumGame.Menu.GetStateDown(SteamVR_Input_Sources.Any))
        {
            BaseEventData data = new BaseEventData(EventSystem.current);
            GameObject cur = EventSystem.current.currentSelectedGameObject;
            data.selectedObject = cur;
            ExecuteEvents.Execute(cur, data, ExecuteEvents.submitHandler);
        }
    }
}
