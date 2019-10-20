namespace Domain
{
    public class UserImage
    {
        public int UserImageId { get; set; }
        public int UserId { get; set; }
        public string TipoAcesso { get; set; }
        public string Name { get; set; }
        public byte[] Dados { get; set; }
        public string ContentType { get; set; }
    }
}