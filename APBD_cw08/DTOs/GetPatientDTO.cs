namespace APBD_cw08.DTOs;

public class GetPatientDTO
{
    public String Pesel { get; set; } = "";
    public String FirstName { get; set; } = "";
    public String LastName { get; set; } = "";
    public String Sex { get; set; } = "";
    public int Age { get; set; }
    public List<GetAdmissionDTO> Admissions { get; set; }= new List<GetAdmissionDTO>();
    public List<GetBedAssignmentDTO> BedAssignments { get; set; }= new List<GetBedAssignmentDTO>();

    
}