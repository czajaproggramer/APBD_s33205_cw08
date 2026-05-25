using APBD_cw08.DAK;
using APBD_cw08.DTOs;
using APBD_cw08.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_cw08.Repos;

public class EFDbRepo : IDbRepo
{
    private UniversityTasksDbContext _context;

    public EFDbRepo(UniversityTasksDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<GetPatientDTO>> GetAllPatients(FilterDTO? filterDto)
    {
        var result = await _context.Patients
            .Where(p => p.FirstName.Contains(filterDto.FirstName) 
                        || p.FirstName.Contains(filterDto.Search)
                        || p.LastName.Contains(filterDto.LastName) 
                        || p.LastName.Contains(filterDto.Search))
            .Select(p => new GetPatientDTO()
                {
                    Age = p.Age, FirstName = p.FirstName, LastName = p.LastName, Pesel = p.Pesel,
                    Sex = p.Sex ? "Male" : "Female",
                    Admissions = p.Admissions.Select(a => new GetAdmissionDTO()
                    {
                        admissionDate = a.AdmissionDate, Id = a.Id,
                        Ward = new GetWardDTO()
                        {
                            Description = a.Ward.Description, Id = a.WardId, Name = a.Ward.Name
                        }
                    }).ToList(),
                    BedAssignments = p.BedAssignments.Select(bAs => new GetBedAssignmentDTO(){
                        Id = bAs.Id, From = bAs.From, To = bAs.To,
                        Bed = new GetBedTypeDTO()
                        {
                            Description = bAs.Bed.BedType.Description,
                            Name = bAs.Bed.BedType.Name,
                            Id = bAs.Bed.BedType.Id,
                        },
                        Room = new GetRoomDTO()
                        {
                            Id = bAs.Bed.Room.Id,
                            HasTv = bAs.Bed.Room.HasTv,
                            Ward = new GetWardDTO()
                            {
                                Description = bAs.Bed.Room.Ward.Description,
                                Id = bAs.Bed.Room.Ward.Id,
                                Name = bAs.Bed.Room.Ward.Name,
                            }
                        }
                    }).ToList()
                }
            ).AsNoTracking()
            .ToListAsync();
        return result;
    }

    public async Task<int> PostBedAssignment(BedAssignment bedA)
    {
        _context.Add(bedA);
        await _context.SaveChangesAsync();
        return 1;
    }

    public async Task<bool> WardExistsByName(string name)
    {
        Console.WriteLine($"Sprawdzam czy istnieje oddział {name}");
        return await _context.Wards.AsNoTracking()
            .Where(w => w.Name == name)
            .AnyAsync();
    }

    public async Task<bool> BedTypeExistsByName(string name)
    {
        return await _context.BedTypes.AsNoTracking().Where(bt => bt.Name == name).AnyAsync();
    }

    public async Task<int?> GetAvailableBedById(DateTime from, DateTime? to, string bedTypeName)
    {
        int? id = await _context.Beds.AsNoTracking().Include(b => b.BedType)
            .Where(b => b.BedType.Name == bedTypeName 
            && b.BedAssignments.Any(bd => 
                (bd.From <= from && bd.To < to) || bd.From >= to))
            .Select(b => b.Id)
            .FirstAsync();

        return id;
    }
}