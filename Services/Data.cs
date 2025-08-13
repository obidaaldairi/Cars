using Models;
using Newtonsoft.Json;

namespace Services
{
    public class Data : IData
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public Data(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl ="https://vpic.nhtsa.dot.gov/api";

        }

        public async Task<ApiResponse<List<CarMake>>> GetAllMakes()
        {
            try
            {

                var url = $"{_baseUrl}/vehicles/getallmakes?format=json";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<CarsResponse<CarMake>>(content);

                    if (apiResponse?.Results != null)
                    {
                        var data = apiResponse.Results
                            .OrderBy(m => m.Make_Name)
                            .ToList();


                        return new ApiResponse<List<CarMake>>
                        {
                            IsSuccess = true,
                            Data = data
                        };
                    }
                }

                return new ApiResponse<List<CarMake>>
                {
                    IsSuccess = false,
                    ErrorMessage = $"API request failed with status: {response.StatusCode}"
                };
            }

            catch (Exception ex)
            {
                return new ApiResponse<List<CarMake>>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Sommething went wrong : {ex.Message}"
                };
            }
        }

        public Task<ApiResponse<List<CarModel>>> GetModelsForMakeAndYear(int makeId, int year)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<VehicleType>>> GetVehicleTypesForMake(int makeId)
        {
            throw new NotImplementedException();
        }
    }
}
