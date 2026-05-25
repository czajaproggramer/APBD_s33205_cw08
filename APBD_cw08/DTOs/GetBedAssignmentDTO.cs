namespace APBD_cw08.DTOs;

public class GetBedAssignmentDTO
{
    public int Id { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public GetBedTypeDTO Bed { get; set; }
    public GetRoomDTO Room { get; set; }
}