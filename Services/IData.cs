using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IData
    {
        Task<ApiResponse<List<CarMake>>> GetAllMakes();
        Task<ApiResponse<List<VehicleType>>> GetVehicleTypesForMake(int makeId);
        Task<ApiResponse<List<CarModel>>> GetModelsForMakeAndYear(int makeId, int year);
    }
}
