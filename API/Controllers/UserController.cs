using API.Services;
using AutoMapper;
using DataAccess.Data;
using DataAccess.DTOs;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            return Ok(await _userRepository.GetUsers());
        }

        [Authorize]
        [HttpGet("get-user/{id:int}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            User? user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound($"User mit Id {id} ist nicht existiert.");
            }

            UserDto userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [Authorize]
        [HttpPost("add-user")]
        public async Task<ActionResult<UserDto>> AddAdmin([FromForm] UserRegisterDto userDto)
        {
            if (await _userRepository.ExistsByUsername(userDto.Username))
            {
                return BadRequest("username ist bereit existiert!");
            }

            if (await _userRepository.ExistsByEmail(userDto.Email))
            {
                return BadRequest("Email ist bereit existiert!");
            }

            if (userDto.Password != userDto.ConfirmPassword)
            {
                return BadRequest("Passwörte stimmen nicht überein!");
            }

            User user = _mapper.Map<User>(userDto);

            if (userDto.Photo != null)
            {
                //var imageUrl = await _fileService.UploadFile(userDto.Foto, ContainerName.admins);
                //if (imageUrl.IsNullOrEmpty())
                //{
                //    return Problem("Bild konnte nicht hochgeladen!");
                //}

                //user.Photo = imageUrl;
            }

            using var hmac = new HMACSHA512();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));
            user.PasswordSalt = hmac.Key;

            user.Id = await _userRepository.AddUser(user);

            UserDto registeredUser = _mapper.Map<UserDto>(user);

            return registeredUser;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDto>> LoginUser(UserLoginDto userLoginDto)
        {
            User? user = await _userRepository.GetUserByUsername(userLoginDto.UsernameOrEmail);

            if (user == null)
            {
                user = await _userRepository.GetUserByEmail(userLoginDto.UsernameOrEmail);
            }

            if (user == null)
            {
                return Unauthorized("Username or email ist ungültig!");
            }

            bool password = CheckPassword(user.PasswordSalt, user.PasswordHash, userLoginDto.Password);

            if (!password)
            {
                return Unauthorized("Passwort ist ungültig!");
            }

            UserTokenDto userToken = _mapper.Map<UserTokenDto>(user);
            userToken.Token = _tokenService.CreateToken(user);

            return userToken;
        }

        [Authorize]
        [HttpPut("edit-user")]
        public async Task<ActionResult<UserTokenDto>> EditUser([FromForm] UserEditDto userEdit)
        {
            User? user = await _userRepository.GetUserByEmail(userEdit.Email);

            if (user == null)
            {
                return NotFound($"Admin mit Emailadresse: {userEdit.Email} wurde nicht gefunden.");
            }

            bool password = CheckPassword(user.PasswordSalt, user.PasswordHash, userEdit.Password);

            if (!password)
            {
                return Unauthorized("Passwort ist ungültich!");
            }

            if (await _userRepository.ExistsByUsername(userEdit.Username)
                && user.Username != userEdit.Username)
            {
                return BadRequest("Username ist bereit existiert!");
            }

            _mapper.Map(userEdit, user);

            if (userEdit.Photo != null)
            {
                //var imageUrl = await _fileService.UploadFile(userEdit.Foto, ContainerName.admins);
                //if (imageUrl.IsNullOrEmpty())
                //{
                //    return Problem("Bild konnte nicht hochgeladen!");
                //}

                //if (!user.Foto.IsNullOrEmpty())
                //{
                //    await _fileService.DeleteFile(user.Foto, ContainerName.admins);
                //}

                //user.Foto = imageUrl;
            }

            await _userRepository.UpdateUser(user);

            UserTokenDto userToken = _mapper.Map<UserTokenDto>(user);
            userToken.Token = _tokenService.CreateToken(user);

            return userToken;
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound($"Admin with Id= {id} not found");
            }

            _userRepository.DeleteUser(id);

            //await _fileService.DeleteFile(user.Foto, ContainerName.admins);

            return Ok();
        }

        // Check Password
        private bool CheckPassword(byte[] PasswordSaltFromDB,
            byte[] PasswordHashFromDB, string PasswordFromClient)
        {
            using var hmac = new HMACSHA512(PasswordSaltFromDB);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(PasswordFromClient));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != PasswordHashFromDB[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
