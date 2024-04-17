using Microsoft.AspNetCore.Mvc;
using StudentRegistrationForm.Models;
using StudentRegistrationForm.Util;
using System.Data;

namespace StudentRegistrationForm.BussinessLayer
{
    public class StudentBL : IStudentBL
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StudentBL> _logger;
        private readonly IWebHostEnvironment _environment;
        public StudentBL(IConfiguration configuration, ILogger<StudentBL> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _configuration = configuration;
            _environment = environment;
        }


        public async Task<StateCityresponse> GetStateandCity()
        {
            StateCityresponse response = new StateCityresponse();
            try
            {
                string? ConStg = _configuration.GetConnectionString("StudentDbCon");
                string ProcedureName = "GetStateAndCity";
                string Parameters = @"";
                _ = new ClDataSetClass(out DataSet ds, ProcedureName, Parameters, ConStg);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.code = 200;
                    response.Data = LowercaseJsonSerializer.SerializeObject(ds);
                }
                else
                {
                    response.code = 404;
                    response.Data = "";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.code = 500;
                response.Data = "";
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
                return response;
            }

        }

         
        public async Task<StudentInsertResponse> InsertStudentData([FromBody] StudentInsertRequet requestData)
        {
            StudentInsertResponse response = new StudentInsertResponse();
            try
            {


                // Remove "data:image/png;base64," or "data:image/jpeg;base64," from base64 string
                string base64Image = requestData.inputGroupFile01.Replace("data:image/png;base64,", "")
                                         .Replace("data:image/jpeg;base64,", "");

                // Remove "data:image/png;base64," or "data:image/jpeg;base64," from base64 string
                base64Image = base64Image.Replace("data:image/png;base64,", "")
                                         .Replace("data:image/jpeg;base64,", "");

                // Convert base64 string to byte array
                byte[] imageBytes = Convert.FromBase64String(base64Image);

                // Specify the folder where you want to save the image
                string folderPath = Path.Combine(_environment.WebRootPath, "Uploadedimages");

                // Check if the folder exists, if not, create it
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Generate unique file name (you can implement your own logic here)
                string fileName = $"{Guid.NewGuid()}.png";

                // Specify the full file path
                string filePath = Path.Combine(folderPath, fileName);

                // Save byte array as image file
                System.IO.File.WriteAllBytes(filePath, imageBytes);




                string? ConStg = _configuration.GetConnectionString("StudentDbCon");
                string ProcedureName = "[InsertStudentData]";
                string Parameters = @"@Name='"+requestData.Name+"',@Email='"+requestData.Email+"',@Mobile='"+requestData.Mobile+"'" +
                    ",@State='"+requestData.State+"',@City='"+requestData.City+"',@AboutYourSelf='"+requestData.AboutYourself+"'" +
                    ",@UploadPhotoUrl='"+ fileName + "',@mode='register'";
                _ = new ClDataSetClass(out DataSet ds, ProcedureName, Parameters, ConStg);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.code = 200;
                    response.Data = LowercaseJsonSerializer.SerializeObject(ds);
                }
                else
                {
                    response.code = 404;
                    response.Data = "";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.code = 500;
                response.Data = "";
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
                return response;
            }

        }

        public async Task<StateCityresponse> GetStudentData()
        {
            StateCityresponse response = new StateCityresponse();
            try
            {
                string? ConStg = _configuration.GetConnectionString("StudentDbCon");
                string ProcedureName = "InsertStudentData";
                string Parameters = @"@mode='viewstudent'";
                _ = new ClDataSetClass(out DataSet ds, ProcedureName, Parameters, ConStg);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.code = 200;
                    response.Data = LowercaseJsonSerializer.SerializeObject(ds);
                }
                else
                {
                    response.code = 404;
                    response.Data = "";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.code = 500;
                response.Data = "";
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
                return response;
            }

        }

        public async Task<StateCityresponse> GetStudentDatabyId([FromBody] StudentRequest requestData)
        {
            StateCityresponse response = new StateCityresponse();
            try
            {
                string? ConStg = _configuration.GetConnectionString("StudentDbCon");
                string ProcedureName = "InsertStudentData";
                string Parameters = @"@mode='viewstudentbyid',@id='"+ requestData.StudentId + "'";
                _ = new ClDataSetClass(out DataSet ds, ProcedureName, Parameters, ConStg);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.code = 200;
                    response.Data = LowercaseJsonSerializer.SerializeObject(ds);
                }
                else
                {
                    response.code = 404;
                    response.Data = "";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.code = 500;
                response.Data = "";
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
                return response;
            }

        }

        public async Task<StudentInsertResponse> UpdateStudentData([FromBody] UpdateRequet requestData)
        {
            StudentInsertResponse response = new StudentInsertResponse();
            try
            {// Remove "data:image/png;base64," or "data:image/jpeg;base64," from base64 string
                string base64Image = requestData.inputGroupFile01.Replace("data:image/png;base64,", "")
                                         .Replace("data:image/jpeg;base64,", "");

                // Remove "data:image/png;base64," or "data:image/jpeg;base64," from base64 string
                base64Image = base64Image.Replace("data:image/png;base64,", "")
                                         .Replace("data:image/jpeg;base64,", "");

                // Convert base64 string to byte array
                byte[] imageBytes = Convert.FromBase64String(base64Image);

                // Specify the folder where you want to save the image
                string folderPath = Path.Combine(_environment.WebRootPath, "Uploadedimages");

                // Check if the folder exists, if not, create it
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Generate unique file name (you can implement your own logic here)
                string fileName = $"{Guid.NewGuid()}.png";

                // Specify the full file path
                string filePath = Path.Combine(folderPath, fileName);

                // Save byte array as image file
                System.IO.File.WriteAllBytes(filePath, imageBytes);


                string? ConStg = _configuration.GetConnectionString("StudentDbCon");
                string ProcedureName = "[InsertStudentData]";
                string Parameters = @"@Name='" + requestData.Name + "',@Email='" + requestData.Email + "',@Mobile='" + requestData.Mobile + "'" +
                    ",@State='" + requestData.State + "',@City='" + requestData.City + "',@AboutYourSelf='" + requestData.AboutYourself + "'" +
                    ",@UploadPhotoUrl='" + fileName + "',@mode='update',@id='"+requestData.StudentId+"'";
                _ = new ClDataSetClass(out DataSet ds, ProcedureName, Parameters, ConStg);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.code = 200;
                    response.Data = LowercaseJsonSerializer.SerializeObject(ds);
                }
                else
                {
                    response.code = 404;
                    response.Data = "";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.code = 500;
                response.Data = "";
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
                return response;
            }

        }

        public async Task<StateCityresponse> Deletestudentdata([FromBody] StudentRequest requestData)
        {
            StateCityresponse response = new StateCityresponse();
            try
            {
                string? ConStg = _configuration.GetConnectionString("StudentDbCon");
                string ProcedureName = "InsertStudentData";
                string Parameters = @"@mode='Delete',@id='" + requestData.StudentId + "'";
                _ = new ClDataSetClass(out DataSet ds, ProcedureName, Parameters, ConStg);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.code = 200;
                    response.Data = LowercaseJsonSerializer.SerializeObject(ds);
                }
                else
                {
                    response.code = 404;
                    response.Data = "";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.code = 500;
                response.Data = "";
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
                return response;
            }

        }

    }
}
