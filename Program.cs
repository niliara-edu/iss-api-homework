using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

class Program {

	public static void Main(string[] args){
		while (true)
		{
			Vector coordinates = new Vector();
			updateISS(ref coordinates);
			updateLocation(coordinates);
			applyDelay();
		}
	}

	public static void updateISS(ref Vector coordinates)
	{
		ISS result = getISS();
		coordinates.latitude = double.Parse(result.iss_position.latitude);
		coordinates.longitude = double.Parse(result.iss_position.longitude);
		Console.Write("Current coordinates: ({0}, {1})\n", coordinates.latitude, coordinates.longitude);
	}

	public static ISS getISS()
	{
		const string apiUrl = "http://api.open-notify.org/iss-now.json";
		var client = new HttpClient();
		var response = client.GetAsync(apiUrl).Result;
		var content = response.Content.ReadAsStringAsync().Result;
		ISS model = JsonConvert.DeserializeObject<ISS>(content);
		return model;
	}


	public static void updateLocation (Vector coordinates)
	{
		string location = getLocation(coordinates);
		Console.Write("Current country: {0}\n", location);

	}

	public static string getLocation (Vector coordinates)
	{
		//string apiUrl = String.Format("http://api.geonames.org/countryCode?lat=20&lng=5&username=niliara.edu", coordinates.latitude, coordinates.longitude);
		string apiUrl = String.Format("http://api.geonames.org/countrySubdivisionJSON?lat={0}&lng={1}&username=niliara.edu", coordinates.latitude, coordinates.longitude);
		//string apiUrl = String.Format("http://api.geonames.org/citiesJSON?north=44.1&south=-9.9&east=-22.4&west=55.2&lang=de&username=demo");
		var client = new HttpClient();
		var response = client.GetAsync(apiUrl).Result;
		var content = response.Content.ReadAsStringAsync().Result;
		CountryName countryName = JsonConvert.DeserializeObject<CountryName>(content);
		if (countryName.countryName == null) {
			return "NA";
		}

		return countryName.countryName;
	}
	

	public static void applyDelay()
	{
		Console.Write("\n");

		for (int i=10; i>0; i--)
		{
			Console.Write("{0}... ", i);
			Thread.Sleep(1000);
		}

		Console.Write("\n\n");
	}






	public static string getCountry(double latitude, double longitude)
	{
		return "sex";
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

public class CountryName {
	public string countryName { get; set; }
}

