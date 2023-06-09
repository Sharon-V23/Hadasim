﻿using System;
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
        [HttpPost("getPatientWithoutVaccineCount")]
        public ActionResult <int> getPatientWithoutVaccineCount()
        {
            var PatientWithoutVaccineCount = _iPatientBll.getPatientWithoutVaccineCount();
            //var  = _iPatientBll.getAllCoronaPatient();
            return Ok(PatientWithoutVaccineCount);

        }



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
            if (patientDto == null)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "The object cannot be null"
                });
            }
            if (patientDto.Tz.Length != 9)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "The ID number must contain 9 digits"
                });

            }
            if (patientDto.vaccines?.Count > 4)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "You cannot get more than 4 vaccinations"
                });
            }
            if (patientDto.birth_date > DateTime.Today)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "It is not possible to enter a date later than today"
                });


            }
            if (patientDto.Corona_Detail?.posutuve_result > DateTime.Today)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "It is not possible to enter a date later than today"
                });
            }
            if (patientDto.telephone.Length != 9)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "The phone number must contain 9 digits"
                });
            }
            if (patientDto.mobile_phone.Length != 10)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "The phone number must contain 10 digits"
                });
            }
            if (patientDto.Corona_Detail?.posutuve_result.AddDays(14) != patientDto.Corona_Detail?.recovery_date)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,

                    returnCodeDesc = "The recovery time is 14 days"
                });
            }
            if (patientDto.vaccines!=null&&( patientDto.vaccines[0]?.veccine_date < patientDto.birth_date || patientDto.vaccines[1]?.veccine_date < patientDto.birth_date || patientDto.vaccines[2]?.veccine_date < patientDto.birth_date || patientDto.vaccines[3]?.veccine_date < patientDto.birth_date))
            {
                return BadRequest(new RequestResult
                {


                    returnCode = -1,
                    returnCodeDesc = "It is illegal to enter a vaccination date before the patient birth date"
                });
            }
            
            
            if (patientDto.vaccines != null && (patientDto.vaccines[0]?.veccine_date > DateTime.Now || patientDto.vaccines[1]?.veccine_date > DateTime.Now || patientDto.vaccines[2]?.veccine_date > DateTime.Now || patientDto.vaccines[3]?.veccine_date > DateTime.Now))
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "You can't introduce a vaccine that hasn't happened yet"
                });
            }

            if (patientDto.vaccines != null) {  
                foreach(var vaccine in patientDto.vaccines)
            {
                if (vaccine?.manufacturer != "Pfizer" || vaccine?.manufacturer == "Moderna" || vaccine?.manufacturer == "AstraZeneca" || vaccine?.manufacturer == "Novavax")

                {
                    return BadRequest(new RequestResult
                    {
                        returnCode = -1,
                        returnCodeDesc = "There is no manufacturer by this name"
                    });
                }
            }}

            if (int.TryParse(patientDto.Tz,out int number)==false)
            {
                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "You need to enter numbers only"
                });
            }


            var createPatient = _iPatientBll.addPatient(patientDto);
            if (createPatient==-1)

                return BadRequest(new RequestResult
                {
                    returnCode = -1,
                    returnCodeDesc = "This user already exists in the system"
                });
      
            return Ok(new RequestResult
            {
                returnCode = createPatient,
                returnCodeDesc = "success"
            });
        }
    }
}



