using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using IImporter;

namespace BackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReflectionController : ControllerBase
    {
        private const string ReflectionDirectory = "reflection";

        [HttpGet("importers")]
        public IActionResult GetImporters()
        {
            try
            {
                var projectDirectory = Directory.GetCurrentDirectory();
                
                var directoryPath = Path.Combine(projectDirectory, ReflectionDirectory);

                Console.WriteLine($"Buscando en directorio: {directoryPath}");
                
                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine($"El directorio no existe: {directoryPath}");
                    return Ok(Array.Empty<string>());
                }
                
                var dllFiles = Directory.GetFiles(directoryPath, "*.dll");
                Console.WriteLine($"Encontradas {dllFiles.Length} DLLs en el directorio");

                var validDlls = new List<string>();

                foreach (var dllPath in dllFiles)
                {
                    try
                    {
                        Console.WriteLine($"Procesando: {Path.GetFileName(dllPath)}");
                        
                        var assembly = Assembly.LoadFrom(dllPath);
                        
                        var hasImporter = assembly
                            .GetTypes()
                            .Any(t => t.IsClass && 
                                      t.IsPublic && 
                                      !t.IsAbstract && 
                                      typeof(ImporterInterface).IsAssignableFrom(t));

                        if (hasImporter)
                        {
                            Console.WriteLine($"✓ DLL válida: {Path.GetFileName(dllPath)}");
                            validDlls.Add(Path.GetFileName(dllPath));
                        }
                        else
                        {
                            Console.WriteLine($"✗ DLL no contiene implementaciones válidas: {Path.GetFileName(dllPath)}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"✗ No se pudo cargar el ensamblado {Path.GetFileName(dllPath)}: {ex.Message}");
                    }
                }

                Console.WriteLine($"Total de DLLs válidas encontradas: {validDlls.Count}");
                return Ok(validDlls.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al escanear importers: {ex.Message}");
                return Ok(Array.Empty<string>());
            }
        }
    }
}