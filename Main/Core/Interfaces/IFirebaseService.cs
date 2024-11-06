using Core.DTOs;

namespace Core.Interfaces
{
    public interface IFirebaseService
    {
        Task<ResponseDTO> SendNotification(FirebaseDTO notification);
    }
}
