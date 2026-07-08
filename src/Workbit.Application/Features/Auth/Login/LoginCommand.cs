using MediatR;
using Workbit.Application.Features.Auth.Shared;

public record LoginCommand(string Email, string Password) : IRequest<AuthResult>;