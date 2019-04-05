using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent game_event;
    public UnityEvent response;

    private void OnEnable()
    { game_event.RegisterListener(this); }
    private void OnDisable()
    { game_event.UnregisterListener(this); }

    public void OnEventRaised()
    {
        Debug.Log("GameEventRaised");
        response.Invoke();
    }
}
