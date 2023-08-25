using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    [Route("[controller]")]
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;

        }

        [HttpGet]
        [Route("coupon-index")]
        public async Task<IActionResult> CouponIndex()
        {

            List<CouponDto>? couponList = new();

            ResponseDto? response = await _couponService.GetAllCouponAsync();

            if (response?.Result != null && response.IsSuccess)
            {
                couponList = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }

            return View(couponList);
        }

        [HttpGet]
        [Route("coupon-create")]
        public IActionResult CouponCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("coupon-create")]
        public async Task<IActionResult> CouponCreate(CouponDto couponDto)
        {
            ResponseDto? response = await _couponService.CreateCouponAsync(couponDto);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction("CouponIndex");
            }

            return View(couponDto);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);

            if (response?.Result != null && response.IsSuccess)
            {
                CouponDto? couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response?.Result));
                return View(couponDto);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction("CouponIndex");
            }
            return View(couponDto);
        }

    }
}