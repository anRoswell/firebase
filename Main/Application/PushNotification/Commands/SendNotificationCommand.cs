using Core.DTOs;
using MediatR;
using Newtonsoft.Json;

namespace Application.PushNotification.Commands
{
    public class SendNotificationCommand : IRequest<ResponseDTO>
    {
        [JsonProperty("id_dispositivo")]
        public required string Id_dispositivo { get; set; }

        [JsonProperty("titulo")]
        public required string Titulo { get; set; }

        [JsonProperty("cuerpo")]
        public required string Cuerpo { get; set; }


        [JsonProperty("ind_android")]
        public bool Ind_android { get; set; }

        public dynamic? Notification { get; set; }

        [JsonProperty("estado")]
        public string? Estado { get; set; }

        [JsonProperty("tipo")]
        public string? Tipo { get; set; }

        [JsonProperty("id_solicitud")]
        public int Id_solicitud { get; set; }
    }
}
