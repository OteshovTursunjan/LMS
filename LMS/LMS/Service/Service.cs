using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using LMS.DTOs;
using LMS.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using LMS.Repository;
//using LMS.DTOs;

namespace LMS.Service
{
    public interface IAppService
    {
        Task<IEnumerable<Group>> GetAllGroup(Expression<Func<Group, bool>> expression = null);
        Task<Group> UpdateGroup(Group dto);
        Task DeleteGroup(Group group);
        Task<IEnumerable<Group>> GetGroup(int id);

        Task<IEnumerable<Contract>> GetAllContract(Expression<Func<Contract, bool>> expression = null);
        Task<Contract> AddContract(Contract contract);
        Task<Contract> UpdateContract(Contract contract);
        Task<IEnumerable<Contract>> GetContract(int id);
        Task DeleteContract(Contract contract);

        Task<IEnumerable<Subject>> GetAllSubject(Expression<Func<Subject, bool>> expression = null);
        Task<Subject> UpdateSubject(Subject subject);
        Task<Subject> AddSubject(Subject subject);
        Task<IEnumerable<Subject>> GetSubject(int id);
        Task DeleteSubject(Subject subject);

        Task<IEnumerable<Student>> GetAllStudent(Expression<Func<Student, bool>> expression = null);
        Task UpdateStudent(UStudentDTO student);
        Task DeleteStudent(int id);
        Task CreateStudent(Student student);
        Task<IEnumerable<Student>> GetStudent(int id);

        Task<WorkTable> AddWorkTable(WorkTable workTable);
        Task<IEnumerable<WorkTable>> GetAllWorkTable(Expression<Func<WorkTable, bool>> expression = null);
        Task UpdateWorkTable(WorkTable workTable);
        Task DeleteWorkTable(WorkTable workTable);

        Task<Group> AddGroup(Group group);
        Task<ResponseDTO<LoginResponseDTO?>> GetToken(LoginDTO dto);
        Task<Student> CreateStudent(StudentDTO dto);
        Task<ResponseDTO<UserResponseDTO>> CreateUser(UserDTO dto);
        Task<ResponseDTO<UserResponseDTO>> UpdateUser(UserDTO dto);
        Task<LoginDTO> DeleteUser(LoginDTO dto);


    }

    public class AppService : IAppService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IWorkTableRepository _workTableRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUserRepository _userRepository;

        public AppService(
            IGroupRepository groupRepository,
            IContractRepository contractRepository,
            IStudentRepository studentRepository,
            IWorkTableRepository workTableRepository,
            ISubjectRepository subjectRepository,
            IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _contractRepository = contractRepository;
            _studentRepository = studentRepository;
            _workTableRepository = workTableRepository;
            _subjectRepository = subjectRepository;
            _userRepository = userRepository;
        }
        #region User
        public async Task<ResponseDTO<LoginResponseDTO?>> GetToken(LoginDTO dto)
        {
            User a = new User();
            var user = (await _userRepository.GetAll(x => x.Login == dto.Login &&
            x.Password == dto.Password && a.IsDelete == false)).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            try
            {
                List<Claim> claims = new List<Claim>
     {
      new Claim(ClaimTypes.Name, user.Name),
      new Claim(ClaimTypes.Role, user.Role)
    };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom" +
                  " Secret key for authentication"));

                var token = new JwtSecurityToken(
                  expires: DateTime.UtcNow.AddDays(20),
                  claims: claims,
                  signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
                UserResponseDTO userDto = new UserResponseDTO();
                userDto.Login = user.Login;
                userDto.Name = user.Name;
                userDto.Role = user.Role;
                var data = new LoginResponseDTO()
                {
                    token = tokenStr,
                    user = userDto
                };

                return new ResponseDTO<LoginResponseDTO>() { Data = data };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<LoginResponseDTO?>() { Error = ex.Message };
            }
        }




        public async Task<ResponseDTO<UserResponseDTO>> CreateUser(UserDTO dto)
        {
            var user = (await _userRepository.GetAll(x => x.Login == dto.Login && x.Password == dto.Password)).FirstOrDefault();
            if (user != null)
            {
                throw new Exception("This login is already exist");
            }
            var newUser = new User
            {
                Name = dto.Name,
                Password = dto.Password,
                Role = dto.Role,
                Login = dto.Login,
                IsDelete = false
            };
            await _userRepository.CreateAsync(newUser);

            return new ResponseDTO<UserResponseDTO>()
            {
                Data = new UserResponseDTO
                {
                    Name = dto.Name,
                    Login = dto.Login,
                    Role = dto.Role,
                }
            };


        }
        public async Task<ResponseDTO<UserResponseDTO>> UpdateUser(UserDTO dto)
        {
            var res = (await _userRepository.GetAll(x => x.Login == dto.Login)).FirstOrDefault();
            if (res == null)
                return new ResponseDTO<UserResponseDTO>() { Error = "The owner of this information does not exist" };

            res.Name = dto.Name;
            res.Password = dto.Password;
            res.Role = dto.Role;
            await _userRepository.UpdateAsync(res);
            return new ResponseDTO<UserResponseDTO>()
            {
                Data = new UserResponseDTO
                {
                    Name = res.Name,
                    Login = res.Login,
                    Role = res.Role
                }
            };
        }
        public async Task<ResponseDTO<UserResponseDTO>> DeleteUser([FromBody] string delete_login)
        {
            var user = (await _userRepository.GetAll(x => x.Login == delete_login)).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            user.IsDelete = true;
            await _userRepository.UpdateAsync(user);
            return new ResponseDTO<UserResponseDTO>()
            {
                Data = new UserResponseDTO
                {
                    Name = user.Name,
                    Login = user.Login,
                    Role = user.Role
                }
            };
        }


        public async Task<LoginDTO> DeleteUser(LoginDTO dto)
        {
            var user = (await _userRepository.GetAll(x => x.Login == dto.Login && x.Password == dto.Password)).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("This user does not exist");
            }


            user.IsDelete = true;


            await _userRepository.UpdateAsync(user);

            return dto;
        }


        #endregion

        #region Group
        public async Task<Group> AddGroup(Group group)
        {
            return await _groupRepository.CreateAsync(group);
        }

        public async Task<IEnumerable<Group>> GetAllGroup(Expression<Func<Group, bool>> expression = null)
        {
            return await _groupRepository.GetAll(expression);
        }
        public async Task<IEnumerable<Group>> GetGroup(int id)
        {
            return await _groupRepository.GetAll(x => x.Id == id);
        }
        public async Task<Group> UpdateGroup(Group dto)
        {
            var res = await _groupRepository.GetAll(x => x.Id == dto.Id);
            Group group = res.FirstOrDefault();
            if (group != null)
            {
                group.Name = dto.Name;
                return await _groupRepository.UpdateAsync(group);
            }
            return group;

        }

        public async Task DeleteGroup(Group group)
        {
            await _groupRepository.DeleteAsync(group);
        }
        #endregion
    

        #region Contract
        public async Task<IEnumerable<Contract>> GetAllContract(Expression<Func<Contract, bool>> expression = null)
        {
            return await _contractRepository.GetAll(expression);
        }
        public async Task<IEnumerable<Contract>> GetContract(int id)
        {
            return await _contractRepository.GetAll(x => x.Id == id);
        }
        public async Task<Contract> AddContract(Contract contract)
        {
            return await _contractRepository.CreateAsync(contract);
        }

        public async Task<Contract> UpdateContract(Contract contract)
        {
            return await _contractRepository.UpdateAsync(contract);
        }
        public async Task DeleteContract(Contract contract)
        {
            await _contractRepository.DeleteAsync(contract);
        }

        #endregion

        #region Subject
        public async Task<IEnumerable<Subject>> GetAllSubject(Expression<Func<Subject, bool>> expression = null)
        {
            return await _subjectRepository.GetAll(expression);
        }
        public async Task<IEnumerable<Subject>> GetSubject(int id)
        {
            return await _subjectRepository.GetAll(x => x.Id == id);
        }
        public async Task<Subject> UpdateSubject(Subject sub)
        {
            var res = await _subjectRepository.GetAll(x => x.Id == sub.Id);
            Subject subject = res.FirstOrDefault();
            if (subject != null)
            {
                subject.Name = sub.Name;
                return await _subjectRepository.UpdateAsync(subject);
            }
            return subject;
        }

        public async Task<Subject> AddSubject(Subject subject)
        {
            return await _subjectRepository.CreateAsync(subject);
        }
        public async Task DeleteSubject(Subject subject)
        {
            await _subjectRepository.DeleteAsync(subject);
        }
        #endregion

        #region Student
        public async Task<IEnumerable<Student>> GetAllStudent(Expression<Func<Student, bool>> expression)
        {
            var con = (await GetAllContract())?.ToList() ?? new List<Contract>();
            var group = (await GetAllGroup())?.ToList() ?? new List<Group>();

            return await _studentRepository.GetAll(expression);

        }


        public async Task UpdateStudent(UStudentDTO dto)
        {
            try
            {
                var res = await _studentRepository.GetAsync(x => x.ID == dto.ID);
                if (res != null)
                {
                    res.Name = dto.Name;
                    res.GroupId = dto.GroupId;
                    res.ContractId = dto.ContractId;

                    await _studentRepository.UpdateAsync(res);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                throw new InvalidOperationException("Ошибка при обновлении студента", ex);
            }

        }
        public async Task<Student> CreateStudent(StudentDTO dto)
        {
            Student student = new Student();
            student.Name = dto.Name;
            student.GroupId = dto.GroupId;
            student.ContractId = dto.ContractId;
            return await _studentRepository.CreateAsync(student);
        }

        public async Task DeleteStudent(int id)
        {
            var student = await _studentRepository.GetAll(x => x.ID == id);
            if (student != null)
                await _studentRepository.DeleteAsync(student.FirstOrDefault());
        }
        public async Task<IEnumerable<Student>> GetStudent(int id)
        {
            return await _studentRepository.GetAll(x => x.ID == id);
        }
        #endregion
     

        #region WorkTable
        public async Task<WorkTable> AddWorkTable(WorkTable workTable)
        {
            return await _workTableRepository.CreateAsync(workTable);
        }

        public async Task<IEnumerable<WorkTable>> GetAllWorkTable(Expression<Func<WorkTable, bool>> expression = null)
        {
            return await _workTableRepository.GetAll(expression);
        }

        public async Task UpdateWorkTable(WorkTable workTable)
        {
            await _workTableRepository.UpdateAsync(workTable);
        }

        public async Task DeleteWorkTable(WorkTable workTable)
        {
            await _workTableRepository.DeleteAsync(workTable);
        }



        public Task CreateStudent(Student student)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}


   
