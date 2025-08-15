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
            _baseUrl = "https://vpic.nhtsa.dot.gov/api";

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

        public async Task<ApiResponse<List<CarModel>>> GetModelsForMakeAndYear(int makeId, int year)
        {
            try
            {
                var url = $"{_baseUrl}/vehicles/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<CarsResponse<CarModel>>(content);

                    if (apiResponse?.Results != null)
                    {
                        var sortedModels = apiResponse.Results
                            .GroupBy(m => m.Model_Name)
                            .Select(g => g.First())
                            .OrderBy(m => m.Model_Name)
                            .ToList();


                        return new ApiResponse<List<CarModel>>
                        {
                            IsSuccess = true,
                            Data = sortedModels
                        };
                    }
                }

                return new ApiResponse<List<CarModel>>
                {
                    IsSuccess = false,
                    ErrorMessage = $"API request failed with status: {response.StatusCode}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CarModel>>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Sommething went wrong : {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<List<VehicleType>>> GetVehicleTypesForMake(int makeId)
        {
            try
            {

                var url = $"{_baseUrl}/vehicles/GetVehicleTypesForMakeId/{makeId}?format=json";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<CarsResponse<VehicleType>>(content);

                    if (apiResponse?.Results != null)
                    {
                        var sortedTypes = apiResponse.Results
                            .OrderBy(vt => vt.VehicleTypeName)
                            .ToList();


                        return new ApiResponse<List<VehicleType>>
                        {
                            IsSuccess = true,
                            Data = sortedTypes
                        };
                    }
                }

                return new ApiResponse<List<VehicleType>>
                {
                    IsSuccess = false,
                    ErrorMessage = $"API request failed with status: {response.StatusCode}"
                };
            }

            catch (Exception ex)
            {
                return new ApiResponse<List<VehicleType>>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Sommething went wrong : {ex.Message}"
                };
            }
        }
    }
}
