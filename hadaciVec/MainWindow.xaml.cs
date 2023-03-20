using System.Collections.Generic;
using System.Windows;
using hadaciVec.Entity;

namespace hadaciVec
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Question _question = new Question();
		
		public MainWindow()
		{
			InitializeComponent();
			
			MathProblem.Content = _question.GetEquation();

			List<int> options = _question.GetOptions();
			LeftButton.Content = options[0];
			MiddleButton.Content = options[1];
			RightButton.Content = options[2];
		}

		void CheckOption(object sender, RoutedEventArgs e)
		{
			
		}
	}
}