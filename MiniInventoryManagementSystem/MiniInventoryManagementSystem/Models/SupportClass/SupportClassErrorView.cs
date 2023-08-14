namespace MiniInventoryManagementSystem.Models.SupportClass
{
    public class SupportClassErrorView
    {
        public int id {  get; set; }
        public bool IsError { get; set; }
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
}
