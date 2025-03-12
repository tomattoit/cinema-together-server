﻿namespace Application.Common.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken = default);
}