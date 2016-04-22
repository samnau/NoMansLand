using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainedEventsManager : MonoBehaviour {

	public int EventId;    
	public List<string> ChainedEvents;
	public bool IsExecuting = false;
	//    
	public void NextTask(int nextId)
	{
		EventId = nextId;
		//audio.Play();
	}

	void Update()
	{
		if(EventId >= 0 && !IsExecuting)
		{ 
			IsExecuting = true;
			//Execute Next chained action          
			SendMessageUpwards(ChainedEvents[EventId]);     
			//if is the last item on the chain, set the event id to -1 so the cycle is not hit anymore
			if (EventId == ChainedEvents.Count)
			{
				EventId = -1;
			}

		}
	}
}