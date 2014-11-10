using UnityEngine;
using System;

public class ClockAnimator : MonoBehaviour{
	private const float
		hoursToDegrees = 360f / 12f,
		minutesToDegrees = 360f / 60f,
		secondsToDegrees = 360f / 60f;

	public Transform hours, minutes, seconds;

	public bool analog;

	Quaternion transformRotation (float span, float conversion){
		return Quaternion.Euler(0f,0f,span * -conversion);
	}

	/*void analogTimeToDegrees (){
		TimeSpan timespan = DateTime.Now.TimeOfDay;
		hours.localRotation = transformRotation((float)timespan.TotalHours,(float)hoursToDegrees);
		minutes.localRotation = transformRotation((float)timespan.TotalMinutes,(float)minutesToDegrees);
		seconds.localRotation = transformRotation((float)timespan.TotalSeconds,(float)secondsToDegrees);
	}*/

	void timeToDegrees (float hoursVal, float minutesVal, float secondsVal){
		hours.localRotation = transformRotation((float)hoursVal,(float)hoursToDegrees);
		minutes.localRotation = transformRotation((float)minutesVal,(float)minutesToDegrees);
		seconds.localRotation = transformRotation((float)secondsVal,(float)secondsToDegrees);
	}

	void Update (){
		if (analog) {
			TimeSpan timespan = DateTime.Now.TimeOfDay;
			timeToDegrees((float)timespan.TotalHours,(float)timespan.TotalMinutes,(float)timespan.TotalSeconds);
		} else {
			DateTime time = DateTime.Now;
			timeToDegrees((float)time.Hour,(float)time.Minute,(float)time.Second);
		}

	}
}