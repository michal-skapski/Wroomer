using UnityEngine;
using System.Collections;

public class JoystickFix : MonoBehaviour
{ //does not work
	public SimpleTouchController rightController;
	public float speedMovements = 5f;
	public float speedContinuousLook = 100f;
	public float speedProgressiveLook = 3000f;

	// PRIVATE
	[SerializeField] bool continuousRightController = true;

	public bool ContinuousRightController
	{
		set { continuousRightController = value; }
	}
}
