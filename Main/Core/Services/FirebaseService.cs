using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using CorePush.Firebase;
using CorePush.Models;

namespace Core.Services
{
    public class FirebaseService(
        FirebaseSender firebaseSender
        ) : IFirebaseService
    {
        private readonly FirebaseSender _firebaseSender = firebaseSender;

        public async Task<ResponseDTO> SendNotification(FirebaseDTO notificationModel)
        {
            ResponseDTO response = new();

            try
            {
                if (notificationModel.Id_dispositivo != null)
                {
                    // Inicializar la conexión a Firebase

                    if (_firebaseSender != null)
                    {
                        // Construir el objeto a enviar en la notificación
                        FirebaseMessage payload = ConstruirModeloNotificacion(notificationModel);

                        // Enviar notificación
                        PushResult result = await _firebaseSender.SendAsync(payload);

                        if (result.IsSuccessStatusCode)
                        {
                            response.Estado = 200;
                            response.Mensaje = "Notificación enviada exitosamente.";
                            return response;
                        }
                        else
                        {
                            response.Estado = 400;
                            response.Mensaje = result.Error;
                            return response;
                        }
                    }
                    else
                    {
                        response.Estado = 400;
                        response.Mensaje = "No se encontró archivo Json.";
                        return response;
                    }
                }
                else
                {
                    response.Estado = 400;
                    response.Mensaje = "No se encontró un dispositivo válido.";
                    return response;
                }

                // Code here for APN Sender (iOS Device)
                // var apn = new ApnSender(apnSettings, httpClient);
                // await apn.SendAsync(notification, deviceToken);
            }
            catch (Exception ex)
            {
                response.Estado = 400;
                response.Mensaje = ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Método para construir el objeto a enviar en la notificación
        /// </summary>
        /// <param name="notificationModel">Datos para notificación</param>
        /// <returns></returns>
        private static FirebaseMessage ConstruirModeloNotificacion(FirebaseDTO notificationModel)
        {
            string deviceToken = notificationModel.Id_dispositivo;

            return new FirebaseMessage
            {
                message = new Message
                {
                    token = deviceToken,
                    notification = new Notification
                    {
                        title = notificationModel.Titulo ?? string.Empty,
                        body = notificationModel.Cuerpo ?? string.Empty
                    }
                }
            };
        }
    }
}
