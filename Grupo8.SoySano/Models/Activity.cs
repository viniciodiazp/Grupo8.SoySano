using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Grupo8.SoySano.Models
{
    public class Activity
    {
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public String Name { get; set; }

		[JsonProperty("user")]
		public User User { get; set; }

		[JsonProperty("startDate")]
		public DateTime StartDate { get; set; }

		[JsonProperty("finishDate")]
		public DateTime FinishDate { get; set; }

		[JsonProperty("photo")]
		public String Photo { get; set; }

		[JsonProperty("duration")]
		public String Duration
        {
			get
            {
				TimeSpan diff = FinishDate.Subtract(StartDate);

				return diff.ToString(@"hh\:mm\:ss");
            }
        }
		[JsonProperty("calories")]
		public double Calories
		{
			get
			{				
				return 0.029 * User.Weight * FinishDate.Subtract(StartDate).TotalMinutes;
			}
		}
	}
}
