using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimedScriptTrigger : MonoBehaviour
{
    [Header("Event Configuration")]
    [Tooltip("Event to trigger after the delay. Configure in inspector to call any method on any GameObject")]
    public UnityEvent onTriggerEvent;
    
    [Header("Timing Configuration")]
    [Tooltip("Delay in seconds before calling the method")]
    public float delayInSeconds = 1.0f;
    
    [Tooltip("Whether to trigger the method on Start")]
    public bool triggerOnStart = false;
    
    private Coroutine _currentCoroutine;
    
    void Start()
    {
        if (triggerOnStart)
        {
            TriggerMethod();
        }
    }
    
    /// <summary>
    /// Triggers the method call after the specified delay
    /// </summary>
    public void TriggerMethod()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
        
        _currentCoroutine = StartCoroutine(TriggerMethodAfterDelay());
    }
    
    /// <summary>
    /// Triggers the event call immediately without delay
    /// </summary>
    public void TriggerMethodImmediately()
    {
        OnTriggerEvent();
    }
    
    /// <summary>
    /// Stops any pending method call
    /// </summary>
    public void CancelPendingTrigger()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }
    
    private IEnumerator TriggerMethodAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        OnTriggerEvent();
        _currentCoroutine = null;
    }
    
    /// <summary>
    /// Called when the event should be triggered
    /// </summary>
    private void OnTriggerEvent()
    {
        if (onTriggerEvent != null)
        {
            onTriggerEvent.Invoke();
            Debug.Log("TimedScriptTrigger: Event triggered successfully");
        }
        else
        {
            Debug.LogWarning("TimedScriptTrigger: No event listeners configured!");
        }
    }
    
    /// <summary>
    /// Sets a new delay time
    /// </summary>
    public void SetDelay(float newDelay)
    {
        delayInSeconds = Mathf.Max(0, newDelay);
    }
    
    /// <summary>
    /// Sets the trigger on start flag
    /// </summary>
    public void SetTriggerOnStart(bool trigger)
    {
        triggerOnStart = trigger;
    }
}
