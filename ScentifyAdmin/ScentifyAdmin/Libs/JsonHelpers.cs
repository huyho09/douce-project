using Newtonsoft.Json;
using ScentifyAdmin.Models.Dtos;

namespace ScentifyAdmin.Libs
{
	public static class JsonHelpers
	{
		public static List<ProductSize> ParseJson(string json)
		{
			if (json.Trim().StartsWith("["))
			{
				return JsonConvert.DeserializeObject<List<ProductSize>>(json);
			}
			else
			{
				var singleObject = JsonConvert.DeserializeObject<ProductSize>(json);
				return new List<ProductSize> { singleObject };
			}
		}
	}
}
