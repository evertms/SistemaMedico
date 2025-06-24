using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.UseCases.Specialties.GetAllSpecialties;
using SistemaMedico.Application.UseCases.Specialties.GetDoctorsBySpecialty;

namespace SistemaMedico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecialtiesController : ControllerBase
{
    private readonly IGetAllSpecialtiesHandler _getAllSpecialtiesHandler;
    private readonly IGetDoctorsBySpecialtyHandler _getDoctorsBySpecialtyHandler;

    public SpecialtiesController(
        IGetAllSpecialtiesHandler getAllSpecialtiesHandler,
        IGetDoctorsBySpecialtyHandler getDoctorsBySpecialtyHandler)
    {
        _getAllSpecialtiesHandler = getAllSpecialtiesHandler;
        _getDoctorsBySpecialtyHandler = getDoctorsBySpecialtyHandler;
    }

    // GET: api/specialties
    [HttpGet]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetAllSpecialties()
    {
        var specialties = await _getAllSpecialtiesHandler.HandleAsync();
        return Ok(specialties);
    }

    // GET: api/specialties/{specialtyId}/doctors
    [HttpGet("{specialtyId}/doctors")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetDoctorsBySpecialty(Guid specialtyId)
    {
        var query = new GetDoctorsBySpecialtyQuery
        {
            SpecialtyId = specialtyId
        };

        var doctors = await _getDoctorsBySpecialtyHandler.HandleAsync(query);
        return Ok(doctors);
    }
}
