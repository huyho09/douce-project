using Newtonsoft.Json;
using ScentifyAdmin.Models.Dtos;

namespace ScentifyAdmin.Libs
{
	public static class JsonHelpers
	{
		public static List<T> ParseJson<T>(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return new List<T>();
			}

			if (json.Trim().StartsWith("["))
			{
				return JsonConvert.DeserializeObject<List<T>>(json);
			}
			else
			{
				var singleObject = JsonConvert.DeserializeObject<T>(json);
				return new List<T> { singleObject };
			}
		}

	}
}
