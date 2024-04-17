using Microsoft.AspNetCore.Mvc;
using StudentRegistrationForm.BussinessLayer;
using StudentRegistrationForm.Models;
using StudentRegistrationForm.RepositoryLayer;

namespace StudentRegistrationForm.ServiceLayer
{
    public class StudentSL :IStudentRL
    {

        private readonly IStudentBL _StudentBL;

        public StudentSL(IStudentBL StudentBL)
        {
            _StudentBL = StudentBL;
        }

        public async Task<StateCityresponse> GetStateandCity()
        {
            return await _StudentBL.GetStateandCity();

        }


        public async Task<StudentInsertResponse> InsertStudentData([FromBody] StudentInsertRequet requestData)        
        {
            return await _StudentBL.InsertStudentData(requestData);

        }
        public async Task<StateCityresponse> GetStudentData()
        {
            return await _StudentBL.GetStudentData();

        }

        public async Task<StateCityresponse> GetStudentDatabyId([FromBody] StudentRequest requestData)
        {
            return await _StudentBL.GetStudentDatabyId(requestData);

        }

        public async Task<StudentInsertResponse> UpdateStudentData([FromBody] UpdateRequet requestData)
        {
            return await _StudentBL.UpdateStudentData(requestData);
        }

        public async Task<StateCityresponse> Deletestudentdata([FromBody] StudentRequest requestData)
        {
            return await _StudentBL.Deletestudentdata(requestData);

        }

    }
}
