using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Models
{
    internal class ScoreModel : INotifyPropertyChanged 
    {
		private int score;

		public int Score
		{
			get { return score; }
			set 
			{ 
				score = value;
				OnPropertyChanged(nameof(Score));
				if (BestScore < score)
				{
					BestScore = score;
				}
			}
		}

		private int bestScore;

		public int BestScore
		{
			get { return bestScore; }
			set { 
				bestScore = value;
                OnPropertyChanged(nameof(BestScore));
            }
		}

		public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
    }
}
