using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using MediatR;

namespace Application.PushNotification.Commands
{
    public class SendNotificationCommandHandler(
        IFirebaseService firebaseService,
        IMapper mapper
        ) : IRequestHandler<SendNotificationCommand, ResponseDTO>
    {
        private readonly IFirebaseService _firebaseService = firebaseService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseDTO> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            var fb = _mapper.Map<FirebaseDTO>(request);
            return await _firebaseService.SendNotification(fb);
        }
    }
}
