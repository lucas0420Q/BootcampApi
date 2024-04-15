using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace PromotionController
{
    [Route("api/[controller]")]
    public class PromotionController : BaseApiController
    {
        // Aquí normalmente inyectarías el servicio que maneja la lógica de negocios
        // Pero por simplicidad, vamos a trabajar con datos en memoria

        private static List<Promotion> promociones = new List<Promotion>();

        // GET: api/Promociones
        [HttpGet]
        public IEnumerable<Promotion> Get()
        {
            return promociones;
        }

        // POST: api/Promociones
        [HttpPost]
        public void Post([FromBody] Promotion promocion)
        {
            promociones.Add(promocion);
        }
        private static List<Business> empresas = new List<Business>();
    }
}