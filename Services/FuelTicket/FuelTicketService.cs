//using System.Linq.Expressions;
//using AutoMapper;
//using GestorViajes.Models;
//using GestorViajes.Models.ViewModels.Image;
//using GestorViajes.Repositories.FuelTickets;
//using GestorViajes.Repositories.Users;
//using GestorViajes.Services.Image;
//using GestorViajes.Services.User;
//using GestorViajes.Services.Users;
//using GestorViajes.ViewModels;

//namespace GestorViajes.Services.FuelTicket
//{
//    public class FuelTicketService : IFuelTicketService
//    {
//        private readonly IFuelTicketRepository _fuelTicketRepository;
//        private readonly IUserService _userService;
//        private readonly IMapper _mapper;
//        private readonly IImageProcessor _imageProcessor;
//        public FuelTicketService(
//            IFuelTicketRepository fuelTicketRepository,
//            IUserService userService,
//            IMapper mapper,
//            IImageProcessor imageProcessor)
//        {
//            _fuelTicketRepository = fuelTicketRepository;
//            _userService = userService;
//            _mapper = mapper;
//            _imageProcessor = imageProcessor;
//        }

//        public async Task<GenericResponse<List<FuelTicketViewModel>>> List(Expression<Func<Models.EFCore.Rove.FuelTicket, bool>>? predicate = null)
//        {
//            var response = new GenericResponse<List<FuelTicketViewModel>>();
//            try
//            {
//                var result = await _fuelTicketRepository.List(predicate);
//                if (!result.Success)
//                {
//                    response.Error = result.Error;
//                    return response;
//                }

//                response.Data = _mapper.Map<List<FuelTicketViewModel>>(result.Data);
//            }
//            catch (Exception ex)
//            {
//                response.Error = new ErrorResponse(ex);
//            }
//            return response;
//        }

//        public async Task<GenericResponse<FuelTicketViewModel>> Get(long id, bool? include = false)
//        {
//            var response = new GenericResponse<FuelTicketViewModel>();
//            try
//            {
//                var current = await _userService.CurrentUser();
//                var result = await _fuelTicketRepository.Get(id, include);
//                if (!result.Success)
//                {
//                    response.Error = result.Error;
//                    return response;
//                }
//                if (result.Data.UserId != current.Id)
//                {
//                    response.Error = new ErrorResponse("No tienes permiso para ver este ticket.");
//                }

//                response.Data = _mapper.Map<FuelTicketViewModel>(result.Data);
//            }
//            catch (Exception ex)
//            {
//                response.Error = new ErrorResponse(ex);
//            }
//            return response;
//        }

//        public async Task<GenericResponse<FuelTicketViewModel>> Add(FuelTicketViewModel model)
//        {
//            var response = new GenericResponse<FuelTicketViewModel>();
//            try
//            {
//                var current = await _userService.CurrentUser();

//                var entity = _mapper.Map<Models.EFCore.Rove.FuelTicket>(model);
//                entity.UserId = current!.Id;
//                entity.CreatedBy = current.Email;
//                entity.LastUpdatedBy = current.Email;
//                entity.UploadedAt = DateTime.UtcNow;

//                var result = await _fuelTicketRepository.Add(entity);
//                if (!result.Success)
//                {
//                    response.Error = result.Error;
//                    return response;
//                }


//                var input = new ImageResizeInput()
//                {
//                    MaxSize = 1000,
//                    MaxHeight = 1000,
//                    MaxWidth = 1000
//                };

//                using (var memoryStream = new MemoryStream())
//                {
//                    await model.ImageFile.CopyToAsync(memoryStream);
//                    var fileBytes = memoryStream.ToArray();
//                    input.File = Convert.ToBase64String(fileBytes);
//                }

//                var imageResponse = await _imageProcessor.Resize(input);
//                if (!imageResponse.Success)
//                    return new GenericResponse<FuelTicketViewModel>() { Error = new ErrorResponse("Error en el tratamiento de la imagen") };

//                var uploadResponse = await _fuelTicketRepository.UploadImage(result.Data!.Id, imageResponse.Data!.File);
//                if (!uploadResponse.Success)
//                    return new GenericResponse<FuelTicketViewModel>() { Error = new ErrorResponse("Error al subir la imagen") };


//                response.Data = _mapper.Map<FuelTicketViewModel>(result.Data);
//            }
//            catch (Exception ex)
//            {
//                response.Error = new ErrorResponse(ex);
//            }
//            return response;
//        }

//        public async Task<GenericResponse<FuelTicketViewModel>> Update(FuelTicketViewModel model)
//        {
//            var response = new GenericResponse<FuelTicketViewModel>();
//            try
//            {
//                var current = await _userService.CurrentUser();
//                var resultExisting = await _fuelTicketRepository.Get(model.Id);
//                if (!resultExisting.Success || resultExisting.Data == null)
//                {
//                    response.Error = new ErrorResponse("El ticket no existe.");
//                    return response;
//                }

//                if (resultExisting.Data.UserId != current.Id)
//                {
//                    response.Error = new ErrorResponse("No tienes permiso para editar este ticket.");
//                }


//                var existingTicket = resultExisting.Data;
//                var updatedEntity = _mapper.Map<Models.EFCore.Rove.FuelTicket>(model);
//                updatedEntity.UserId = existingTicket.UserId; // Preservar UserId original
//                updatedEntity.CreatedAt = existingTicket.CreatedAt;
//                updatedEntity.CreatedBy = existingTicket.CreatedBy;
//                updatedEntity.LastUpdatedBy = current.Email;
//                updatedEntity.UploadedAt = existingTicket.UploadedAt;

//                var result = await _fuelTicketRepository.Update(updatedEntity);
//                if (!result.Success)
//                {
//                    response.Error = result.Error;
//                    return response;
//                }

//                var input = new ImageResizeInput()
//                {
//                    MaxSize = 1000,
//                    MaxHeight = 1000,
//                    MaxWidth = 1000
//                };

//                using (var memoryStream = new MemoryStream())
//                {
//                    await model.ImageFile.CopyToAsync(memoryStream);
//                    var fileBytes = memoryStream.ToArray();
//                    input.File = Convert.ToBase64String(fileBytes);
//                }

//                var imageResponse = await _imageProcessor.Resize(input);
//                if (!imageResponse.Success)
//                    return new GenericResponse<FuelTicketViewModel>() { Error = new ErrorResponse("Error en el tratamiento de la imagen") };

//                var uploadResponse = await _fuelTicketRepository.UploadImage(result.Data!.Id, imageResponse.Data!.File);
//                if (!uploadResponse.Success)
//                    return new GenericResponse<FuelTicketViewModel>() { Error = new ErrorResponse("Error al subir la imagen") };



//                response.Data = _mapper.Map<FuelTicketViewModel>(result.Data);
//            }
//            catch (Exception ex)
//            {
//                response.Error = new ErrorResponse(ex);
//            }
//            return response;
//        }

//        public async Task<GenericResponse<bool>> Delete(long id)
//        {
//            var response = new GenericResponse<bool>();
//            try
//            {
//                var current = await _userService.CurrentUser();
//                var resultExisting = await _fuelTicketRepository.Get(id);
//                if (!resultExisting.Success || resultExisting.Data == null)
//                {
//                    response.Error = new ErrorResponse("El ticket no existe.");
//                    return response;
//                }
//                if (resultExisting.Data.UserId != current.Id)
//                {
//                    response.Error = new ErrorResponse("No tienes permiso para eliminar este ticket.");
//                    return response;
//                }

//                var result = await _fuelTicketRepository.Delete(id);
//                if (!result.Success)
//                {
//                    response.Error = result.Error;
//                    return response;
//                }

//                response.Data = result.Data;
//            }
//            catch (Exception ex)
//            {
//                response.Error = new ErrorResponse(ex);
//            }
//            return response;
//        }

//        public async Task<GenericResponse<bool>> Exists(Expression<Func<Models.EFCore.Rove.FuelTicket, bool>> predicate)
//        {
//            var response = new GenericResponse<bool>();
//            try
//            {
//                var result = await _fuelTicketRepository.Exists(predicate);
//                if (!result.Success)
//                {
//                    response.Error = result.Error;
//                    return response;
//                }

//                response.Data = result.Data;
//            }
//            catch (Exception ex)
//            {
//                response.Error = new ErrorResponse(ex);
//            }
//            return response;
//        }
//    }
//}
