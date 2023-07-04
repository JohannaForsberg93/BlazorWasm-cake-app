using System.ComponentModel.DataAnnotations;

namespace BlazorCakeApp.Shared
	{
	public class Test
		{
			[Required(ErrorMessage = "Eventet måste ha ett namn!")]
			public string Name { get; set; }
			public string Description { get; set; }

		}
	}
