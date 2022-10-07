using Domain.Entities;

namespace Domain.Dtos;

public class GetGroupsWithParticipantsDto
{
        public int Id { get; set; }
        public string GroupNick { get; set; }
        public int ChallangeId { get; set; }
        public virtual Challange Challange { get; set; }
        public bool NeededMember { get; set; }
        public string TeamSlogan { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual List<GetParticipantDto> Participants { get; set; }
    
}