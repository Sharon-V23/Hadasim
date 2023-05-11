using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entity;
using Bll;
using Dto;
using Dll;
using AutoMapper.Execution;
using System.Net;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientBll _iPatientBll;

        public PatientsController(IPatientBll IPatientBll)
        {
            _iPatientBll = IPatientBll;
        }


        //[HttpGet]
        //public IActionResult GetAllPatients()
        //{
        //    var members = PatientBll.getAllPatients();
        //    return Ok(members);
        //}
        [HttpGet]
        public ActionResult<List<PatientWithIdDto>> GetPatients()
        {
            
            var patients = _iPatientBll.getAllPatients();

            return Ok(patients);
        }
        //[HttpGet]
        //public ActionResult<List<PatientWithIdDto>> GetCoronaPatients()
        //{

        //    var patients = _iPatientBll.getAllCoronaPatients();

        //    return Ok(patients);
        //}

        [HttpGet("{id}")]
        public ActionResult<PatientDto> GetPatientById(int id)
        {
            var patient = _iPatientBll.getPatientById(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
        //[HttpPost]
       

        //return CreatedAtAction(nameof(GetPatientById), new { id = createdPatient.Id }, createdPatient);
    //}
        [HttpPost("addPatient")]
        public ActionResult<RequestResult> AddPatient([FromBody] PatientDto patientDto)
        {
           if (patientDto == null){
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "The object cannot be null"
                });                
            }
            if (patientDto.Tz.Length!=9)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "The ID number must contain 9 digits"
                });

            }
            if (patientDto.vaccines.Count>4)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "You cannot get more than 4 vaccinations"
                });
            }
            if (patientDto.birth_date>DateTime.Today)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "It is not possible to enter a date later than today"
                });

            }
            if (patientDto.telephone.Length!= 9)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "The phone number must contain 9 digits"
                });
            }
            if (patientDto.mobile_phone.Length!=10)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "The phone number must contain 10 digits"
                });
            }
            if (patientDto.vaccines[0]?.veccine_date>DateTime.Now|| patientDto.vaccines[1]?.veccine_date > DateTime.Now || patientDto.vaccines[2]?.veccine_date > DateTime.Now || patientDto.vaccines[3]?.veccine_date > DateTime.Now)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "You can't introduce a vaccine that hasn't happened yet"
                });
            }
            //if (patientDto.vaccines[0].manufacturer==)
            //{
            //    return BadRequest(new RequestResult
            //    {
            //        returnCode = -1,
            //        returnCodeDesc = "The phone number must contain 10 digits"
            //    });
            //}
            var createPatient = _iPatientBll.addPatient(patientDto);

            return Ok(new RequestResult
            {
                returnCode =createPatient,
                returnCodeDesc = "success"
            });
        }
        //    [HttpPost]
        //public void addPatient([FromBody] string value)
        //    {
        //    }


    } }



