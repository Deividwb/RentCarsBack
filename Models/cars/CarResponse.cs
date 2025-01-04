using System.ComponentModel.DataAnnotations.Schema;

namespace RentCars_Back.Models
{
    public class CarResponse
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Plate { get; set; }
        public int ModelYear { get; set; }
        public string MonthLicensing { get; set; }
        public int NumberRenavam { get; set; }
        public string ColorCar { get; set; }
        public int Id_Driver { get; set; }
        public string DriverName { get; set; }
        private string Image { get; set; }
        private List<string> imageUrls = new List<string>();
        public List<string> ImageUrls
        {
            get
            {
                if (!string.IsNullOrEmpty(Image))
                {

                    var urls = Image.Replace("{", "").Replace("}", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(x => x.Trim())
                                    .ToList();
                    imageUrls.AddRange(urls);
                }

                return imageUrls;
            }
            set => imageUrls = value;
        }

    }
}
