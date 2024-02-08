using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private readonly IMapper _mapper;

        public ProductAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Get()
        {
            try
            {
                var result = _db.Products.ToList();

                _responseDto.IsSuccess = true;
                _responseDto.Response = _mapper.Map<List<ProductDto>>(result);
                
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Response = e.Message;
            }

            return _responseDto;

        }

        [HttpPost]
        
        public ResponseDto Post([FromBody] ProductDto productDto )
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                _db.Products.Add(product);
                _db.SaveChanges();
                _responseDto.IsSuccess = true;
                _responseDto.Response = _mapper.Map<ProductDto>(product);
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Response = e.Message;
            }

            return _responseDto;

        }

        [HttpPut]
        public ResponseDto Put([FromBody] ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                _db.Products.Update(product);
                _db.SaveChanges();
                _responseDto.IsSuccess = true;
                _responseDto.Response = _mapper.Map<ProductDto>(product);
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
        public ResponseDto Put(int id)
        {
            try
            {
                var productToDelete = _db.Products.First(p => p.ProductId == id);
                _db.Products.Remove(productToDelete);
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
