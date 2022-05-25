using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Grupo8.SoySano.Models
{
    public class User
    {
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("email")]
		public String Email { get; set; }

		[JsonProperty("name")]
		public String Name { get; set; }

		[JsonProperty("gender")]
		public String Gender { get; set; }

		[JsonProperty("birthDate")]
		public DateTime BirthDate { get; set; }

		[JsonProperty("Height")]
		public double Height { get; set; }

		[JsonProperty("weight")]
		public double Weight { get; set; }
	}
}
