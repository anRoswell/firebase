﻿using Application.PushNotification.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PushNotificationController(
        IMediator mediator
        ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationCommand notification)
        {
            var result = await _mediator.Send(notification);

            if (result.Estado == 400)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
