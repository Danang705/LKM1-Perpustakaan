using Microsoft.AspNetCore.Mvc;
using LKM1_Perpustakaan.Models;

namespace LKM1_Perpustakaan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BukuController : ControllerBase
    {
        private string __constr;

        public BukuController(IConfiguration configuration)
        {
            // Mengambil connection string dari appsettings.json
            __constr = configuration.GetConnectionString("DefaultConnection");
        }

        // 1. READ ALL (GET: api/Buku)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                BukuContext context = new BukuContext(this.__constr);
                List<Buku> listBuku = context.GetAllBuku();

                // Format Response Sukses (Status Code 200)
                return Ok(new { status = "success", data = listBuku });
            }
            catch (Exception ex)
            {
                // Format Response Error (Status Code 500)
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        // 2. READ BY ID (GET: api/Buku/{id})
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                BukuContext context = new BukuContext(this.__constr);
                Buku? buku = context.GetBukuById(id);

                if (buku == null)
                {
                    // Status Code 404 jika data tidak ada
                    return NotFound(new { status = "error", message = $"Data buku dengan ID {id} tidak ditemukan" });
                }

                return Ok(new { status = "success", data = buku });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        // 3. CREATE (POST: api/Buku)
        [HttpPost]
        public IActionResult Post([FromBody] Buku buku)
        {
            try
            {
                BukuContext context = new BukuContext(this.__constr);
                context.AddBuku(buku);

                // Status Code 201 (Created) untuk data yang baru dibuat
                return StatusCode(201, new { status = "success", message = "Data buku berhasil ditambahkan" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        // 4. UPDATE (PUT: api/Buku/{id})
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Buku buku)
        {
            try
            {
                BukuContext context = new BukuContext(this.__constr);

                // Cek apakah buku ada sebelum diupdate
                Buku? existingBuku = context.GetBukuById(id);
                if (existingBuku == null)
                {
                    return NotFound(new { status = "error", message = $"Data buku dengan ID {id} tidak ditemukan" });
                }

                context.UpdateBuku(id, buku);
                return Ok(new { status = "success", message = "Data buku berhasil diperbarui" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        // 5. DELETE (DELETE: api/Buku/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                BukuContext context = new BukuContext(this.__constr);

                // Cek apakah buku ada sebelum dihapus
                Buku? existingBuku = context.GetBukuById(id);
                if (existingBuku == null)
                {
                    return NotFound(new { status = "error", message = $"Data buku dengan ID {id} tidak ditemukan" });
                }

                context.DeleteBuku(id); // Ini akan memicu Soft Delete
                return Ok(new { status = "success", message = "Data buku berhasil dihapus (Soft Delete)" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }
    }
}