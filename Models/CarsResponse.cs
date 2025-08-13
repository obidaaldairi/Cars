namespace Models
{
    public class CarsResponse<T>
    {
        public CarsResponse()
        {
            Results = new List<T>();
        }
        public int Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public List<T> Results { get; set; }
    }
}
