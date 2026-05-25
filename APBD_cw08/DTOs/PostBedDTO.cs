namespace APBD_cw08.DTOs;

public class PostBedDTO
{
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public string BedType { get; set; } = "";
    public string Ward { get; set; } = "";
}