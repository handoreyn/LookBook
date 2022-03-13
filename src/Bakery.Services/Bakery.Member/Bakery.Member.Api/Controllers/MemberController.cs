using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/api/member")]
public class MemberController : ControllerBase
{
    private readonly IMemberRepository _memberRepository;

    public MemberController(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
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
        return CreatedAtAction(nameof(MemberProfile), new { id = member.Id }, member);
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