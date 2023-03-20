using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using hadaciVec.Entity;
using hadaciVec.Util;

namespace hadaciVec
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Question _question;
		private SaveManger _saveManger = new SaveManger();
		private int _score = 0;
		public MainWindow()
		{
			InitializeComponent();
			GenerateLevel();
			_question.SetData(_saveManger.LoadData());
		}

		private void CheckOption(object sender, RoutedEventArgs e)
		{
			Button element = (Button)sender;
			
			int answere = (int)element.Content;
			if (_question.CheckAnswere(answere))
			{
				_score++;
				element.Background = Brushes.ForestGreen;
				GenerateLevel();
			}
			else
			{
				element.Background = Brushes.Crimson;
				ScoreBox.Text = "WRONG ANSWERE";
				
				_score = 0;
				GenerateLevel();
			}

			_saveManger.SaveStats(_question.GetStringJsonData());
		}

		private void GenerateLevel()
		{
			_question = new Question();
			
			// default
			SetColors();
			
			// set math problem
			MathProblem.Content = _question.GetEquation();

			// set options
			List<int> options = _question.GetOptions();
			LeftButton.Content = options[0];
			MiddleButton.Content = options[1];
			RightButton.Content = options[2];
			
			// set score
			ScoreBox.Text = "Your score: " + _score;
		}

		private void SetColors()
		{
			// buttons
			LeftButton.Background = Brushes.HotPink;
			MiddleButton.Background = Brushes.HotPink;
			RightButton.Background = Brushes.HotPink;
			
			// ScoreBox
			ScoreBox.Foreground = Brushes.WhiteSmoke;
		}
	}
}