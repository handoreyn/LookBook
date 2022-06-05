using Bakery.Member.Api.EventServices;
using Bakery.Member.Core.Dtos.Member;
using Bakery.Member.Core.Events;
using Bakery.Member.Core.Repository;
using Bakery.SharedKernel.ApiResponses;
using Bakery.SharedKernel.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Member.Api.Controllers;


///<summary>
/// <c>MemberController</c> class for handling member operations. 
///</summary>
[ApiController]
[Route("v1/api/member")]
public class MemberController : ControllerBase
{
    // Member Repo
    private readonly IMemberRepository _memberRepository;
    // Member event service
    private readonly IMemberIntegrationEventService _eventBus;

    ///<summary>
    /// Controller Ctor
    ///</summary>
    public MemberController(IMemberRepository memberRepository, IMemberIntegrationEventService eventBus)
    {
        _memberRepository = memberRepository;
        _eventBus = eventBus;
    }

    ///<summary>
    /// This action registers members to the system.
    ///</summary>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(MemberRegisterCreateDto model)
    {
        // validate model
        if (!ModelState.IsValid)
            return BadRequest(new ResponseModel<DtoBase>("Request model is incorrect"));

        // check user does exist by username
        if (await _memberRepository.IsMemberExist(model.Username))
            return BadRequest(new ResponseModel<DtoBase>("Member already exists!"));
        
        // register member
        var member = await _memberRepository.Register(model);
        
        // create member registered event
        var memberRegisteredEvent = new MemberRegisteredIntegrationEvent(member.Id, member.Username, member.Email);
        // publish member registered event
        await _eventBus.PublishThroughEventBusAsync(memberRegisteredEvent);
        //
        return CreatedAtAction(nameof(MemberProfile), new { id = member.Id }, member);
    }

    // GET
    [HttpPost]
    [Route("login")]
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

        var isMemberExist = await _memberRepository.IsMemberExistById(id);
        if (!isMemberExist) return NotFound(new ResponseModel<DtoBase>("Member does not exist!"));

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