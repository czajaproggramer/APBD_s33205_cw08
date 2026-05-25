using System.Runtime.InteropServices.JavaScript;
using APBD_cw08.DTOs;
using APBD_cw08.Models;

namespace APBD_cw08.Repos;

public interface IDbRepo
{
    public Task<List<GetPatientDTO>> GetAllPatients(FilterDTO? filterDto);
    public Task<int> PostBedAssignment(BedAssignment bed);
    // bool wardExists = true;
    // bool bedTypeExists = true;
    // bool bedAvailableInPeriod = true;
    //
    // int bedId = 1;
    public Task<bool> WardExistsByName(string name);
    public Task<bool> BedTypeExistsByName(string name);
    public Task<int?> GetAvailableBedById(DateTime from, DateTime? to, string bedTypeName);
}