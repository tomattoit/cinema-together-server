using System;
using System.Collections.Generic;

namespace Application.Common.Dto
{
    public record GroupDetailsDto(
        Guid Id,
        string Name,
        string Description,
        string Type,
        List<string> Genres,
        Guid OwnerId,
        string OwnerUsername,
        List<MemberDto> Members,
        string ChatId
    );

    public record MemberDto(Guid Id, string Username, string ProfilePicturePath);
} 