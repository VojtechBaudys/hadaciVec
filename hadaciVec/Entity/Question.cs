using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hadaciVec.Entity;

public class Question
{
	private int[] _numbers;
	private List<int> _options;
	private char _operator;
	private int _result;
	private Random _random = new Random();
	
	public Question(int amoutOfNumbers = 2)
	{
		_numbers = GenerateNumbers(amoutOfNumbers);
		_operator = GenerateOperator();
		_options = GenerateOptions(_operator);
	}

	private List<int> GenerateOptions(char oper)
	{
		List<int> opt = new List<int>();
		int parameterNumber = 0;
		
		for (int i = 0; i < 3; i++)
		{
			switch (oper)
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

			do
			{
				parameterNumber = _random.Next(-20, 20);
			} while (parameterNumber == 0);
		}
		
		_result = opt[0];
		
		Shuffle(opt);
		
		return opt;
	}

	private int[] GenerateNumbers(int length)
	{
		int[] nums = new int[length];
			
		for (int i = 0; i < nums.Length; i++)
		{
			nums[i] = _random.Next(0, 1000);
		}

		return nums;
	}
	
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
	
	public string GetEquation()
	{
		return _numbers[0].ToString() + " " + _operator.ToString() + " " + _numbers[1].ToString();
	}

	public bool CheckAnswere(int answere)
	{
		return answere == _result ? true : false;
	}
	
	public void Shuffle<T>(List<T> list)
	{  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = _random.Next(n + 1);  
			(list[k], list[n]) = (list[n], list[k]);
		}
	}
	
	// getters & setters

	public List<int> GetOptions()
	{
		return _options;
	}

	public string GetStringJsonData()
	{
		Dictionary<string, object> data = new Dictionary<string, dynamic>();

		data.Add("numbers", _numbers);
		data.Add("options", _options);
		data.Add("operator", _operator);
		data.Add("result", _result);

		return JsonConvert.SerializeObject(data);
	}
}