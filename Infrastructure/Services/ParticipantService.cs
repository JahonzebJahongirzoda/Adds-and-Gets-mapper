using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ParticipantService : IParticipantService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ParticipantService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetParticipantDto>>> GetParticipants()
    {
        var locations = _mapper.Map<List<GetParticipantDto>>(_context.Participants.ToList());
        return new Response<List<GetParticipantDto>>(locations);
    }

    //add location 
    public async Task<Response<AddParticipantDto>> AddParticipant(AddParticipantDto model)
    {
        try
        {
            var participant = _mapper.Map<Participant>(model);
            await _context.Participants.AddAsync(participant);
            await _context.SaveChangesAsync();
            model.Id = participant.Id;
            return new Response<AddParticipantDto>(model);
        }
        catch (System.Exception ex)
        {
            return new Response<AddParticipantDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetParticipantDto>> GetParticipantById(int id)
    {
        var find = _mapper.Map<GetParticipantDto>(await _context.Participants.FindAsync(id));
        if (find == null) return new Response<GetParticipantDto>(HttpStatusCode.NotFound, "");
        return new Response<GetParticipantDto>(find);
    }

    //add location 
    public async Task<Response<AddParticipantDto>> UpdateParticipant(AddParticipantDto participantDto)
    {
        try
        {
            var finds = await _context.Participants.FindAsync(participantDto.Id);
            if (finds == null) return new Response<AddParticipantDto>(System.Net.HttpStatusCode.NotFound, "");

            // if location is found
            finds.FullName = participantDto.FullName;
            finds.Id = participantDto.Id;
            finds.Email = participantDto.Email; 
            finds.Phone = participantDto.Phone;
            finds.GroupId = participantDto.GroupId; 
            finds.CreatedAt = participantDto.CreatedAt;
            
            await _context.SaveChangesAsync();
            return new Response<AddParticipantDto>(participantDto);
        }
        catch (System.Exception ex)
        {
            return new Response<AddParticipantDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    //add location 
    public async Task<Response<string>> DeleteParticipant(int id)
    {
        try
        {
            var find = await _context.Participants.FindAsync(id);
            if (find == null) return new Response<string>(System.Net.HttpStatusCode.NotFound, "");

            _context.Participants.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<string>("removed successfully");
        }
        catch (System.Exception ex)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}