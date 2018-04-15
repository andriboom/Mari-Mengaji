using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _ScoringGame : MonoBehaviour {

	public Text correctContainer;
	public Text falseContainer;
	public int correctScore;
	public int falseScore;

	void Start()
	{
		correctScore = 0;
		falseScore = 0;
	}

	public void UpdateScore()
	{
		if (correctContainer != null)
			correctContainer.text = correctScore.ToString();

		if (falseContainer != null)
			falseContainer.text = falseScore.ToString();
	}
	
}
