using Bakery.Member.Api.EventServices;
using Bakery.Member.Core.Dtos.Member;
using Bakery.Member.Core.Events;
using Bakery.Member.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Member.Api.Controllers;

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

    // POST
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

    // GET
    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn(MemberSignInDto model)
    {
        if (!ModelState.IsValid) return BadRequest(new ResponseModel<MemberSignInDto>("Missing parameters", model));

        var member = await _memberRepository.FindMember(model);
        // TODO generate JWT
        await _eventBus.PublishThroughEventBusAsync(new MemberSignedInIntegrationEvent(member.MemberId, member.Username,
            member.Username, model.Client, model.Location, member.Email));
        // TODO return JWT
        return Ok();
    }

    [HttpGet]
    [Route("forgot-password/{username}")]
    public async Task<IActionResult> ForgotPassword(string username)
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

    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> Update(string id, MemberUpdateDto model)
    {
        if (string.IsNullOrEmpty(id) || !ModelState.IsValid)
            return BadRequest(new ResponseModel<MemberUpdateDto>("Invalid parameters"));

        if (await _memberRepository.IsMemberExistById(id))
            return NotFound(new ResponseModel<DtoBase>("Member does not exist!"));

        await _memberRepository.UpdateMemberAsync(id, model);
        return Ok();
    }

    [HttpPut]
    [Route("subscribe/{memberId}")]
    public async Task<IActionResult> Subscribe(string memberId)
    {
        await _memberRepository.Subscribe(memberId);
        return Ok();
    }
}