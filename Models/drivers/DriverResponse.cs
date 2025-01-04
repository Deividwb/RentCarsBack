namespace RentCars_Back.Models
{
    public class DriverResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PaymentDay { get; set; }
        public string Location { get; set; }
        public string Cep { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Uf { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}