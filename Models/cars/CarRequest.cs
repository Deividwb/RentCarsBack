namespace RentCars_Back.Models
{
    public class CarRequest
    {

        public string Brand { get; set; }
        public string Plate { get; set; }
        public int ModelYear { get; set; }
        public string MonthLicensing { get; set; }
        public int NumberRenavam { get; set; }
        public string ColorCar { get; set; }
        public int Id_Driver { get; set; }
        public List<string>? ImageUrls { get; set; }
    }
}


