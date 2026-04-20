using Microsoft.AspNetCore.Mvc;
using LKM1_Perpustakaan.Models;
using Microsoft.AspNetCore.Authorization;

namespace LKM1_Perpustakaan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private string __constr; private IConfiguration __config;
        public AuthController(IConfiguration config) { __config = config; __constr = config.GetConnectionString("DefaultConnection")!; }
        [HttpPost("register")] public IActionResult Reg([FromBody] UserRegister u) { try { new AuthContext(__constr).Register(u); return StatusCode(201, new { status = "success", message = "Registrasi sukses" }); } catch (Exception ex) { return StatusCode(500, new { status = "error", message = ex.Message }); } }
        [HttpPost("login")] public IActionResult Log([FromBody] UserLogin u) { try { var token = new AuthContext(__constr).Login(u, __config); if (token == null) return Unauthorized(new { status = "error", message = "Login gagal" }); return Ok(new { status = "success", token = token }); } catch (Exception ex) { return StatusCode(500, new { status = "error", message = ex.Message }); } }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // <--- Gembok API Buku
    public class BukuController : ControllerBase
    {
        private string __constr; public BukuController(IConfiguration config) { __constr = config.GetConnectionString("DefaultConnection")!; }
        [HttpGet] public IActionResult Get() { try { return Ok(new { status = "success", data = new BukuContext(__constr).GetAll() }); } catch (Exception ex) { return StatusCode(500, new { status = "error", message = ex.Message }); } }
        [HttpGet("{id}")] public IActionResult Get(int id) { try { var buku = new BukuContext(__constr).GetById(id); if (buku == null) return NotFound(new { status = "error", message = "Buku tidak ditemukan" }); return Ok(new { status = "success", data = buku }); } catch (Exception ex) { return StatusCode(500, new { status = "error", message = ex.Message }); } }
        [HttpPost] public IActionResult Post([FromBody] Buku b) { try { new BukuContext(__constr).Add(b); return StatusCode(201, new { status = "success", message = "Buku ditambah" }); } catch (Exception ex) { return StatusCode(500, new { status = "error", message = ex.Message }); } }
        [HttpPut("{id}")] public IActionResult Put(int id, [FromBody] Buku b) { try { var ctx = new BukuContext(__constr); if (ctx.GetById(id) == null) return NotFound(new { status = "error", message = "Tidak ditemukan" }); ctx.Update(id, b); return Ok(new { status = "success", message = "Buku diupdate" }); } catch (Exception ex) { return StatusCode(500, new { status = "error", message = ex.Message }); } }
        [HttpDelete("{id}")] public IActionResult Delete(int id) { try { var ctx = new BukuContext(__constr); if (ctx.GetById(id) == null) return NotFound(new { status = "error", message = "Tidak ditemukan" }); ctx.Delete(id); return Ok(new { status = "success", message = "Buku dihapus (Soft Delete)" }); } catch (Exception ex) { return StatusCode(500, new { status = "error", message = ex.Message }); } }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // <--- Gembok API Peminjaman
    public class PeminjamanController : ControllerBase
    {
        private string __constr; public PeminjamanController(IConfiguration config) { __constr = config.GetConnectionString("DefaultConnection")!; }
        [HttpGet] public IActionResult Get() { try { return Ok(new { status = "success", data = new PeminjamanContext(__constr).GetAll() }); } catch (Exception ex) { return StatusCode(500, new { status = "error", message = ex.Message }); } }
        [HttpPost] public IActionResult Post([FromBody] Peminjaman p) { try { new PeminjamanContext(__constr).Add(p); return StatusCode(201, new { status = "success", message = "Peminjaman dicatat" }); } catch (Exception ex) { return StatusCode(500, new { status = "error", message = ex.Message }); } }
        [HttpPut("Kembalikan/{id}")] public IActionResult Put(int id) { try { new PeminjamanContext(__constr).Kembalikan(id); return Ok(new { status = "success", message = "Buku dikembalikan" }); } catch (Exception ex) { return StatusCode(500, new { status = "error", message = ex.Message }); } }
    }
}