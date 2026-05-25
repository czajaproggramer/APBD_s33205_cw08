namespace APBD_cw08.DTOs;

public class GetAdmissionDTO
{
    public int Id { get; set; }
    public DateTime admissionDate { get; set; }
    public DateTime? dischargeDate { get; set; }
    public GetWardDTO Ward { get; set; }
}