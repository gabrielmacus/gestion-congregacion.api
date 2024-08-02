namespace gestion_congregacion.api.Features.Common
{
    public interface IBaseModel : ISoftDeleteModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
