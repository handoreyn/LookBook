using Bakery.Member.Api.EventServices;
using Bakery.Member.Core.Dtos.Member;
using Bakery.Member.Core.Events;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/api/member")]
public class MemberController : ControllerBase
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMemberIntegrationEventService _eventBus;

    public MemberController(IMemberRepository memberRepository, IMemberIntegrationEventService eventBus)
    {
        _memberRepository = memberRepository;
        _eventBus = eventBus;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(MemberRegisterCreateDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResponseModel<DtoBase>("Request model is incorrect"));
        if (await _memberRepository.IsMemberExist(model.Username))
            return BadRequest(new ResponseModel<DtoBase>("Member already exists!"));

        var member = await _memberRepository.Register(model);
        var memberRegisteredEvent = new MemberRegisteredIntegrationEvent(member.Id, member.Username, member.Email);

        await _eventBus.PublishThroughEventBusAsync(memberRegisteredEvent);
        return CreatedAtAction(nameof(MemberProfile), new { id = member.Id }, member);
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(MemberSignInDto model)
    {
        if (!ModelState.IsValid) return BadRequest(new ResponseModel<MemberSignInDto>("Missing parameters", model));

        var member = await _memberRepository.FindMember(model);
        // TODO generate JWT
        await _eventBus.PublishThroughEventBusAsync(new MemberSignedIntegrationEvent(member.MemberId, member.Username,
            member.Username, model.Client, model.Location, member.Email));
        // TODO return JWT
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> ForgotPassword([FromQuery]string username)
    {
        if (string.IsNullOrEmpty(username))
            return BadRequest(new ResponseModel<DtoBase>("Email is required!", new MemberProfileDto()));

        var member = await _memberRepository.FindMember(username);
        await _eventBus.PublishThroughEventBusAsync(new MemberForgotPasswordIntegrationEvent(member.MemberId,
            member.Email, Guid.NewGuid().ToString(), member.Email));
        return Ok();
    }

    [HttpGet]
    [Route("profile/{id}")]
    public async Task<IActionResult> MemberProfile(string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest(new ResponseModel<DtoBase>("Id is required"));

        var memberProfile = await _memberRepository.GetMemberProfileDto(id);
        return Ok(new ResponseModel<MemberProfileDto>(string.Empty, memberProfile));
    }
}