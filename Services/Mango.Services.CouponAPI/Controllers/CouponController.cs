using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using AutoMapper;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private ResponseDto _response;

        public CouponController(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _response = new ResponseDto();
        }

        // GET: api/Coupon
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetCoupons()
        {
            try
            {
                if (_context.Coupons == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not Found";
                    return NotFound(_response);
                }

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }

            _response.Result = _mapper.Map<IEnumerable<CouponDto>>(await _context.Coupons.ToListAsync());

            return Ok(_response);

        }

        // GET: api/Coupon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetCoupon(int id)
        {
            try
            {
                if (_context.Coupons == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Table Not Found";
                    return NotFound(_response);
                }
                var coupon = _mapper.Map<CouponDto>(await _context.Coupons.FindAsync(id));

                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon Not Found";
                    return NotFound(_response);
                }
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("GetByCode/{couponCode}")]
        public async Task<ActionResult<ResponseDto>> GetCouponByCouponCode(string couponCode)
        {
            try
            {
                if (_context.Coupons == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Table Not Found";
                    return NotFound(_response);
                }
                var coupon = _mapper.Map<CouponDto>(await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode));

                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon Not Found";
                    return NotFound(_response);
                }
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        // PUT: api/Coupon/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> PutCoupon(int id, Coupon coupon)
        {
            if (id != coupon.CouponId)
            {
                _response.IsSuccess = false;
                _response.Message = "Id mismatch";
                return _response;
            }

            _context.Entry(coupon).State = EntityState.Modified;

            try
            {
                _response.Result = coupon;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CouponExists(id))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Id dont belong to any coupon";
                    return _response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = ex.Message;
                    return _response;
                }
            }

            return Ok(_response);
        }

        // POST: api/Coupon
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseDto>> PostCoupon(Coupon coupon)
        {
            try
            {
                if (_context.Coupons == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Table Not Found";
                    return NotFound(_response);
                }

                _context.Coupons.Add(coupon);
                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);

            }

            // return CreatedAtAction("GetCoupon", new { id = coupon.CouponId }, coupon);
            _response.Result = _mapper.Map<CouponDto>(coupon);
            return Ok(_response);
        }

        // DELETE: api/Coupon/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteCoupon(int id)
        {
            try
            {
                if (_context.Coupons == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Table Not Found";
                    return NotFound(_response);
                }

                var coupon = await _context.Coupons.FindAsync(id);

                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon Not Found";
                    return NotFound(_response);
                }

                _context.Coupons.Remove(coupon);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }

            _response.Message = $"Deleted Successfully coupont {id}";
            return _response;
        }

        private bool CouponExists(int id)
        {
            return (_context.Coupons?.Any(e => e.CouponId == id)).GetValueOrDefault();
        }
    }
}
