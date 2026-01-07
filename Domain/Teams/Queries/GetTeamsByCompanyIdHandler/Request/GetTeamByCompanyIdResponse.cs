using Application.Models.DTOs;

namespace Domain.Teams.Queries.GetTeamsByCompanyIdHandler.Request;

public class GetTeamsByCompanyIdResponse
{
    public IEnumerable<TeamDTO> Teams { get; set; }
}