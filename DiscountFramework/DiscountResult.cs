namespace DiscountFramework
{
    public class DiscountResult
    {
        public DiscountCart Cart { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; } 
    }
}