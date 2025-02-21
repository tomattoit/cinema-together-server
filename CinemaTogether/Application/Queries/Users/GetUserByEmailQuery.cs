﻿using Application.Common.Dto;
using MediatR;

namespace Application.Queries.Users;

public record GetUserByEmailQuery(string Email) : IRequest<UserDto>;