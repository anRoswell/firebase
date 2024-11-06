namespace Core.Options
{
    public class FirebaseConfig
    {
        public string? SenderId { get; set; } // ID del remitente de Firebase
        public string? ServerKey { get; set; } // Clave del servidor (API Key)
        public string? ProjectId { get; set; } // ID del proyecto de Firebase
        public string? ServiceAccountFilePath { get; set; } // Ruta al archivo JSON de la cuenta de servicio
    }
}
