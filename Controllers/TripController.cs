using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Trip;
using GestorViajes.Services.Trips;
using Microsoft.AspNetCore.Mvc;
using GestorViajes.Services.Users;
using GestorViajes.Services.Vehicles;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace GestorViajes.Controllers
{
	[Authorize]
	public class TripController : Controller
	{
		private readonly ITripService _tripService;
		private readonly IVehicleService _vehicleService;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public TripController(ITripService tripService,
			IMapper mapper,
			IVehicleService vehicleService,
			IUserService userService
			)
		{
			_tripService = tripService;
			_mapper = mapper;
			_vehicleService = vehicleService;
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(bool myTrips = false, int? status = null)
		{
			var current = await _userService.CurrentUser();
			// Pattern Matching
			Expression<Func<Trip, bool>> filter = (myTrips, status) switch
			{
				(true, null) => t => t.DriverId == current!.Id || t.Passengers.Any(x => x.UserId == current.Id),
				(false, null) => t => true,
				(false, _) => t => t.Status == (TripStatus)status.Value,
				(true, _) => t => (t.DriverId == current!.Id || t.Passengers.Any(x => x.UserId == current.Id)) && t.Status == (TripStatus)status.Value
			};

			var response = await _tripService.List(filter);

			if (!response.Success)
			{
				TempData["status"] = "error";
				TempData["message"] = response.Error!.Message;
				return View(new List<TripViewModel>());
			}

			return View(response.Data);
		}

		[HttpGet]
		public async Task<IActionResult> Details(long id)
		{
			var response = await _tripService.GetById(id);
			if (!response.Success)
			{
				TempData["status"] = "error";
				TempData["message"] = response.Error!.Message;
				return RedirectToAction(nameof(Index));
			}

			return View(response.Data);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
		    var vehiclesResponse = await _vehicleService.DropdownByUser();
			if(!vehiclesResponse.Success)
			{
				TempData["status"] = "error";
				TempData["message"] = vehiclesResponse.Error!.Message;
				return RedirectToAction(nameof(Index));
			}
			var model = new TripViewModel()
			{
				Vehicles = vehiclesResponse.Data!
			};
			return View(model);
		}

		//EN un pasado creo que use ListDropdownByUser para obtener el conductor pero visto que ahora tenemos HTTP context accesor no hace falta

		[HttpPost]
		public async Task<IActionResult> Create(TripViewModel model)
		{
		    var response = await _tripService.Add(model);
		    if (!response.Success)
		    {
		        TempData["status"] = "error";
		        TempData["message"] = response.Error?.Message;
				return RedirectToAction(nameof(Index), new { myTrips = true });
			}
		    TempData["status"] = "success";
		    TempData["message"] = "Viaje creado correctamente.";
		    return RedirectToAction(nameof(Index), new { myTrips = true});
		}



		[HttpGet]
		public async Task<IActionResult> Edit(long id)
		{
			var response = await _tripService.GetById(id);
			if (!response.Success)
			{
				TempData["status"] = "error";
				TempData["message"] = response.Error!.Message;
				return RedirectToAction(nameof(Index), new { myTrips = true });
			}

			if(response.Data!.Status != (int)TripStatus.Available)
			{
				TempData["status"] = "error";
				TempData["message"] = "No se puede editar un viaje que ya ha sido solicitado o aceptado.";
				return RedirectToAction(nameof(Index), new { myTrips = true });
			}

			return View(response.Data);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(TripViewModel model)
		{
			var response = await _tripService.Update(model);
			if (!response.Success)
			{
				TempData["status"] = "error";
				TempData["message"] = response.Error!.Message;
				return RedirectToAction("Edit", new { id = model.Id });
			}

			TempData["status"] = "success";
			TempData["message"] = "Viaje actualizado correctamente.";
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Delete(long id)
		{
			var response = await _tripService.Delete(id);
			if (!response.Success)
			{
				TempData["status"] = "error";
				TempData["message"] = response.Error!.Message;
				return RedirectToAction(nameof(Index), new { myTrips = true });
			}

			TempData["status"] = "success";
			TempData["message"] = "Viaje eliminado correctamente.";
			return RedirectToAction(nameof(Index), new { myTrips = true });
		}

		[HttpGet]
		public async Task<IActionResult> JoinTrip(long id)
		{
			var response = await _tripService.JoinTrip(id);
			if (!response.Success)
			{
				TempData["status"] = "error";
				TempData["message"] = response.Error!.Message;
				return RedirectToAction(nameof(Index), new { myTrips = true });
			}
			TempData["status"] = "success";
			TempData["message"] = "Se ha unido al viaje.";
			return RedirectToAction(nameof(Index), new { myTrips = true });
		}

		[HttpGet]
		public async Task<IActionResult> ExitTrip(long id)
		{
			var current = await _userService.CurrentUser();
			var response = await _tripService.ExitTrip(id, current!.Id);
			if (!response.Success)
			{
				TempData["status"] = "error";
				TempData["message"] = response.Error!.Message;
				return RedirectToAction(nameof(Index), new { myTrips = true });
			}
			TempData["status"] = "success";
			TempData["message"] = "Se ha salido del viaje.";
			return RedirectToAction(nameof(Index), new { myTrips = true });
		}


		[HttpGet]
		public async Task<IActionResult> Finalizar(long id)
		{
			var response = await _tripService.EndTrip(id);
			if (!response.Success)
			{
				TempData["status"] = "error";
				TempData["message"] = response.Error!.Message;
				return RedirectToAction(nameof(Index), new { myTrips = true });
			}
			TempData["status"] = "success";
			TempData["message"] = "Viaje finalizado correctamente.";
			return RedirectToAction(nameof(Index), new { myTrips = true });
		}

		[HttpGet]
		public async Task<IActionResult> Cancelar(long id)
		{
			var responseDelete = await _tripService.CancelTrip(id);
			if (!responseDelete.Success)
			{
				TempData["status"] = "error";
				TempData["message"] = responseDelete.Error!.Message;
				return RedirectToAction(nameof(Index), new { myTrips = true });
			}
			TempData["status"] = "success";
			TempData["message"] = "Viaje cancelado correctamente.";
			return RedirectToAction(nameof(Index), new { myTrips = true });
		}
	}
}