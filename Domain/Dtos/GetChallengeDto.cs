using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class GetChallengeDto
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Description  { get; set; }
}

public class GetChallengeWithGroupsDto
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Description  { get; set; }
    public List<GetGroupDto> Groups { get; set; }
}