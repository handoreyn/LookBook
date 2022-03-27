using Bakery.Email.Core.Dtos.ApiSubscriber;
using Bakery.Email.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Email.Api.Controllers;

[ApiController]
[Route("api/v1/subscriber")]
public class SubscriberController : ControllerBase
{
    private readonly IApiSubscriberRepository _subscriberRepository;

    public SubscriberController(IApiSubscriberRepository subscriberRepository)
    {
        _subscriberRepository = subscriberRepository;
    }

    public async Task<IActionResult> Create(CreateApiSubscriberDto model)
    {
        if (ModelState.IsValid) return BadRequest(new ResponseModel<CreateApiSubscriberDto>("Model is invalid."));

        var apiSubscriber = await _subscriberRepository.FindSubscriberByEmailAsync(model.Email);
        if (apiSubscriber == null) return BadRequest(new ResponseModel<CreateApiSubscriberDto>("Subscriber exist."));

        apiSubscriber = await _subscriberRepository.CreateApiSubscriberAsync(model);

        return CreatedAtAction("Details", apiSubscriber);
    }

    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest(new ResponseModel<DtoBase>("Id cannot be null"));
        var apiSubscriber = await _subscriberRepository.FindSubscriberByIdAsync(id);

        if (apiSubscriber == null) return NotFound(new ResponseModel<DtoBase>("Subscriber does not exist"));

        return Ok(apiSubscriber);
    }

    public async Task<IActionResult> SubscriberList(int page = 0, int pageSize = 10)
    {
        var result = await _subscriberRepository.GetApiSubscribers(page * pageSize, pageSize);
        var model = new ApiSubscriberListDto(result);
        return Ok(new ResponseModel<ApiSubscriberListDto>(string.Empty, model));
    }
}