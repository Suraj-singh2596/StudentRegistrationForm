using Microsoft.AspNetCore.Mvc;
using StudentRegistrationForm.Models;

namespace StudentRegistrationForm.BussinessLayer
{
    public interface IStudentBL
    {
        Task<StateCityresponse> GetStateandCity();

        Task<StudentInsertResponse> InsertStudentData([FromBody] StudentInsertRequet requestData);

        Task<StateCityresponse> GetStudentData();
        Task<StateCityresponse> GetStudentDatabyId([FromBody] StudentRequest requestData);

        Task<StudentInsertResponse> UpdateStudentData([FromBody] UpdateRequet requestData);
        Task<StateCityresponse> Deletestudentdata([FromBody] StudentRequest requestData);

    }
}
