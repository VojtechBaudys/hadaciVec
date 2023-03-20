using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hadaciVec.Entity;

public class Question
{
	private int[] _numbers; // math problem numbers
	private List<int> _options; // answere options
	private char _operator; // math problem operator
	private int _result; // correct result
	private Random _random = new Random();
	
	public Question(int amoutOfNumbers = 2)
	{
		_numbers = GenerateNumbers(amoutOfNumbers);
		_operator = GenerateOperator();
		_options = GenerateOptions();
	}

	// generate answere options
	// return Options
	private List<int> GenerateOptions()
	{
		List<int> opt = new List<int>();
		int parameterNumber = 0; // parameter number (randomizer)
		
		for (int i = 0; i < 3; i++)
		{
			switch (_operator)
			{
				case '+':
					opt.Add((_numbers[0] + parameterNumber) + _numbers[1]);
					break;
				case '-':
					opt.Add((_numbers[0] + parameterNumber) - _numbers[1]);
					break;
				case '*':
					opt.Add((_numbers[0] + parameterNumber) * _numbers[1]);
					break;
				case '/':
					opt.Add((_numbers[0] + parameterNumber) / _numbers[1]);
					break;
				default:
					throw new ArgumentException();
			}

			// generate random paramenter
			do
			{
				parameterNumber = _random.Next(-20, 20);
			} while (parameterNumber == 0);
		}
		
		_result = opt[0]; // set correct result
		
		Shuffle(opt); // shuffle options
		
		return opt;
	}

	// generate numbers
	// lenght - [int]
	//		  - amount of numbers
	private int[] GenerateNumbers(int length)
	{
		int[] nums = new int[length];
			
		for (int i = 0; i < nums.Length; i++)
		{
			nums[i] = _random.Next(0, 1000);
		}

		return nums;
	}
	
	// generate Operator
	private char GenerateOperator()
	{
		switch (_random.Next(1, 4))
		{
			case 1:
				return '+';
			case 2:
				return '-';
			case 3:
				return '*';
			case 4:
				return '/';
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
	
	// get equation
	public string GetEquation()
	{
		return _numbers[0].ToString() + " " + _operator.ToString() + " " + _numbers[1].ToString();
	}

	// check answere
	// answere - [int]
	//		   - (button content)
	public bool CheckAnswere(int answere)
	{
		return answere == _result ? true : false;
	}
	
	// shuffle list
	private void Shuffle<T>(List<T> list)
	{  
		int n = list.Count;  
		while (n > 1) 
		{  
			n--;  
			int k = _random.Next(n + 1);  
			(list[k], list[n]) = (list[n], list[k]);
		}
	}
	
	// GETTERS 

	// get options
	public List<int> GetOptions()
	{
		return _options;
	}

	// get String Json Data
	// score - [int]
	//		 - (level score)
	public string GetStringJsonData(int score)
	{
		Dictionary<string, object> data = new Dictionary<string, object>();

		data.Add("score", score);
		data.Add("numbers", _numbers);
		data.Add("options", _options);
		data.Add("operator", _operator);
		data.Add("result", _result);

		return JsonConvert.SerializeObject(data);
	}
	
	// Setters
	
	// set loaded data	
	public void SetData(dynamic data)
	{
		_numbers = data["numbers"].ToObject<int[]>();
		_options = data["options"].ToObject<List<int>>();
		_operator = (char)data["operator"];
		_result = (int)data["result"];
	}
}