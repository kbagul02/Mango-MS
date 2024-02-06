using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ResponseDto _responseDto;
        private readonly IMapper _mapper;


        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        // GET: api/CouponAPI
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                var result = _db.Coupons.ToList();

                _responseDto.IsSuccess = true;
                _responseDto.Response = _mapper.Map<List<CouponDto>>(result);
                ;
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Response = e.Message;
            }

            return _responseDto;

        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                var result = _db.Coupons.First(c => c.CouponId == id);
                _responseDto.IsSuccess = true;
                _responseDto.Response = _mapper.Map<CouponDto>(result);
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Response = e.Message;
            }

            return _responseDto;
        }

        //Add Get method to get coupon by code
        [HttpGet]
        [Route("code/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                var result = _db.Coupons.First(c => c.CouponCode == code);
                _responseDto.IsSuccess = true;
                _responseDto.Response = _mapper.Map<CouponDto>(result);
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Response = e.Message;
            }

            return _responseDto;
        }

        //add new HTTPPOST method to add new coupon
        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(coupon);
                _db.SaveChanges();
                _responseDto.IsSuccess = true;
                _responseDto.Response = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Response = e.Message;
            }

            return _responseDto;

        }

        //Add HTTP PUT method to update coupon
        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(coupon);
                _db.SaveChanges();
                _responseDto.IsSuccess = true;
                
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Response = e.Message;
            }

            return _responseDto;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var coupon = _db.Coupons.First(c => c.CouponId == id);
                _db.Coupons.Remove(coupon);
                _db.SaveChanges();
                _responseDto.IsSuccess = true;
                
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Response = e.Message;
            }

            return _responseDto;
        }
    }
}
