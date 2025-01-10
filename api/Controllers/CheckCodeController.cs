using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;

namespace api.Controllers
{

    [ApiController]
    [Route("api/uploadrar")]
    public class CheckCodeController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadAndExtract([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Define the target upload and extraction path
            var targetPath = @"C:\Users\ASUS\Desktop\UserSourceCode";

            // Ensure the target directory exists
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            var uploadedFilePath = Path.Combine(targetPath, file.FileName);

            // Save the uploaded file to the target directory
            using (var stream = new FileStream(uploadedFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Define the extraction directory
            var extractPath = Path.Combine(targetPath, Path.GetFileNameWithoutExtension(file.FileName));

            if (!Directory.Exists(extractPath))
            {
                Directory.CreateDirectory(extractPath);
            }

            try
            {
                // Extract the .rar file using SharpCompress
                using (var archive = RarArchive.Open(uploadedFilePath))
                {
                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                    {
                        entry.WriteToDirectory(extractPath, new ExtractionOptions
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }

                // After extraction, analyze the code with SonarQube
                var analysisResult = await AnalyzeCodeWithSonarQube(extractPath);

                return Ok(new
                {
                    Message = "File uploaded and extracted successfully.",
                    ExtractPath = extractPath,
                    SonarQubeAnalysisResult = analysisResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error extracting file: {ex.Message}");
            }
            finally
            {
                // Optionally delete the uploaded .rar file after extraction
                if (System.IO.File.Exists(uploadedFilePath))
                {
                    System.IO.File.Delete(uploadedFilePath);
                }
            }
        }

        private async Task<string> AnalyzeCodeWithSonarQube(string projectPath)
        {
            // This method will execute the SonarScanner for .NET on the extracted code.
            try
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "dotnet-sonarscanner",
                    Arguments = $"begin /k:\"your_project_key\" /d:sonar.login=\"your_sonarqube_token\" /d:sonar.projectBaseDir={projectPath}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = System.Diagnostics.Process.Start(startInfo))
                {
                    using (var reader = process.StandardOutput)
                    {
                        string output = await reader.ReadToEndAsync();
                        Console.WriteLine(output);  // Log the output from SonarScanner
                    }

                    process.WaitForExit();
                }

                // Run the final step to push the results to SonarQube
                startInfo.Arguments = "end /d:sonar.login=\"your_sonarqube_token\"";
                using (var process = System.Diagnostics.Process.Start(startInfo))
                {
                    using (var reader = process.StandardOutput)
                    {
                        string output = await reader.ReadToEndAsync();
                        Console.WriteLine(output);  // Log the output from SonarScanner
                    }

                    process.WaitForExit();
                }

                return "SonarQube analysis completed successfully.";
            }
            catch (Exception ex)
            {
                return $"Error running SonarQube analysis: {ex.Message}";
            }
        }
    }
}