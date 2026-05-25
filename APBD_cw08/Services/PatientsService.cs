using APBD_cw08.DTOs;
using APBD_cw08.Exceptions;
using APBD_cw08.Models;
using APBD_cw08.Repos;

namespace APBD_cw08.Services;

public class PatientsService
{
    private IDbRepo _repo;
    public PatientsService(IDbRepo repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<GetPatientDTO>> GetAllPatients(FilterDTO? filterDto)
    {
        
        return await _repo.GetAllPatients(filterDto);
    }

    public async Task<int> AddBedAssignment(PostBedDTO bedDto, string pesel)
    {
        //sprawdzic czy to > from
        //sprawdzic czy istnieje oddzial
        //sprawdzic czy istnieje typ lozka
        //sprawdzic czy jest takie wolne lozko w danym czasy
        bool wardExists = await _repo.WardExistsByName(bedDto.Ward);
        if (!wardExists)
        {
            throw new Exception("Nie oddziału o podanej nazwie");
        }
        bool bedTypeExists = await _repo.BedTypeExistsByName(bedDto.BedType);
        if (!bedTypeExists)
        {
            throw new NoSuchBedException();
        }
        int? bedId = await _repo.GetAvailableBedById(bedDto.From, bedDto.To, bedDto.BedType);
        if (!bedId.HasValue)
        {
            throw new Exception("Nie ma wolnego łóka w podanym przedziale czasowym");
        }
        var newBed = new BedAssignment()
        {
            PatientPesel = pesel,
            BedId = bedId.Value,
            From = bedDto.From,
            To = bedDto.To
        };
        var result = await _repo.PostBedAssignment(newBed);
        return 0;
    }
}