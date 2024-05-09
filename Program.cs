using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

class Program {

	public static void Main(string[] args){
		while (true)
		{
			ISS result = getISS();
			Console.Write("- {0}\n", result.message);
			Vector coordinates = new Vector();
			coordinates.latitude = double.Parse(result.iss_position.latitude);
			coordinates.longitude = double.Parse(result.iss_position.longitude);
			Console.Write("- {0}, {1}\n", coordinates.latitude, coordinates.longitude);

			for (int i=10; i>0; i--)
			{
				Console.Write("{0}... ", i);
				Thread.Sleep(1000);
			}

			Console.Write("\n");
		}
	}



	//public IActionResult Index()
	public static ISS getISS()
	{
		const string apiUrl = "http://api.open-notify.org/iss-now.json";
		var client = new HttpClient();
		var response = client.GetAsync(apiUrl).Result;
		var content = response.Content.ReadAsStringAsync().Result;
		ISS model = JsonConvert.DeserializeObject<ISS>(content);
		return model;
	}
}


public class ISS
{
    public string message { get; set; }
    public int timestamp { get; set; }

	public Position iss_position { get; set; }

}

public class Position {
	public string latitude { get; set; }
	public string longitude { get; set; }
}

public class Vector {
	public double latitude { get; set; }
	public double longitude { get; set; }
}

