using System.Collections.Generic;
using System.ComponentModel;
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
		private Question _question; // Math problem class
		private SaveManger _saveManger = new SaveManger(); // Save & Load class
		private dynamic _save; // loaded save data
		private int _score = 0; // score
		
		public MainWindow()
		{
			InitializeComponent(); // init
			_save = _saveManger.LoadData(); // load save.json
			GenerateLevel(); // generate new level
		}

		// button OnClick Event
		private void CheckOption(object sender, RoutedEventArgs e)
		{
			Button element = (Button)sender; // set element as button instance
			
			int answere = (int)element.Content; // get button content (answere)
			if (_question.CheckAnswere(answere)) // check if answere is correct
			{
				// correct answere
				_score++; // add score point +1
				element.Background = Brushes.ForestGreen;
				GenerateLevel(); // reset level (create new math problem)
			}
			else
			{
				// incorrect answere
				element.Background = Brushes.Crimson;
				ScoreBox.Text = "WRONG ANSWERE";
				
				_score = 0; // reset score
				GenerateLevel(); // reset level (create new math problem)
			}
		}

		// main level method
		private void GenerateLevel()
		{
			_question = new Question(); // new Question
			
			// load save
			if (_save != null)
			{
				if (_save.ToString() != "{}")
				{
					_question.SetData(_save); // set Question data
					_score = (int)_save["score"]; // set score
					_save = null!;	
				}
			}
			
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

		// save current level
		void MainWindow_OnClosing(object? sender, CancelEventArgs e)
		{
			_saveManger.SaveStats(_question.GetStringJsonData(_score)); // save Question and _score TO save.json
		}
	}
}