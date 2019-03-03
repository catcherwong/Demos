namespace BasicEpplusDemo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Threading;
    using OfficeOpenXml;
    using System;
    using OfficeOpenXml.Style;
    using Microsoft.AspNetCore.Hosting;

    [Route("api/epplus")]
    [ApiController]
    public class EPPlusController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public EPPlusController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        // POST api/epplus/export
        [HttpGet("exportv2")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database
            await Task.Yield();
            var list = new List<UserInfo>()
            {
                new UserInfo { UserName = "catcher", Age = 18 },
                new UserInfo { UserName = "james", Age = 20 },
            };
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                // simple way
                workSheet.Cells.LoadFromCollection(list, true);

                //// mutual
                //workSheet.Row(1).Height = 20;
                //workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //workSheet.Row(1).Style.Font.Bold = true;
                //workSheet.Cells[1, 1].Value = "No";
                //workSheet.Cells[1, 2].Value = "Name";
                //workSheet.Cells[1, 3].Value = "Age";

                //int recordIndex = 2;
                //foreach (var item in list)
                //{
                //    workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                //    workSheet.Cells[recordIndex, 2].Value = item.UserName;
                //    workSheet.Cells[recordIndex, 3].Value = item.Age;
                //    recordIndex++;
                //}

                package.Save();
            }
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            return File(stream, "application/octet-stream", excelName);
            //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }


        // POST api/epplus/import
        [HttpPost("import")]
        public async Task<DemoResponse<List<UserInfo>>> Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return DemoResponse<List<UserInfo>>.GetResult(-1, "formfile is empty");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return DemoResponse<List<UserInfo>>.GetResult(-1, "Not Support file extension");
            }

            var list = new List<UserInfo>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        list.Add(new UserInfo
                        {
                            UserName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            Age = int.Parse(worksheet.Cells[row, 2].Value.ToString().Trim()),
                        });
                    }
                }
            }

            // add list to db ..
            // here just read and return

            return DemoResponse<List<UserInfo>>.GetResult(0, "OK", list);
        }

        // POST api/epplus/export
        [HttpGet("export")]
        public async Task<DemoResponse<string>> Export(CancellationToken cancellationToken)
        {
            string folder = _hostingEnvironment.WebRootPath;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            string downloadUrl = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, excelName);
            FileInfo file = new FileInfo(Path.Combine(folder, excelName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(folder, excelName));
            }

            // query data from database
            await Task.Yield();

            var list = new List<UserInfo>()
            {
                new UserInfo { UserName = "catcher", Age = 18 },
                new UserInfo { UserName = "james", Age = 20 },
            };

            using (var package = new ExcelPackage(file))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                workSheet.Cells.LoadFromCollection(list, true);

                package.Save();
            }

            return DemoResponse<string>.GetResult(0, "OK", downloadUrl);
        }
    }
}
