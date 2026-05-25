namespace APBD_cw08.DTOs;

public class GetRoomDTO
{
    public string Id { get; set; } = "";
    public bool HasTv { get; set; }
    public GetWardDTO Ward { get; set; }
}