using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class GroupService : IGroupService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GroupService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<List<GetGroupsWithParticipantsDto>>> GetGroupsWithParticipants()
    {
        var participant = await (
            from ch in _context.Groups
            select new GetGroupsWithParticipantsDto()
            {
                Challange = ch.Challange,
                Id = ch.Id,
                ChallangeId = ch.ChallangeId,
                CreatedAt = ch.CreatedAt,
                GroupNick = ch.GroupNick,
                NeededMember = ch.NeededMember,
                TeamSlogan = ch.TeamSlogan, 
                Participants= (
                    from gr in _context.Participants
                    where gr.Id == ch.Id
                    select _mapper.Map<GetParticipantDto>(gr) 
                    
                ).ToList()
            }
        ).ToListAsync();

        return new Response<List<GetGroupsWithParticipantsDto>>(participant);
    }
    public async Task<Response<List<GetGroupDto>>> GetGroups()
    {
        var locations = _mapper.Map<List< GetGroupDto >> (_context.Groups.ToList());
        return new Response<List<GetGroupDto>>(locations);
    }

    //add location 
    public async Task<Response<AddGroupDto>> AddGroup(AddGroupDto model)
    {
        try
        {
            var group = _mapper.Map<Group>(model);
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            model.Id = group.Id;
            return new Response<AddGroupDto>(model);
        }
        catch (System.Exception ex)
        {
            return new Response<AddGroupDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetGroupDto>> GetGroupById(int id)
    {
        var find = _mapper.Map<GetGroupDto>(await _context.Groups.FindAsync(id));
        if (find == null) return new Response<GetGroupDto>(HttpStatusCode.NotFound, "");
        return new Response<GetGroupDto>(find);
    }

    //add location 
    public async Task<Response<AddGroupDto>> UpdateGroup(AddGroupDto groupDto)
    {
        try
        {
            var finds = await _context.Groups.FindAsync(groupDto.Id);
            if (finds == null) return new Response<AddGroupDto>(System.Net.HttpStatusCode.NotFound, "");

            // if location is found
            finds.ChallangeId = groupDto.ChallangeId;
            finds.Id = groupDto.Id;
            finds.GroupNick = groupDto.GroupNick; 
            finds.NeededMember = groupDto.NeededMember;
            finds.TeamSlogan = groupDto.TeamSlogan; 
            finds.CreatedAt = groupDto.CreatedAt;
            
            await _context.SaveChangesAsync();
            return new Response<AddGroupDto>(groupDto);
        }
        catch (System.Exception ex)
        {
            return new Response<AddGroupDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    //add location 
    public async Task<Response<string>> DeleteGroup(int id)
    {
        try
        {
            var find = await _context.Groups.FindAsync(id);
            if (find == null) return new Response<string>(System.Net.HttpStatusCode.NotFound, "");

            _context.Groups.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<string>("removed successfully");
        }
        catch (System.Exception ex)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}