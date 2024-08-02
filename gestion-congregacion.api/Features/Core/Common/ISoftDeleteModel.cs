namespace gestion_congregacion.api.Features.Common
{
    public interface ISoftDeleteModel
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
