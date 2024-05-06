using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using User_UserApi.Models;
using User_UserApi.Models.ViewModel;
using User_UserApi.Repository.IRepository;



namespace User_UserApi.Controllers
{
    [Route("api/UserApi")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        public enum Status
        {
            Draft = 1,
            Submitted = 2,
            Approved = 3,
            Rejected = 4,

        }

        public readonly IUnitOfWork _unitOfWork;
    
        public UserApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          

        }
        [HttpGet]
        public IActionResult UserList()
        {
            var users = _unitOfWork.UserRepository.UserIndex().ToList();

            var data = users.Select(u=> new UserDto
            {
                UserId = u.UserId,
                Name = u.Name,
                CountryId = u.CountryId,
                StateId = u.StateId,
                CityId = u.CityId,
                Status = u.Status,
                Count = u.Count,
                Country = u.Country,
                States = u.States,
                Cities = u.Cities
                
            });
            
            return Ok(data);
        }

        [HttpGet("CountryList")]
        public IActionResult CountryList()
        {
            var data = _unitOfWork.CountryRepository.GetAll().ToList();
            return Ok(data);

        }
        [HttpGet("StateList")]
        public IActionResult StateList(int countryId)
        {
            var data = _unitOfWork.StateRepository.GetAll().Where(u => u.CountryId == countryId);
            return Ok(data); // Return states as JSON

        }
        [HttpGet("CityList")]
        public IActionResult CityList(int stateId)
        {
            var data = _unitOfWork.CityRepository.GetAll().Where(u => u.StateId == stateId);
            return Ok(data); // Return states as JSON

        }


        [HttpGet("{id:int}", Name = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var user = _unitOfWork.UserRepository.Get(u => u.UserId == id, includeProperties: "Country,States,Cities");
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDto> Add([FromBody]UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(userDto);
            }
            if (userDto.UserId > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (ModelState.IsValid)
            {
                Status status = Status.Draft;
                int statusValue = (int)status;

                var obj = new User
                {
                    Name = userDto.Name,
                    Count = 1,
                    CountryId = userDto.CountryId,
                    CityId = userDto.CityId,
                    StateId = userDto.StateId,
                    Status = statusValue

                };
                _unitOfWork.UserRepository.Add(obj);
                _unitOfWork.Save();
                return CreatedAtRoute("User", new { id = obj.UserId }, obj);
            }
            return Ok(userDto); 
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "Delete")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var user = _unitOfWork.UserRepository.Get(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            _unitOfWork.UserRepository.Remove(user);
            _unitOfWork.Save();

            return NoContent();
        }


        [HttpPut("{id:int}", Name = "Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult Update(int id, UserDto userDto)
        {
            if (userDto == null || id != userDto.UserId)
            {
                return BadRequest();
            }

            User user = new User
            {
                UserId = userDto.UserId,
                Name = userDto.Name,
                CountryId = userDto.CountryId,
                StateId = userDto.StateId,
                CityId = userDto.CityId,
                Status = userDto.Status,
                Count = userDto.Count
            };

          
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();

            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartial(int id, JsonPatchDocument<UserDto> patchUserDto)
        {
            if(patchUserDto==null || id== 0)
            {
                return BadRequest();
            }
            var existinguser = _unitOfWork.UserRepository.Get(u => u.UserId == id);
            if(existinguser == null)
            {
                return NotFound();  
            }
            UserDto obj = new UserDto
            {
                UserId = existinguser.UserId,
                Name = existinguser.Name,
                CountryId = existinguser.CountryId,
                StateId = existinguser.StateId,
                CityId = existinguser.CityId,
                Status = existinguser.Status,
                Count = existinguser.Count
            };
            patchUserDto.ApplyTo(obj, ModelState);


            User user = new User
            {
                UserId = obj.UserId,
                Name = obj.Name,
                CountryId = obj.CountryId,
                StateId = obj.StateId,
                CityId = obj.CityId,
                Status = obj.Status,
                Count = obj.Count
            };

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
            if(!ModelState.IsValid)
            {
                return BadRequest();    
            }
            return NoContent();

        }
    }
}
