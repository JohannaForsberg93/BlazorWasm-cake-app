namespace BlazorCakeApp.Shared.Dto
{
    public class CakeDto
    {
        public string Id { get; set; }
        public string CakeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Ingredients { get; set; } = string.Empty;
        public string Allergens { get; set; } = string.Empty;
        public bool Available { get; set; }
    }
}
