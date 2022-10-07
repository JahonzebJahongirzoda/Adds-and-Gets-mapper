using System.Net;
using AutoMapper;
using Domain.Dtos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ChallengeService : IChallengeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ChallengeService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetChallengeDto>>> GetChallenges()
    {
        var locations = _mapper.Map<List<GetChallengeDto>>(_context.Challanges.ToList());
        return new Response<List<GetChallengeDto>>(locations);
    }
    
    public async Task<Response<List<GetChallengeWithGroupsDto>>> GetChallengeWithGroups()
    {
        var challanges = await (
            from ch in _context.Challanges
            select new GetChallengeWithGroupsDto()
            {
                Description = ch.Description,
                Id = ch.Id,
                Title = ch.Title,
                Groups = (
                    from gr in _context.Groups
                    where gr.ChallangeId == ch.Id
                    select _mapper.Map<GetGroupDto>(gr)
                ).ToList()
            }
        ).ToListAsync();

        return new Response<List<GetChallengeWithGroupsDto>>(challanges);
    }


    //add location 
    public async Task<Response<AddChallengeDto>> AddChallenge(AddChallengeDto model)
    {
        try
        {
            var group = _mapper.Map<Challange>(model);
            await _context.Challanges.AddAsync(group);
            await _context.SaveChangesAsync();
            model.Id = group.Id;
            return new Response<AddChallengeDto>(model);
        }
        catch (System.Exception ex)
        {
            return new Response<AddChallengeDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetChallengeDto>> GetChallengeById(int id)
    {
        var find = _mapper.Map<GetChallengeDto>(await _context.Challanges.FindAsync(id));
        if (find == null) return new Response<GetChallengeDto>(HttpStatusCode.NotFound, "");
        return new Response<GetChallengeDto>(find);
    }

    //add location 
    public async Task<Response<AddChallengeDto>> UpdateChallenge(AddChallengeDto groupDto)
    {
        try
        {
            var finds = await _context.Challanges.FindAsync(groupDto.Id);
            if (finds == null) return new Response<AddChallengeDto>(System.Net.HttpStatusCode.NotFound, "");

            // if location is found
            finds.Title = groupDto.Title;
            finds.Id = groupDto.Id;
            finds.Description = groupDto.Description; 
   
            await _context.SaveChangesAsync();
            return new Response<AddChallengeDto>(groupDto);
        }
        catch (System.Exception ex)
        {
            return new Response<AddChallengeDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    //add location 
    public async Task<Response<string>> DeleteChallenge(int id)
    {
        try
        {
            var find = await _context.Challanges.FindAsync(id);
            if (find == null) return new Response<string>(System.Net.HttpStatusCode.NotFound, "");

            _context.Challanges.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<string>("removed successfully");
        }
        catch (System.Exception ex)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}