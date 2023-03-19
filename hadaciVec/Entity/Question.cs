using System;
using System.Reflection.Emit;

namespace hadaciVec.Entity;

public class Question
{
	private int[] _numbers = new int[2];
	private int[] _outcomes = new int[3];
	string _operator; 
	Random _random = new Random();
	
	public Question()
	{
		_numbers[0] = _random.Next(0, 1000);
		_numbers[1] = _random.Next(0, 1000);
		GenerateEverything();
	}

	private void GenerateEverything()
	{
		switch (_random.Next(0, 3))
		{
			case 1:
				_operator = "+";
				
				break;
			case 2:
				_operator = "-";
				break;
			case 3:
				_operator = "*";
				break;
			case 4:
				_operator = "/";
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void GenerateOutcomes()
	{
		for (int i = 0; i < _outcome.Length; i++)
		{
			_outcome[i] = 
		}
	}
}