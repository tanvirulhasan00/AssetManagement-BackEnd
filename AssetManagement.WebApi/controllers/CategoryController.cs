using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AssetManagement.Models.db;
using AssetManagement.Models.Request.Dto;
using AssetManagement.Models.Request.Generic;
using AssetManagement.Models.Response.Api;
using AssetManagement.Repositories.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.WebApi.controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/category")]
    [ApiVersion("1.0")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> GetAllCategory(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var genericReq = new GenericRequest<Category>
            {
                Expression = null,
                IncludeProperties = null,
                NoTracking = true,
                CancellationToken = cancellationToken
            };
            try
            {
                var category = await _unitOfWork.Categories.GetAllAsync(genericReq);
                if (category == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Sucessful";
                response.Result = category;
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> GetCategory(int Id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var genericReq = new GenericRequest<Category>
            {
                Expression = x => x.Id == Id,
                IncludeProperties = null,
                NoTracking = true,
                CancellationToken = cancellationToken
            };
            try
            {
                var category = await _unitOfWork.Categories.GetAsync(genericReq);
                if (category == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Sucessful";
                response.Result = category;
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                response.Error = ex.GetType().Name;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Error = ex.GetType().Name;
                return response;
            }
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> CreateCategory([FromBody] CategoryCreateDto categoryDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (categoryDto.Name == null || categoryDto.Name == "")
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - name is empty";
                return response;
            }

            try
            {
                var genericReq = new GenericRequest<Category>
                {
                    Expression = x => x.Name == categoryDto.Name,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var duplicateCheck = await _unitOfWork.Categories.GetAllAsync(genericReq);
                if (duplicateCheck.Count > 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.Conflict;
                    response.Message = "Duplicate category name found";
                    return response;
                }
                Category categoryToCreate = new()
                {
                    Name = categoryDto.Name,
                    Price = categoryDto.Price,
                    Active = int.Parse(categoryDto.Active),
                };
                await _unitOfWork.Categories.AddAsync(categoryToCreate);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something wrong while creating category";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Message = "Create Successful";
                return response;

            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Error = ex;
                return response;

            }
        }

        [HttpPost]
        [Route("update")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> UpdateCategory([FromBody] CategoryUpdateDto categoryDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (categoryDto.Id == 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - id is not valid";
                return response;
            }
            try
            {
                var genericReq = new GenericRequest<Category>
                {
                    Expression = x => x.Id == categoryDto.Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var categoryData = await _unitOfWork.Categories.GetAsync(genericReq);
                if (categoryData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - category not found with the id {categoryDto.Id}";
                    return response;
                }
                categoryData.Name = (categoryDto.Name == null || categoryDto.Name == "") ? categoryData.Name : categoryDto.Name;
                categoryData.Price = (categoryDto.Price == 0) ? categoryData.Price : categoryDto.Price;
                categoryData.Active = int.Parse(categoryDto.Active);
                _unitOfWork.Categories.Update(categoryData);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something wrong while updating category";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Update Successful";
                return response;

            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Error = ex.GetType().Name;
                return response;

            }
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> DeleteCategory(List<string> Ids, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (Ids == null || Ids.Count == 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - No IDs provided";
                return response;
            }
            try
            {
                var genericReq = new GenericRequest<Category>
                {
                    Expression = x => Ids.Contains(x.Id.ToString()),
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var categoryToDelete = await _unitOfWork.Categories.GetAllAsync(genericReq);
                if (categoryToDelete == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - No areas found with the provided IDs: {Ids}";
                    return response;
                }

                _unitOfWork.Categories.RemoveRange(categoryToDelete);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while deleting category";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{categoryToDelete.Count} Category(s) deleted Successfully";
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Error = ex.GetType().Name;
                return response;

            }
        }
    }
}